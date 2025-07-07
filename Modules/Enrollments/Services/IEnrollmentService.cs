namespace SchoolManagementSystem.Modules.Enrollments.Services
{
    using SchoolManagementSystem.Modules.Enrollments.Dtos;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEnrollmentService
    {
        Task<IEnumerable<EnrollmentDto>> GetAllEnrollmentsAsync();
        Task<EnrollmentDto> GetEnrollmentByIdAsync(int id);
        Task<EnrollmentDto> CreateEnrollmentAsync(EnrollmentDto enrollmentDto);
        Task<EnrollmentDto> UpdateEnrollmentAsync(int id, EnrollmentDto enrollmentDto);
        Task<bool> DeleteEnrollmentAsync(int id);
        Task<IEnumerable<EnrollmentDto>> ExistAsync(int id_student, int id_kelas);
    }
}