using SchoolManagementSystem.Modules.Classes.Dtos;
using SchoolManagementSystem.Modules.Classes.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Modules.Classes.Services
{
    public class ClassService : IClassService
    {
        private readonly IClassRepository _classRepository;

        public ClassService(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        public async Task<IEnumerable<KelasDto>> GetAllClassesAsync()
        {
            return await _classRepository.GetAllClassesAsync();
        }

        public async Task<KelasDto> GetClassByIdAsync(int id)
        {
            return await _classRepository.GetClassByIdAsync(id);
        }

        public async Task<KelasDto> CreateClassAsync(KelasDto kelasDto)
        {
            if (string.IsNullOrWhiteSpace(kelasDto.nama_kelas))
            {
                throw new ArgumentException("Nama kelas wajib diisi.");
            }

            return await _classRepository.CreateClassAsync(kelasDto);
        }

        public async Task<KelasDto> UpdateClassAsync(int id, KelasDto kelasDto)
        {
            if (string.IsNullOrWhiteSpace(kelasDto.nama_kelas))
            {
                throw new ArgumentException("Nama kelas wajib diisi.");
            }

            return await _classRepository.UpdateClassAsync(id, kelasDto);
        }

        public async Task<bool> DeleteClassAsync(int id)
        {
            return await _classRepository.DeleteClassAsync(id);
        }

        public async Task<KelasDto> AssignTeacherAsync(int id_kelas, int id_guru)
        {
            // Validasi apakah guru sudah ditugaskan ke kelas lain
            var existingClass = await _classRepository.GetAllClassesAsync();
            if (existingClass.Any(c => c.id_guru == id_guru && c.id_kelas != id_kelas))
            {
                throw new InvalidOperationException("Guru sudah ditugaskan ke kelas lain.");
            }

            return await _classRepository.AssignTeacherAsync(id_kelas, id_guru);
        }

        public async Task<KelasDto> UnassignTeacherAsync(int id_kelas)
        {
            return await _classRepository.UnassignTeacherAsync(id_kelas);
        }
    }
}