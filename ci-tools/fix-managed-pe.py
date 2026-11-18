#!/usr/bin/env python3
"""Fix PE DLL flag for exported managed .dll files in the build workspace.

Some source archive DLLs carry the PE executable flag despite being libraries.
IL2CPP treats them as executables and cannot find an entry point. This changes
only the IMAGE_FILE_DLL header bit, not any game code or asset contents.
"""
import pathlib
import struct

for path in pathlib.Path('Assets/Plugins').rglob('*.dll'):
    data = bytearray(path.read_bytes())
    if len(data) < 64 or data[:2] != b'MZ':
        print('Skipping non-PE:', path)
        continue
    pe = struct.unpack_from('<I', data, 0x3c)[0]
    if pe + 24 > len(data) or data[pe:pe+4] != b'PE\0\0':
        raise RuntimeError(f'Invalid PE: {path}')
    offset = pe + 22
    flags = struct.unpack_from('<H', data, offset)[0]
    if not flags & 0x2000:
        struct.pack_into('<H', data, offset, flags | 0x2000)
        path.write_bytes(data)
        print('Set DLL flag:', path)
