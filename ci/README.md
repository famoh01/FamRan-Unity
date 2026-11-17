# CI — คอมไพล์ APK ด้วย GitHub Actions

## ต้องตั้งค่า Secrets ก่อน (สำคัญ)

ไปที่ **Settings → Secrets and variables → Actions → New repository secret** แล้วเพิ่ม:

### 1) Unity License (ต้องมี 1 ใน 2 แบบ)

**แบบ A — Personal License (ฟรี)**
| Secret | ค่า |
|---|---|
| `UNITY_EMAIL` | อีเมลบัญชี Unity |
| `UNITY_PASSWORD` | รหัสผ่าน |
| `UNITY_LICENSE` | ไฟล์ license (`.ulf`) แปลงเป็น base64 |

> วิธีขอไฟล์ `.ulf`:
> 1. เปิด Unity Hub → Preferences → Licenses → Activate → **Get a free personal license**
> 2. ไฟล์จะอยู่ที่ `C:\ProgramData\Unity\Unity_lic.ulf` (Windows)
> 3. แปลง base64:
>    ```bash
>    base64 -w0 Unity_lic.ulf > license.b64
>    ```
> 4. copy เนื้อหาใน `license.b64` ใส่ secret `UNITY_LICENSE`
>
> (หรือใช้ action [game-ci/unity-request-activation-file](https://game.ci/docs/github/activation) สร้างอัตโนมัติ)

**แบบ B — Serial (Plus/Pro)**
| Secret | ค่า |
|---|---|
| `UNITY_EMAIL`, `UNITY_PASSWORD` | เหมือนกัน |
| `UNITY_SERIAL` | `XX-XXXX-XXXX-XXXX-XXXX-XXXX` |

### 2) Android Keystore (สำหรับเซ็น APK)

| Secret | ค่า |
|---|---|
| `ANDROID_KEYSTORE_BASE64` | ไฟล์ `.keystore` แปลง base64 |
| `ANDROID_KEYSTORE_PASS` | รหัสผ่าน keystore |
| `ANDROID_KEYALIAS_NAME` | ชื่อ alias |
| `ANDROID_KEYALIAS_PASS` | รหัสผ่าน alias |

สร้าง keystore ใหม่:
```bash
keytool -genkeypair -v -keystore user.keystore -alias famai \
  -keyalg RSA -keysize 2048 -validity 10950 \
  -storepass <PASS> -keypass <PASS> \
  -dname "CN=Fam Ai, O=Fam Ai, L=Ranong, C=TH"
base64 -w0 user.keystore > keystore.b64
```

> ถ้าไม่ต้องการเซ็นเอง game-ci จะเซ็นด้วย debug key ให้เล่นได้ (แต่ขึ้น Play Store ไม่ได้)

## รัน build

1. ไปที่แท็บ **Actions**
2. เลือก **Build Android APK (Unity 6000.3.25f1)**
3. กด **Run workflow**
4. รอ ~40-90 นาที (build ครั้งแรกช้าที่สุด)
5. ดาวน์โหลด **Artifacts → FamRan-Android**

## ผลลัพธ์

- `Build/*.apk` — ไฟล์ติดตั้ง (IL2CPP, ARM64, minSdk 24)
- `Build/*.aab` — ถ้าเลือก build AAB

## ข้อจำกัด free tier

| หัวข้อ | ค่า |
|---|---|
| นาที GitHub Actions (free) | 2,000 นาที/เดือน |
| build 1 ครั้ง | ~40-90 นาที (ไม่นับ cache) |
| พื้นที่ runner | 14 GB (ใช้ free-disk-space เพิ่มได้ ~50 GB) |
| repo size | repo นี้ ~3.5 GB (ต่ำกว่า soft limit 5 GB) |

> ⚠️ **Pro/Linux build ของ Unity ที่ license ฟรี**: Unity Personal รองรับ Linux build server ผ่าน game-ci ได้
