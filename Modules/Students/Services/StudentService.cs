using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Modules.Students.Entities;
using SchoolManagementSystem.Configurations.AppDbContext;
using SchoolManagementSystem.Modules.Enrollments.Entities;
using SchoolManagementSystem.Modules.Classes.Entities;
using SchoolManagementSystem.Common.Requests;
using SchoolManagementSystem.Common.Responses;


namespace SchoolManagementSystem.Modules.Students.Services
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public StudentService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<PagedResponse<List<StudentDto>>> GetAllStudentsAsync(PaginationParams paginationParams)
        {
            var totalCount = await _db.Students.CountAsync();

            var studentDtos = await _db.Students
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize)
                .Select(s => _mapper.Map<StudentDto>(s))
                .ToListAsync();

            return new PagedResponse<List<StudentDto>>(studentDtos, paginationParams.PageNumber, paginationParams.PageSize, totalCount);
        }

        public async Task<StudentDto?> GetStudentByIdAsync(int id)
        {
            var student = await _db.Students
                .Include(m => m.Enrollments)
                .FirstOrDefaultAsync(m => m.id_student == id);

            if (student == null) return null;

            return _mapper.Map<StudentDto>(student);
        }

        public async Task<StudentDto> AddStudentAsync(StudentDto dto)
        {
            var student = new Murid
            {
                nama = dto.Name,
                email = dto.Email,
                waktu_registrasi = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
            };

            if (dto.KelasIds != null && dto.KelasIds.Any())
            {
                foreach (var kelasId in dto.KelasIds)
                {
                    var kelas = await _db.Classes.FindAsync(kelasId);
                    if (kelas != null)
                    {
                        student.Enrollments.Add(new Pendaftaran
                        {
                            id_kelas = kelasId,
                            waktu_pendaftaran = DateTime.UtcNow
                        });
                    }
                }
            }

            _db.Students.Add(student);
            await _db.SaveChangesAsync();

            return _mapper.Map<StudentDto>(student);
        }

        public async Task<StudentDto?> UpdateStudentAsync(int id, StudentDto dto)
        {
            var student = await _db.Students
                .Include(m => m.Enrollments)
                .FirstOrDefaultAsync(m => m.id_student == id);

            if (student == null) return null;

            student.nama = dto.Name;
            student.email = dto.Email;

            if (dto.KelasIds != null)
            {
                student.Enrollments.Clear();

                foreach (var kelasId in dto.KelasIds)
                {
                    student.Enrollments.Add(new Pendaftaran
                    {
                        id_kelas = kelasId,
                        waktu_pendaftaran = DateTime.UtcNow,
                    });
                }
            }

            await _db.SaveChangesAsync();

            return _mapper.Map<StudentDto>(student);
        }


        public async Task<bool> DeleteStudentAsync(int id)
        {
            var student = await _db.Students.FindAsync(id);
            if (student == null) return false;

            _db.Students.Remove(student);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}