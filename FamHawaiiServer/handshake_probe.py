"""Local-only handshake compatibility probe; NOT a game server.

Accepts only the KX01 handshake and returns the KXOK nonce acknowledgement
specified by the supplied Unity client. Deliberately refuses login and world
operations. Bind is restricted to loopback; never exposes player credentials.
"""
import secrets
import socketserver
import struct
import protocol

class Handler(socketserver.BaseRequestHandler):
    def handle(self):
        self.request.settimeout(3)
        decoder = protocol.FrameDecoder()
        while True:
            chunk = self.request.recv(4096)
            if not chunk:
                return
            frames = decoder.feed(chunk)
            for opcode, payload in frames:
                if opcode != protocol.HANDSHAKE or payload != b'KX01':
                    return
                nonce = secrets.randbelow(0xffffffff) + 1
                self.request.sendall(protocol.encode_frame(
                    protocol.HANDSHAKE_ACK, b'KXOK' + struct.pack('<I', nonce)))
                return

class ProbeServer(socketserver.TCPServer):
    allow_reuse_address = True

if __name__ == '__main__':
    with ProbeServer(('127.0.0.1', 7777), Handler) as server:
        print('LOCAL TEST ONLY: KX01 handshake probe on 127.0.0.1:7777', flush=True)
        server.serve_forever()
