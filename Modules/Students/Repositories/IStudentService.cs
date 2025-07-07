namespace SchoolManagementSystem.Modules.Students.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IStudentService
    {
        Task<IEnumerable<StudentDto>> GetAllStudentsAsync();
        Task<StudentDto?> GetStudentByIdAsync(int id); // Ubah ke StudentDto?
        Task<StudentDto> AddStudentAsync(StudentDto studentDto);
        Task<StudentDto?> UpdateStudentAsync(int id, StudentDto studentDto); // Ubah ke StudentDto?
        Task<bool> DeleteStudentAsync(int id);
    }
}