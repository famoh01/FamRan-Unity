"""Local protocol-integration prototype, NOT a playable game server.

Runs handshake and validates token/character ownership, then explicitly
reports that the world is unavailable. No encrypted transport, persistence,
rate limits, or gameplay. Binds only to localhost and must never be exposed.
"""
import json
import secrets
import socketserver
import struct
import protocol

ERROR = 0x80ff

class WorldHandler(socketserver.BaseRequestHandler):
    def handle(self):
        s = self.request
        s.settimeout(5)
        decoder = protocol.FrameDecoder()
        stage = 'handshake'
        nonce = secrets.randbelow(0xffffffff) + 1
        account = None
        try:
            while True:
                data = s.recv(4096)
                if not data: return
                for opcode, payload in decoder.feed(data):
                    if stage == 'handshake':
                        if opcode != protocol.HANDSHAKE or payload != b'KX01': return
                        s.sendall(protocol.encode_frame(protocol.HANDSHAKE_ACK, b'KXOK' + struct.pack('<I',nonce)))
                        stage = 'login'
                        continue
                    payload = protocol.xor_payload(payload, 0) if payload else payload
                    try: body = json.loads(payload.decode('utf-8')) if payload else {}
                    except (ValueError, UnicodeDecodeError): return
                    if not isinstance(body,dict): return
                    app = self.server.app
                    if stage == 'login' and opcode == protocol.LOGIN:
                        token=body.get('token')
                        if not isinstance(token,str): return
                        with app.lock: account=app.handoffs.pop(token,None)
                        if account is None:
                            self.reply(ERROR, {'ok':False,'error':'invalid handoff token'}, nonce); return
                        self.reply(protocol.LOGIN_RESULT, {'ok':True}, nonce)
                        stage='select'
                    elif stage == 'select' and opcode == protocol.CHAR_SELECT:
                        char_id=body.get('id')
                        if not isinstance(char_id,str): return
                        with app.lock:
                            row=app.db.execute('SELECT id FROM characters WHERE id=? AND owner=?',(char_id,account)).fetchone()
                        if row is None:
                            self.reply(protocol.CHAR_SELECT_RESULT, {'ok':False}, nonce);return
                        self.reply(protocol.CHAR_SELECT_RESULT, {'ok':True}, nonce)
                        stage='world'
                    elif stage == 'world' and opcode == protocol.ENTER_WORLD:
                        self.reply(ERROR, {'ok':False,'error':'world not implemented'}, nonce);return
                    else: return
        except (OSError, ValueError): return

    def reply(self, opcode, body, nonce):
        payload=json.dumps(body,separators=(',',':')).encode('utf-8')
        self.request.sendall(protocol.encode_frame(opcode,payload,nonce=nonce))

class WorldServer(socketserver.ThreadingTCPServer):
    allow_reuse_address = True
    daemon_threads = True
    def __init__(self, app, addr=('127.0.0.1',7777)):
        if addr[0] != '127.0.0.1':raise ValueError('local test only')
        self.app=app
        super().__init__(addr,WorldHandler)
