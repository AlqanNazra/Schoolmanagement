namespace SchoolManagementSystem.Modules.Teachers.Repositories
{
    using SchoolManagementSystem.Modules.Teachers.Dtos;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITeacherRepo
    {
        Task<IEnumerable<TeacherDto>> GetAllTeachersAsync();
        Task<TeacherDto> GetTeacherByIdAsync(int id);
        Task<TeacherDto> CreateTeacherAsync(TeacherDto teacherDto);
        Task<TeacherDto> UpdateTeacherAsync(int id, TeacherDto teacherDto);
        Task<bool> DeleteTeacherAsync(int id);
    }
}