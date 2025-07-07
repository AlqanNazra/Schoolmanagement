namespace SchoolManagementSystem.Modules.Students.Services // Pindahkan ke namespace Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using SchoolManagementSystem.Common.Requests;
    using SchoolManagementSystem.Common.Responses;

    public interface IStudentService
    {
        Task<PagedResponse<List<StudentDto>>> GetAllStudentsAsync(PaginationParams paginationParams);
        Task<StudentDto?> GetStudentByIdAsync(int id);
        Task<StudentDto> AddStudentAsync(StudentDto studentDto);
        Task<StudentDto?> UpdateStudentAsync(int id, StudentDto studentDto);
        Task<bool> DeleteStudentAsync(int id);
    }
}