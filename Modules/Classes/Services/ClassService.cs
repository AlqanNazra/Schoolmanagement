using SchoolManagementSystem.Modules.Classes.Dtos;
using SchoolManagementSystem.Modules.Classes.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolManagementSystem.Common.Requests;
using SchoolManagementSystem.Common.Responses;
using SchoolManagementSystem.Configurations.AppDbContext;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagementSystem.Modules.Classes.Services
{
    public class ClassService : IClassService
    {
        private readonly AppDbContext _db;
        private readonly IClassRepository _classRepository;
        public ClassService(AppDbContext db, IClassRepository classRepository)
        {
            _db = db;
            _classRepository = classRepository;
        }


        public async Task<PagedResponse<List<KelasDto>>> GetAllClassesAsync(PaginationParams paginationParams)
        {
            var query = _db.Classes.AsQueryable();
            var totalCount = await query.CountAsync();

            var classes = await query
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize)
                .ToListAsync();

            var classDtos = classes.Select(c => new KelasDto
            {
                nama_kelas = c.nama_kelas,
                id_kelas = c.id_kelas,
                id_guru = c.id_guru,
                pengajarIds = c.Pengajar?.Select(p => p.id_teacher).ToList() ?? new List<int>()
            }).ToList();

            return new PagedResponse<List<KelasDto>>(classDtos, paginationParams.PageNumber, paginationParams.PageSize, totalCount);
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