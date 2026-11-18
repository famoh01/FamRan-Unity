import json
import socket
import struct
import threading
import unittest
import protocol
from auth_probe import App
from world_probe import WorldServer, ERROR

class WorldFlow(unittest.TestCase):
    def test_handoff_and_character_ownership_then_world_unavailable(self):
        app=App()
        with app.lock:
            app.db.execute('INSERT INTO users(username,salt,digest) VALUES(?,?,?)',('tester',b'x',b'y'))
            app.db.execute('INSERT INTO characters(owner,name,slot,cls,school,hair,haircolor,face) VALUES(1,\'Test\',0,0,0,0,0,0)')
            app.db.commit();app.handoffs['test-token']=1
        with WorldServer(app,('127.0.0.1',0)) as server:
            th=threading.Thread(target=server.serve_forever,daemon=True);th.start()
            try:
                with socket.create_connection(server.server_address,timeout=3) as sock:
                    def read_exact(n):
                        b=b''
                        while len(b)<n:
                            data=sock.recv(n-len(b))
                            if not data: raise EOFError()
                            b+=data
                        return b
                    def read_frame(nonce=None):
                        length,op=struct.unpack('<HH',read_exact(4));p=read_exact(length)
                        if nonce is not None: p=protocol.xor_payload(p,nonce)
                        return op,p
                    def send(op,body):
                        sock.sendall(protocol.encode_frame(op,json.dumps(body,separators=(',',':')).encode(),nonce=0))
                    sock.sendall(protocol.encode_frame(protocol.HANDSHAKE,b'KX01'))
                    op,p=read_frame();self.assertEqual(op,protocol.HANDSHAKE_ACK);self.assertEqual(p[:4],b'KXOK');nonce=struct.unpack('<I',p[4:])[0]
                    send(protocol.LOGIN,{'token':'test-token'});op,p=read_frame(nonce);self.assertEqual(op,protocol.LOGIN_RESULT);self.assertTrue(json.loads(p)['ok'])
                    send(protocol.CHAR_SELECT,{'id':'1'});op,p=read_frame(nonce);self.assertEqual(op,protocol.CHAR_SELECT_RESULT);self.assertTrue(json.loads(p)['ok'])
                    send(protocol.ENTER_WORLD,{'gender':0});op,p=read_frame(nonce);self.assertEqual(op,ERROR);self.assertFalse(json.loads(p)['ok'])
                    self.assertNotIn('test-token',app.handoffs)
            finally:server.shutdown();th.join(3)

if __name__=='__main__': unittest.main()
