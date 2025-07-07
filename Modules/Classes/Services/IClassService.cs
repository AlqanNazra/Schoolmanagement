using SchoolManagementSystem.Modules.Classes.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolManagementSystem.Common.Requests;
using SchoolManagementSystem.Common.Responses;

namespace SchoolManagementSystem.Modules.Classes.Services
{
    public interface IClassService
    {
        Task<PagedResponse<List<KelasDto>>> GetAllClassesAsync(PaginationParams paginationParams);
        Task<KelasDto> GetClassByIdAsync(int id);
        Task<KelasDto> CreateClassAsync(KelasDto kelasDto);
        Task<KelasDto> UpdateClassAsync(int id, KelasDto kelasDto);
        Task<bool> DeleteClassAsync(int id);
        Task<KelasDto> AssignTeacherAsync(int id_kelas, int id_guru);
        Task<KelasDto> UnassignTeacherAsync(int id_kelas);
    }
}