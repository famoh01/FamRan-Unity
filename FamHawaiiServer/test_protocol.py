import struct
import unittest
from protocol import FrameDecoder, encode_frame, xor_payload, HANDSHAKE, HANDSHAKE_ACK

class TestProtocol(unittest.TestCase):
    def test_handshake_frame(self):
        self.assertEqual(encode_frame(HANDSHAKE, b'KX01'), b'\x04\x00\x01\x00KX01')
    def test_ack_frame(self):
        self.assertEqual(encode_frame(HANDSHAKE_ACK, b'KXOK'+struct.pack('<I', 123)), b'\x08\x00\x01\x80KXOK\x7b\x00\x00\x00')
    def test_xor_reversible_and_nonce_matters(self):
        original = b'{"token":"test"}'
        self.assertEqual(xor_payload(xor_payload(original, 123), 123), original)
        self.assertNotEqual(xor_payload(original, 123), xor_payload(original, 456))
    def test_fragmentation_and_multiple_frames(self):
        data = encode_frame(1, b'KX01') + encode_frame(3, b'')
        dec = FrameDecoder()
        self.assertEqual(dec.feed(data[:2]), [])
        self.assertEqual(dec.feed(data[2:5]), [])
        self.assertEqual(dec.feed(data[5:]), [(1, b'KX01'), (3, b'')])
    def test_reject_too_large(self):
        with self.assertRaises(ValueError): encode_frame(1, b'a'*65536)

if __name__ == '__main__': unittest.main()
