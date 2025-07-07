using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Configurations.AppDbContext;
using SchoolManagementSystem.Modules.Enrollments.Entities;
using SchoolManagementSystem.Modules.Enrollments.Dtos;
using SchoolManagementSystem.Modules.Enrollments.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Modules.Enrollments.Services
{
    public class EnrollmentServices : IEnrollmentRepo
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EnrollmentServices(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EnrollmentDto>> GetAllEnrollmentsAsync()
        {
            var enrollments = await _context.Enrollments
                .Include(e => e.Murid)
                .Include(e => e.kelas)
                .ToListAsync();
            return _mapper.Map<IEnumerable<EnrollmentDto>>(enrollments);
        }

        public async Task<EnrollmentDto> GetEnrollmentByIdAsync(int id)
        {
            var enrollment = await _context.Enrollments
                .Include(e => e.Murid)
                .Include(e => e.kelas)
                .FirstOrDefaultAsync(e => e.id_enrollment == id);
            if (enrollment == null)
            {
                throw new KeyNotFoundException($"Pendaftaran dengan ID {id} tidak ditemukan.");
            }
            return _mapper.Map<EnrollmentDto>(enrollment);
        }

        public async Task<EnrollmentDto> CreateEnrollmentAsync(EnrollmentDto enrollmentDto)
        {
            if (enrollmentDto.id_student <= 0 || enrollmentDto.id_kelas <= 0)
            {
                throw new ArgumentException("ID siswa dan ID kelas wajib diisi dengan nilai yang valid.");
            }

            // Validasi apakah siswa dan kelas ada
            var student = await _context.Students.FindAsync(enrollmentDto.id_student);
            if (student == null)
            {
                throw new KeyNotFoundException($"Siswa dengan ID {enrollmentDto.id_student} tidak ditemukan.");
            }

            var kelas = await _context.Classes.FindAsync(enrollmentDto.id_kelas);
            if (kelas == null)
            {
                throw new KeyNotFoundException($"Kelas dengan ID {enrollmentDto.id_kelas} tidak ditemukan.");
            }

            // Validasi apakah pendaftaran sudah ada
            var existingEnrollment = await _context.Enrollments
                .AnyAsync(e => e.id_student == enrollmentDto.id_student && e.id_kelas == enrollmentDto.id_kelas);
            if (existingEnrollment)
            {
                throw new InvalidOperationException("Siswa sudah terdaftar di kelas ini.");
            }

            var enrollment = _mapper.Map<Pendaftaran>(enrollmentDto);
            enrollment.waktu_pendaftaran = DateTime.UtcNow;
            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();
            return _mapper.Map<EnrollmentDto>(enrollment);
        }

        public async Task<EnrollmentDto> UpdateEnrollmentAsync(int id, EnrollmentDto enrollmentDto)
        {
            var enrollment = await _context.Enrollments
                .Include(e => e.Murid)
                .Include(e => e.kelas)
                .FirstOrDefaultAsync(e => e.id_enrollment == id);
            if (enrollment == null)
            {
                throw new KeyNotFoundException($"Pendaftaran dengan ID {id} tidak ditemukan.");
            }

            if (enrollmentDto.id_student <= 0 || enrollmentDto.id_kelas <= 0)
            {
                throw new ArgumentException("ID siswa dan ID kelas wajib diisi dengan nilai yang valid.");
            }

            // Validasi apakah siswa dan kelas ada
            var student = await _context.Students.FindAsync(enrollmentDto.id_student);
            if (student == null)
            {
                throw new KeyNotFoundException($"Siswa dengan ID {enrollmentDto.id_student} tidak ditemukan.");
            }

            var kelas = await _context.Classes.FindAsync(enrollmentDto.id_kelas);
            if (kelas == null)
            {
                throw new KeyNotFoundException($"Kelas dengan ID {enrollmentDto.id_kelas} tidak ditemukan.");
            }

            _mapper.Map(enrollmentDto, enrollment);
            await _context.SaveChangesAsync();
            return _mapper.Map<EnrollmentDto>(enrollment);
        }

        public async Task<bool> DeleteEnrollmentAsync(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return false;
            }

            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<EnrollmentDto>> ExistAsync(int id_student, int id_kelas)
        {
            var enrollments = await _context.Enrollments
                .Where(e => e.id_student == id_student && e.id_kelas == id_kelas)
                .ToListAsync();
            return _mapper.Map<IEnumerable<EnrollmentDto>>(enrollments);
        }
    }
}