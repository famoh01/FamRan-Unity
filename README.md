# 🎮 FamRan-Unity — KlassX / Campus Wars (Unity 6000.3)

โปรเจกต์ Unity ต้นฉบับของเกม **KlassX / Campus Wars** พร้อม **GitHub Actions** ที่คอมไพล์เป็น APK ให้อัตโนมัติ

> 📱 **APK พร้อมติดตั้ง (เกมที่สร้างใหม่ด้วย HTML5)**: [famoh01/FamRan](https://github.com/famoh01/FamRan)

---

## ⚡ เริ่มต้นเร็ว

1. **ตั้ง Unity License** → [BUILD-TH.md](BUILD-TH.md) ขั้นที่ 1
2. **เพิ่ม Secrets** (Unity license + keystore) → [BUILD-TH.md](BUILD-TH.md) ขั้นที่ 2
3. **Actions → Run workflow** → [BUILD-TH.md](BUILD-TH.md) ขั้นที่ 3
4. **ดาวน์โหลด APK** จาก Artifacts → [BUILD-TH.md](BUILD-TH.md) ขั้นที่ 4

👉 **คู่มือละเอียด: [BUILD-TH.md](BUILD-TH.md)**

---

## 📋 ข้อมูลโปรเจกต์

| รายการ | ค่า |
|---|---|
| Unity | `6000.3.25f1` (rev `e1dba0a9aba4`) |
| Render pipeline | URP `17.3.0` |
| Scenes | 37 ไฟล์ `.unity` (89 entries ใน build settings) |
| C# scripts | 952 ไฟล์ใน `Assets/Scripts/Assembly-CSharp` |
| Assets ทั้งหมด | 15,115 ไฟล์ |
| ขนาด repo | ~3.5 GB |
| Bundle ID (build) | `com.klab.klassx.selfhost` |
| Backend เป้าหมาย | IL2CPP · ARM64 · minSdk 24 · targetSdk 34 |

---

## 🗂️ โครงสร้าง

```
RanMobileCampusWars/
├─ Assets/
│  ├─ Scripts/Assembly-CSharp/    # 952 สคริปต์เกม (decompiled)
│  ├─ Editor/                     # BuildKlassx.cs + AssetRipperPatches
│  ├─ Scenes/                     # 37 scenes (Init, Login, CharacterSelect, Maplist, …)
│  ├─ Resources/_ranclient/       # data ที่เกมโหลดตอนรัน
│  ├─ Texture2D/                  # 1,315 textures (ย่อ 4K→2K จำนวน 9 ไฟล์)
│  ├─ Mesh/ AnimationClip/ Sprite/ Prefab/ …
│  └─ Plugins/                    # 27 DLL
├─ ProjectSettings/
│  ├─ ProjectVersion.txt          # 6000.3.25f1
│  ├─ EditorBuildSettings.asset   # scene list
│  └─ ProjectSettings.asset       # Android settings
├─ Packages/
│  ├─ manifest.json               # 32 dependencies
│  └─ packages-lock.json          # 46 resolved
├─ ci/README.md                   # วิธีตั้ง secrets แบบละเอียด
├─ BUILD-TH.md                    # คู่มือ build ไทย
└─ .github/workflows/
   └─ unity-android.yml           # CI build APK
```

---

## ⚠️ ข้อจำกัดที่ต้องรู้

### 1. นี่คือ **AssetRipper decompile** ไม่ใช่โปรเจกต์ Unity ต้นฉบับ
- สคริปต์ 952 ไฟล์ถูก decompile กลับมาจาก DLL
- **อาจมี compile error** ที่ต้องแก้ (โค้ดที่ decompile ไม่สมบูรณ์ 100%)
- Asset reference บางส่วนอาจขาด (missing GUID)

### 2. ต้องมี Unity License
- Unity ไม่ให้ build ถ้าไม่มี license (แม้แต่ Personal ก็ต้อง activate)
- ต้องตั้ง secret `UNITY_LICENSE` ก่อน

### 3. Texture ถูกย่อ
- ไฟล์ `.png` ขนาด 8192×8192 จำนวน 9 ไฟล์ (94-100 MB) **ย่อเป็น 2048×2048**
- เพราะ GitHub จำกัดไฟล์เดี่ยว 100 MB
- ถ้าต้องการคุณภาพเต็ม → แตกจาก RAR ต้นฉบับแทน

### 4. `Library/` ไม่ได้ commit
- Unity จะสร้างใหม่เองตอนเปิด/build
- ทำให้ build ครั้งแรกช้า (import asset ใหม่ทั้งหมด)
- cache จะช่วยใน build ครั้งต่อไป

---

## 🔗 ลิงก์ที่เกี่ยวข้อง

| | |
|---|---|
| 🎮 **เกมมือถือ (APK พร้อมเล่น)** | https://github.com/famoh01/FamRan |
| 🎯 Releases | https://github.com/famoh01/FamRan/releases |
| ⚡ Actions (build) | https://github.com/famoh01/FamRan-Unity/actions |
| 📖 คู่มือ build | [BUILD-TH.md](BUILD-TH.md) |
| 🔧 วิธีตั้ง secrets | [ci/README.md](ci/README.md) |

---

## ⚖️ ลิขสิทธิ์

โปรเจกต์นี้เป็น decompile export ของ **KlassX / Campus Wars** — **ไม่มีไลเซนส์จากเจ้าของเดิม**
ใช้เพื่อการศึกษา/เก็บถาวรเท่านั้น การนำไปใช้เชิงพาณิชย์ต้องได้รับอนุญาตจากเจ้าของลิขสิทธิ์

ไฟล์ `.github/workflows/` และ `BUILD-TH.md` เขียนขึ้นใหม่ — ใช้ได้อิสระ
