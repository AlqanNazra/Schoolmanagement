namespace SchoolManagementSystem.Modules.Teachers.Repositories
{
    using SchoolManagementSystem.Modules.Teachers.Dtos;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using SchoolManagementSystem.Common.Requests;
    using SchoolManagementSystem.Common.Responses;

    public interface ITeacherRepo
    {
        Task<PagedResponse<List<TeacherDto>>> GetAllTeachersAsync(PaginationParams paginationParams);
        Task<TeacherDto> GetTeacherByIdAsync(int id);
        Task<TeacherDto> CreateTeacherAsync(TeacherDto teacherDto);
        Task<TeacherDto> UpdateTeacherAsync(int id, TeacherDto teacherDto);
        Task<bool> DeleteTeacherAsync(int id);
    }
}