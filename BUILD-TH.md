# 🚀 คอมไพล์ KlassX/Campus Wars เป็น APK ด้วย GitHub Actions

โปรเจกต์นี้ **คอมไพล์เองที่เครื่องคุณไม่ต้อง** — ใช้ GitHub Actions (ฟรี 2,000 นาที/เดือน) คอมไพล์บนคลาวด์ให้

---

## ⚡ ขั้นตอน (4 ขั้น)

### 1) เตรียม Unity License

Unity ต้องมี license ถึงจะ build ได้ — เลือก 1 วิธี:

#### วิธี A: Personal License (ฟรี) ✅ แนะนำ

**บนเครื่องคุณ (Windows):**
```powershell
# 1. เปิด Unity Hub → Preferences → Licenses → Activate → Get a free personal license
# 2. ไฟล์จะอยู่ที่:
#    C:\ProgramData\Unity\Unity_lic.ulf

# 3. แปลงเป็น base64 แล้ว copy ข้อความ
[Convert]::ToBase64String([IO.File]::ReadAllBytes("C:\ProgramData\Unity\Unity_lic.ulf")) | Set-Clipboard
```
→ เอาไปใส่ secret ชื่อ `UNITY_LICENSE`

#### วิธี B: ใช้ game-ci activation workflow (ไม่ต้องหาไฟล์เอง)

1. เพิ่มไฟล์นี้ที่ `.github/workflows/activation.yml`
2. รัน → ดาวน์โหลด `Unity_v6000.x.alf` → อัปโหลดที่ https://license.unity3d.com/manual → ได้ `.ulf`
3. แปลง base64 → ใส่ secret

```yaml
# .github/workflows/activation.yml
name: Acquire activation file
on: [workflow_dispatch]
jobs:
  activation:
    name: Request manual activation file
    runs-on: ubuntu-latest
    steps:
      - uses: game-ci/unity-request-activation-file@v2
        id: getManualLicenseFile
        with:
          unityVersion: 6000.3.25f1
      - uses: actions/upload-artifact@v4
        with:
          name: unity-activation-file
          path: ${{ steps.getManualLicenseFile.outputs.filePath }}
```

---

### 2) ตั้ง Secrets ใน GitHub

ไปที่ **Settings → Secrets and variables → Actions → New repository secret**

| Secret | ค่า | จำเป็น |
|---|---|---|
| `UNITY_LICENSE` | เนื้อหา base64 ของ `Unity_lic.ulf` | ✅ (ถ้าใช้ Personal) |
| `UNITY_EMAIL` | อีเมลบัญชี Unity | ✅ |
| `UNITY_PASSWORD` | รหัสผ่าน | ✅ |
| `UNITY_SERIAL` | `XX-XXXX-XXXX-XXXX-XXXX-XXXX` | (เฉพาะ Plus/Pro) |
| `ANDROID_KEYSTORE_BASE64` | base64 ของ `user.keystore` | แนะนำ |
| `ANDROID_KEYSTORE_PASS` | รหัส keystore | แนะนำ |
| `ANDROID_KEYALIAS_NAME` | เช่น `famai` | แนะนำ |
| `ANDROID_KEYALIAS_PASS` | รหัส alias | แนะนำ |

#### สร้าง keystore (ถ้ายังไม่มี)

```bash
keytool -genkeypair -v -keystore user.keystore -alias famai \
  -keyalg RSA -keysize 2048 -validity 10950 \
  -storepass FamAi2026 -keypass FamAi2026 \
  -dname "CN=Fam Ai, O=Fam Ai, L=Ranong, C=TH"

# Windows (PowerShell)
[Convert]::ToBase64String([IO.File]::ReadAllBytes("user.keystore")) | Set-Clipboard
```

> ถ้าไม่ใส่ keystore → game-ci จะเซ็นด้วย debug key ให้ติดตั้งเล่นได้ แต่ขึ้น Play Store ไม่ได้

---

### 3) รัน Build

ไปที่แท็บ **Actions** → เลือก **"Build Android APK (Unity 6000.3.25f1)"** → กด **Run workflow**

หรือรันผ่าน command line:
```bash
curl -X POST -H "Authorization: token <TOKEN>" \
  https://api.github.com/repos/famoh01/FamRan-Unity/actions/workflows/unity-android.yml/dispatches \
  -d '{"ref":"main"}'
```

---

### 4) ดาวน์โหลด APK

หลัง build เสร็จ (~40-90 นาที):
- **Actions** → คลิก run ล่าสุด → เลื่อนลง **Artifacts** → ดาวน์โหลด **FamRan-Android**
- ข้างในมี `KlassXLocal.apk` (และ `.aab` ถ้าเลือก)

