using Microsoft.EntityFrameworkCore;
using AutoMapper;
using SchoolManagementSystem.Configurations.AppDbContext;
using SchoolManagementSystem.Modules.Students.Services;
using SchoolManagementSystem.Modules.Students.Mappers;
using SchoolManagementSystem.Modules.Teachers.Repositories;
using SchoolManagementSystem.Modules.Teachers.Mappers;
using SchoolManagementSystem.Modules.Classes.Services;
using SchoolManagementSystem.Modules.Classes.Repositories;
using SchoolManagementSystem.Modules.Classes.Mappers;
using SchoolManagementSystem.Modules.Enrollments.Services;
using SchoolManagementSystem.Modules.Enrollments.Repositories;
using SchoolManagementSystem.Modules.Enrollments.Mappers;
using SchoolManagementSystem.Modules.Users.Services;
using SchoolManagementSystem.Modules.Users.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Tambahkan AppDbContext ke DI container
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Tambahkan AutoMapper dan pindai semua profil
builder.Services.AddAutoMapper(typeof(StudentProfile), typeof(TeachersProfile), typeof(ClassProfile), typeof(EnrollmentProfile));

// Tambahkan layanan dan repository ke DI
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ITeacherRepo, TeacherRepository>();
builder.Services.AddScoped<IClassRepository, ClassRepository>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentServices>();
builder.Services.AddScoped<IEnrollmentRepo, EnrollmentRepository>();
builder.Services.AddScoped<IClassRepository, ClassRepository>();
builder.Services.AddScoped<IClassService, ClassService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

// Tambahkan Controller
builder.Services.AddControllers();
builder.Services.AddAuthorization();

// Tambahkan Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();