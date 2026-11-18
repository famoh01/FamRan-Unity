"""Wire framing observed in the supplied RANMobile WorldTcpClient.cs.

This is compatibility plumbing, not an operational game server.  The XOR
stream is obfuscation, NOT cryptographic security; never rely on it to protect
credentials or use it as a substitute for encrypted transport.
"""
import struct

KEY_MIX = 1511506921
HANDSHAKE = 0x0001
HANDSHAKE_ACK = 0x8001
LOGIN = 0x0002
LOGIN_RESULT = 0x8002
CHAR_SELECT = 0x0013
CHAR_SELECT_RESULT = 0x8013
ENTER_WORLD = 0x0030
ZONE_IDENTIFY = 0x8020
HEARTBEAT = 0x0003
HEARTBEAT_ACK = 0x8003


def xor_payload(payload: bytes, nonce: int) -> bytes:
    state = (KEY_MIX ^ nonce) & 0xffffffff
    result = bytearray()
    for byte in payload:
        state ^= (state << 13) & 0xffffffff
        state ^= state >> 17
        state ^= (state << 5) & 0xffffffff
        state &= 0xffffffff
        result.append(byte ^ (state & 0xff))
    return bytes(result)


def encode_frame(opcode: int, payload: bytes, *, nonce=None) -> bytes:
    if not 0 <= opcode <= 0xffff or len(payload) > 0xffff:
        raise ValueError('opcode or payload exceeds 16-bit frame size')
    if nonce is not None and payload:
        payload = xor_payload(payload, nonce)
    return struct.pack('<HH', len(payload), opcode) + payload


class FrameDecoder:
    def __init__(self):
        self.buffer = bytearray()

    def feed(self, data: bytes):
        self.buffer.extend(data)
        frames = []
        while len(self.buffer) >= 4:
            length, opcode = struct.unpack_from('<HH', self.buffer)
            if len(self.buffer) < length + 4:
                break
            payload = bytes(self.buffer[4:4+length])
            del self.buffer[:4+length]
            frames.append((opcode, payload))
        return frames
