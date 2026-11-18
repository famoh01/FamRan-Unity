import socket
import struct
import threading
import unittest
import protocol
from handshake_probe import Handler, ProbeServer

class TestHandshake(unittest.TestCase):
    def test_fragmented_handshake_over_tcp(self):
        with ProbeServer(('127.0.0.1', 0), Handler) as srv:
            thread = threading.Thread(target=srv.handle_request, daemon=True)
            thread.start()
            with socket.create_connection(srv.server_address, timeout=3) as s:
                frame = protocol.encode_frame(protocol.HANDSHAKE, b'KX01')
                s.sendall(frame[:3]); s.sendall(frame[3:])
                decoder = protocol.FrameDecoder()
                frames = []
                while not frames:
                    data = s.recv(1024)
                    self.assertTrue(data, 'connection closed before reply')
                    frames.extend(decoder.feed(data))
                op, payload = frames[0]
                self.assertEqual(op, protocol.HANDSHAKE_ACK)
                self.assertEqual(payload[:4], b'KXOK')
                self.assertNotEqual(struct.unpack('<I', payload[4:])[0], 0)
            thread.join(timeout=3)
            self.assertFalse(thread.is_alive())

if __name__ == '__main__': unittest.main()
