# 🏫 School Management System (ASP.NET Core + PostgreSQL)

Sistem manajemen sekolah berbasis REST API dengan fitur:
- CRUD untuk Siswa (Murid), Guru, Kelas, dan Pendaftaran
- Autentikasi JWT dengan Role (Admin & Guru)
- PostgreSQL sebagai database (via Supabase / lokal)
- Swagger UI untuk dokumentasi API interaktif

---

## 📦 Teknologi

- ASP.NET Core 8
- Entity Framework Core (Npgsql)
- PostgreSQL (support Neon/Supabase)
- JWT Authentication
- Swagger
- BCrypt untuk hashing password

---

## 🚀 Cara Menjalankan Secara Lokal

### 1. Clone Repository
```bash
git clone https://github.com/username/school-management-system.git
cd school-management-system
```

### 2. Setup Environment (.env)
Buat file `.env` dan isi:

```env
DB_HOST=localhost
DB_PORT=5432
DB_NAME=school_db
DB_USER=postgres
DB_PASSWORD=password
JWT_KEY=SuperRahasiaDanPanjangBanget12345678!!!
JWT_ISSUER=SchoolSystemAPI
```

Atau langsung isi di `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=school_db;Username=postgres;Password=yourpassword"
},
"Jwt": {
  "Key": "SuperRahasiaDanPanjangBanget12345678!!!",
  "Issuer": "SchoolSystemAPI"
}
```

### 3. Restore & Run

```bash
dotnet restore
dotnet ef database update
dotnet run
```

### 4. Struktur File

```
/SchoolManagementSystem
├── /Common 
│   ├── /Constants 
│   ├── /Helpers 
│   ├── /Requests 
│   ├── /Responses 
│   ├── /Utilities 
├── /Modules 
│   ├── /Students
│   │   ├── StudentController.cs
│   │   ├── Dtos/
│   │   ├── Entities/
│   │   ├── Mappers/
│   │   ├── Repositories/
│   │   └── Services/
│   ├── /Teachers
│   │   ├── TeacherController.cs
│   │   ├── Dtos/
│   │   ├── Entities/
│   │   ├── Mappers/
│   │   ├── Repositories/
│   │   └── Services/
│   ├── /Classes
│   ├── /Enrollments
│   └── /Users (optional for Auth + Roles)
├── /Middlewares
│   ├── ExceptionMiddleware.cs
│   ├── LoggingMiddleware.cs
│   └── JwtMiddleware.cs (optional)
├── /Configurations 
│   ├── ServiceExtensions.cs
│   ├── SwaggerConfig.cs
│   └── DatabaseConfig.cs
├── Program.cs 
├── appsettings.json 
├── .env 
└── README.md 
```

### 5. DB Migration & Seeding

```bash
dotnet ef migrations add Init
dotnet ef database update
```

---

## 📦 API Endpoints

> 🛡 = Butuh JWT authentication  (Masih bingung menjalankannya)
> 🔒 = Beberapa service ada role yanda dapat akses ()

### 🧑‍💻 Auth (Public)

#### POST `/api/auth/register`
```json
{
  "username": "admin1",
  "password": "rahasia123",
  "role": "Admin"
}
```

#### POST `/api/auth/login`
```json
{
  "username": "admin1",
  "password": "rahasia123"
}
```

✔️ Hasil:
```json
{
  "token": "eyJhbGciOi...",
  "username": "admin1",
  "role": "Admin"
}
```

Use this token in headers:
```
Authorization: Bearer <token>
```

---

### 👩‍🏫 Teachers

> 🛡🔒 `Admin`

- `GET /api/teacher`
- `POST /api/teacher`
- `PUT /api/teacher/{id}`
- `DELETE /api/teacher/{id}`

---

### 👨‍🎓 Students

> 🛡🔒 `Admin`

- `GET /api/student`
- `POST /api/student`
- `PUT /api/student/{id}`
- `DELETE /api/student/{id}`

---

### 🏫 Classes

> 🛡🔒 `Admin`, `Guru`

- `GET /api/class`
- `POST /api/class`
- `PUT /api/class/{id}`
- `DELETE /api/class/{id}`
- `POST /api/class/assign-teacher`

---

### 📝 Enrollments

> 🛡🔒 `Admin`, `Guru`

- `GET /api/enrollment`
- `POST /api/enrollment`
- `DELETE /api/enrollment/{id}`

---

## 🧠 Role-Akses Access

| Role   | Akses Endpoint                    |
|--------|-----------------------------------|
| Admin  | Semua                             |
| Guru   | Hanya endpoint tertentu (`kelas`, `murid`) |
| Public | Hanya `/auth/register`, `/auth/login` |

Gunakan `[Authorize(Roles = "Admin")]` atau `[Authorize(Roles = "Guru")]` di controller.

---

## 🧱 Database Design (Ringkas)

- `Users (id, username, passwordHash, role)`
- `Murid (id, nama, userId)`
- `Guru (id, nama, userId)`
- `Kelas (id, nama, id_guru)`
- `Pendaftaran (id, id_student, id_kelas)`

---

## 📬 Contact

Made with ❤️ by Alqan Ganteng amin
