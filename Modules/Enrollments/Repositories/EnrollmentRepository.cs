using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Modules.Enrollments.Dtos;
using SchoolManagementSystem.Modules.Enrollments.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolManagementSystem.Configurations.AppDbContext;


namespace SchoolManagementSystem.Modules.Enrollments.Repositories
{
public class EnrollmentRepository : IEnrollmentRepo
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public EnrollmentRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

        public async Task<IEnumerable<EnrollmentDto>> GetAllEnrollmentsAsync()
        {
            var enrollments = await _context.Enrollments.ToListAsync();
            return _mapper.Map<IEnumerable<EnrollmentDto>>(enrollments);
        }

        public async Task<EnrollmentDto> GetEnrollmentByIdAsync(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
                throw new KeyNotFoundException("Enrollment not found");
            return _mapper.Map<EnrollmentDto>(enrollment);
        }

        public async Task<EnrollmentDto> CreateEnrollmentAsync(EnrollmentDto enrollmentDto)
        {
            // Validasi
            if (await _context.Students.AllAsync(s => s.id_student != enrollmentDto.id_student))
                throw new ArgumentException("Invalid student ID");
            if (await _context.Classes.AllAsync(c => c.id_kelas != enrollmentDto.id_kelas))
                throw new ArgumentException("Invalid class ID");

            var enrollment = _mapper.Map<Pendaftaran>(enrollmentDto);
            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();
            return _mapper.Map<EnrollmentDto>(enrollment);
        }

        public async Task<EnrollmentDto> UpdateEnrollmentAsync(int id, EnrollmentDto enrollmentDto)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
                throw new KeyNotFoundException("Enrollment not found");

            // Validasi
            if (await _context.Students.AllAsync(s => s.id_student != enrollmentDto.id_student))
                throw new ArgumentException("Invalid student ID");
            if (await _context.Classes.AllAsync(c => c.id_kelas != enrollmentDto.id_kelas))
                throw new ArgumentException("Invalid class ID");

            _mapper.Map(enrollmentDto, enrollment);
            await _context.SaveChangesAsync();
            return _mapper.Map<EnrollmentDto>(enrollment);
        }

        public async Task<bool> DeleteEnrollmentAsync(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
                return false;

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