---

## 🎯 สิ่งที่ workflow ทำให้

| ขั้น | ทำอะไร |
|---|---|
| 1. Checkout | ดึงโค้ด 3.5 GB |
| 2. Free disk | ล้างเครื่อง runner เพิ่มพื้นที่เป็น ~50 GB |
| 3. Cache Library | cache `Library/` เพื่อ build ครั้งต่อไปเร็วขึ้น |
| 4. Unity Build | เรียก `game-ci/unity-builder` → รัน `BuildKlassx.Build` |
| 5. Upload | เก็บ APK เป็น artifact |
| 6. Release | ถ้า push tag → สร้าง Release อัตโนมัติ |

**การตั้งค่าที่ `BuildKlassx.cs` ทำ:**
- Bundle ID: `com.klab.klassx.selfhost`
- IL2CPP + ARM64
- Managed stripping: Disabled (สำคัญสำหรับโค้ด decompile)
- `insecureHttpOption: AlwaysAllowed` (สำหรับ HTTP 127.0.0.1)
- Texture subtarget: ETC2
- Scenes: ทุก scene ที่ enabled ใน `EditorBuildSettings.asset`

---

## ⚠️ ปัญหาที่อาจเจอ

| อาการ | สาเหตุ | แก้ |
|---|---|---|
| `No valid Unity Editor license found` | ยังไม่ตั้ง `UNITY_LICENSE` | ทำขั้นที่ 1-2 |
| `License is for a different version` | license ไม่ตรง 6000.3 | ขอ license ใหม่จาก Unity Hub เวอร์ชันเดียวกัน |
| `error CS0246: type or namespace not found` | สคริปต์ decompile ขาด dependency | ดูหัวข้อ "แก้ compile error" |
| `Library folder not found` | ปกติ (gitignore) | Unity จะสร้างใหม่เอง |
| build นาน > 6 ชม. | runner timeout | ลด texture / ใช้ self-hosted runner |
| `out of disk space` | repo 3.5 GB + Unity 4 GB + Gradle | step "Free disk space" ช่วยได้ |

---

## 🔧 แก้ compile error (ถ้าเจอ)

โปรเจกต์นี้มาจาก **AssetRipper decompile** — สคริปต์บางไฟล์อาจ compile ไม่ผ่าน วิธีแก้:

### หา error ทั้งหมดในที่เดียว

```bash
# ในเครื่อง (มี Unity) — เปิด Unity แล้วดู Console
# หรือดูจาก log ของ Actions
```

### ประเภท error ที่พบบ่อย

1. **`The type or namespace name 'X' could not be found`**
   → มักเกิดจาก DLL ใน `Assets/Plugins/` มี namespace ไม่ครบ
   → เช็คว่า DLL ที่ต้องการอยู่ใน Plugins หรือไม่

2. **`'X' does not contain a definition for 'Y'`**
   → decompile พลาด method
   → เพิ่ม method stub หรือ resolve จาก DLL ต้นฉบับ

3. **`The type 'X' has no constructors defined`**
   → struct/class ที่ decompile ไม่สมบูรณ์
   → เพิ่ม constructor ว่าง

4. **`Cannot implicitly convert type`**
   → ชนิดข้อมูลเพี้ยนจากการ decompile
   → cast หรือแก้ type ให้ตรง

### ถ้าอยากให้ผมช่วยแก้

ส่ง **log จาก Actions** (คลิก run → job → step "Build with Unity" → copy 50 บรรทัดท้าย) มาให้ผม — ผมจะแก้สคริปต์ให้ทีละจุด

---

## 💰 ค่าใช้จ่าย

| รายการ | free tier |
|---|---|
| GitHub Actions | 2,000 นาที/เดือน (private repo) — **public repo ฟรีไม่จำกัด** ✅ |
| Storage | repo ~3.5 GB (ต่ำกว่า soft limit 5 GB) |
| Artifacts | 500 MB (APK ปกติ ~300-600 MB สำหรับเกมนี้) |
| Unity License | Personal ฟรี (รายได้ < $200k/ปี) |

> **repo นี้เป็น public → Actions ฟรีไม่จำกัดนาที** 🎉

---

## 📊 คาดการณ์เวลา build

| ครั้ง | เวลา |
|---|---|
| ครั้งแรก (ไม่มี cache) | 60-90 นาที |
| ครั้งต่อไป (มี cache) | 25-40 นาที |

ส่วนใหญ่หมดไปกับ:
- Import 15,000+ asset (20-40 นาที)
- IL2CPP compile (15-25 นาที)
- Gradle build (5-10 นาที)
