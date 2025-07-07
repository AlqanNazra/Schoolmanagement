using SchoolManagementSystem.Modules.Classes.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Modules.Classes.Services
{
    public interface IClassService
    {
        Task<IEnumerable<KelasDto>> GetAllClassesAsync();
        Task<KelasDto> GetClassByIdAsync(int id);
        Task<KelasDto> CreateClassAsync(KelasDto kelasDto);
        Task<KelasDto> UpdateClassAsync(int id, KelasDto kelasDto);
        Task<bool> DeleteClassAsync(int id);
        Task<KelasDto> AssignTeacherAsync(int id_kelas, int id_guru);
        Task<KelasDto> UnassignTeacherAsync(int id_kelas);
    }
}