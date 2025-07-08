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

### 2. Setup Environment (.env)
Buat file .env dan isi:

env
Salin
Edit
DB_HOST=localhost
DB_PORT=5432
DB_NAME=school_db
DB_USER=postgres
DB_PASSWORD=password
JWT_KEY=SuperRahasiaDanPanjangBanget12345678!!!
JWT_ISSUER=SchoolSystemAPI
Bisa juga langsung pakai appsettings.json

### 3. Restore & Run

dotnet restore
dotnet ef database update
dotnet run

### 4. Struktur File

/SchoolManagementSystem
├── /Common 
│ ├── /Constants 
│ ├── /Helpers 
│ ├── /Requests 
│ ├── /Responses 
│ ├── /Utilities 
│├── /Modules 
│ ├── /Students
│ │ ├── StudentController.cs
│ │ ├── Dtos/
│ │ ├── Entities/
│ │ ├── Mappers/
│ │ ├── Repositories/
│ │ └── Services/
│ ├── /Teachers
│ │ ├── TeacherController.cs
│ │ ├── Dtos/
│ │ ├── Entities/
│ │ ├── Mappers/
│ │ ├── Repositories/
│ │ └── Services/
│ ├── /Classes
│ ├── /Enrollments
│ └── /Users (optional for Auth + Roles)
├── /Middlewares
│ ├── ExceptionMiddleware.cs
│ ├── LoggingMiddleware.cs
│ └── JwtMiddleware.cs (optional)
├── /Configurations 
│ ├── ServiceExtensions.cs
│ ├── SwaggerConfig.cs
│ └── DatabaseConfig.cs
├── Program.cs 
├── appsettings.json 
├── .env 
└── README.md 

### 5. DB Migration & Seeding

Tambahkan Migrasi
dotnet ef migrations add Init

Update Database
dotnet ef database update
