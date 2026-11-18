"""Local compatibility prototype for the supplied client's HTTP endpoints.

NOT production-ready. Localhost only, in-memory database, no TLS, no rate limit,
no account recovery, no persistent data and no world server. Never use real
passwords or expose it to the internet. No game content is included here.
"""
import hashlib
import hmac
import json
import secrets
import sqlite3
import threading
from http.server import BaseHTTPRequestHandler, ThreadingHTTPServer
from urllib.parse import urlsplit

class App:
    def __init__(self):
        self.db = sqlite3.connect(':memory:', check_same_thread=False)
        self.db.executescript('''CREATE TABLE users (id INTEGER PRIMARY KEY, username TEXT UNIQUE COLLATE NOCASE, salt BLOB, digest BLOB);
          CREATE TABLE characters (id INTEGER PRIMARY KEY, owner INTEGER, name TEXT UNIQUE, slot INTEGER, cls INTEGER, school INTEGER, hair INTEGER, haircolor INTEGER, face INTEGER);
        ''')
        self.lock = threading.RLock()
        self.sessions = {}
        self.handoffs = {}

    @staticmethod
    def digest(password, salt):
        return hashlib.scrypt(password.encode('utf-8'), salt=salt, n=16384, r=8, p=1)

class Handler(BaseHTTPRequestHandler):
    server_version = 'FamRanLocalProbe'
    def log_message(self, *args): pass
    def send_json(self, code, body):
        data = json.dumps(body, separators=(',', ':')).encode()
        self.send_response(code)
        self.send_header('Content-Type', 'application/json')
        self.send_header('Content-Length', str(len(data)))
        self.end_headers()
        self.wfile.write(data)
    def read_json(self):
        size = int(self.headers.get('Content-Length', '0'))
        if size > 16384: raise ValueError('request too large')
        return json.loads(self.rfile.read(size) or b'{}')
    def user_id(self):
        value = self.headers.get('Authorization', '')
        return self.server.app.sessions.get(value[7:]) if value.startswith('Bearer ') else None
    def do_POST(self):
        try:
            self._post()
        except (ValueError, TypeError, json.JSONDecodeError):
            self.send_json(400, {'error': 'invalid request'})
    def _post(self):
        app = self.server.app
        path = urlsplit(self.path).path
        data = self.read_json()
        with app.lock:
            if path == '/api/auth/register':
                name, password = data.get('username'), data.get('password')
                if not isinstance(name,str) or not isinstance(password,str) or not (1 <= len(name) <= 64 and 1 <= len(password) <= 256):
                    return self.send_json(400, {'error':'invalid credentials'})
                if password != data.get('confirmPassword'): return self.send_json(400, {'error':'password mismatch'})
                salt = secrets.token_bytes(16)
                try:
                    app.db.execute('INSERT INTO users(username,salt,digest) VALUES(?,?,?)', (name,salt,app.digest(password,salt)))
                    app.db.commit()
                except sqlite3.IntegrityError:
                    return self.send_json(409, {'error':'username already exists'})
                return self.send_json(201, {'ok':True})
            if path == '/api/auth/login':
                name,password = data.get('username'),data.get('password')
                if not isinstance(name,str) or not isinstance(password,str) or len(password)>256:
                    return self.send_json(400,{'error':'invalid credentials'})
                row=app.db.execute('SELECT id,salt,digest FROM users WHERE username=?',(name,)).fetchone()
                if row is None or not hmac.compare_digest(row[2],app.digest(password,row[1])):
                    return self.send_json(401, {'error':'login failed'})
                token=secrets.token_urlsafe(32);app.sessions[token]=row[0]
                return self.send_json(200, {'token':token})
            uid=self.user_id()
            if uid is None: return self.send_json(401, {'error':'unauthorized'})
            if path == '/api/auth/token':
                token=secrets.token_urlsafe(32);app.handoffs[token]=uid
                return self.send_json(200,{'newToken':token})
            if path == '/api/characters':
                name=data.get('name')
                if not isinstance(name,str) or not (1 <= len(name) <= 32): return self.send_json(400, {'error':'invalid name'})
                slots={x[0] for x in app.db.execute('SELECT slot FROM characters WHERE owner=?',(uid,))}
                slot=next((x for x in range(4) if x not in slots),None)
                if slot is None:return self.send_json(409,{'error':'no slots'})
                try:
                    app.db.execute('INSERT INTO characters(owner,name,slot,cls,school,hair,haircolor,face) VALUES(?,?,?,?,?,?,?,?)',
                                   (uid,name,slot,int(data.get('class',0)),int(data.get('school',0)),int(data.get('hairType',0)),int(data.get('hairColorType',0)),int(data.get('faceType',0))))
                    app.db.commit()
                except sqlite3.IntegrityError:return self.send_json(409,{'error':'name already exists'})
                return self.send_json(201,{'ok':True})
            return self.send_json(404,{'error':'not found'})
    def do_GET(self):
        app=self.server.app
        with app.lock:
            # Prototype list only. It is deliberately marked offline because
            # a real world server and deployment do not exist yet.
            path = urlsplit(self.path).path
            if path == '/api/servers':
                return self.send_json(200, {'servers':[{'id':1,'name':'FamHawaii','host':'127.0.0.1','port':7777,'status':'offline','population':0,'maxPopulation':0}]})
            if path == '/api/servers/1/channels':
                return self.send_json(200, {'channels':[]})
            uid=self.user_id()
            if uid is None:return self.send_json(401,{'error':'unauthorized'})
            if urlsplit(self.path).path != '/api/characters':return self.send_json(404,{'error':'not found'})
            rows=app.db.execute('SELECT id,name,slot,cls,school,hair,haircolor,face FROM characters WHERE owner=? ORDER BY slot',(uid,)).fetchall()
            chars=[{'id':str(x[0]),'name':x[1],'slot':x[2],'class':x[3],'school':x[4],'hairType':x[5],'hairColorType':x[6],'faceType':x[7],'gender':0,'level':1,'exp':0} for x in rows]
            return self.send_json(200,{'characters':chars,'max':4})
    def do_DELETE(self):
        app=self.server.app
        with app.lock:
            uid=self.user_id()
            if uid is None:return self.send_json(401,{'error':'unauthorized'})
            path=urlsplit(self.path).path
            if not path.startswith('/api/characters/'):return self.send_json(404,{'error':'not found'})
            identifier=path.rsplit('/',1)[-1]
            cur=app.db.execute('DELETE FROM characters WHERE id=? AND owner=?',(identifier,uid));app.db.commit()
            return self.send_json(200 if cur.rowcount else 404,{'ok':bool(cur.rowcount)})

class Server(ThreadingHTTPServer):
    def __init__(self, addr=('127.0.0.1',8085)):
        if addr[0] != '127.0.0.1': raise ValueError('prototype must bind to localhost')
        super().__init__(addr, Handler)
        self.app=App()

if __name__ == '__main__':
    with Server() as server:
        print('LOCAL TEST ONLY: no persistence, no TLS, no world service', flush=True)
        server.serve_forever()
