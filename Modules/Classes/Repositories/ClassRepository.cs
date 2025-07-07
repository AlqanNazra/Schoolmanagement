using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Configurations.AppDbContext;
using SchoolManagementSystem.Modules.Classes.Dtos;
using SchoolManagementSystem.Modules.Classes.Entities;
using SchoolManagementSystem.Modules.Classes.Repositories;
using SchoolManagementSystem.Modules.Teachers.Entities;
using SchoolManagementSystem.Modules.Enrollments.Entities;
using SchoolManagementSystem.Modules.Classes.Mappers   ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Modules.Classes.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ClassRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<KelasDto>> GetAllClassesAsync()
        {
            var classes = await _context.Classes
                .Include(k => k.Pengajar)

                .Include(k => k.Enrollments)
                .ToListAsync();
            return _mapper.Map<IEnumerable<KelasDto>>(classes);
        }

        public async Task<KelasDto> GetClassByIdAsync(int id)
        {
            var kelas = await _context.Classes
                .Include(k => k.Pengajar)

                .Include(k => k.Enrollments)
                .FirstOrDefaultAsync(k => k.id_kelas == id);
            if (kelas == null)
            {
                throw new KeyNotFoundException($"Kelas dengan ID {id} tidak ditemukan.");
            }
            return _mapper.Map<KelasDto>(kelas);
        }

            public async Task<KelasDto> CreateClassAsync(KelasDto kelasDto)
                {
                    if (string.IsNullOrWhiteSpace(kelasDto.nama_kelas))
                        throw new ArgumentException("Nama kelas wajib diisi.");

                    if (!kelasDto.id_guru.HasValue)
                        throw new ArgumentException("Guru utama wajib diisi.");

                    var guruUtama = await _context.Teachers.FindAsync(kelasDto.id_guru.Value);
                    if (guruUtama == null)
                        throw new ArgumentException("Guru utama tidak ditemukan.");

                    var kelas = new Kelas
                    {
                        nama_kelas = kelasDto.nama_kelas!,
                        id_guru = kelasDto.id_guru,
                        GuruUtama = guruUtama
                    };

                    // Map pengajarIds
                    if (kelasDto.pengajarIds != null && kelasDto.pengajarIds.Any())
                    {
                        var guruList = await _context.Teachers
                            .Where(g => kelasDto.pengajarIds.Contains(g.id_teacher))
                            .ToListAsync();

                        kelas.Pengajar = guruList;
                    }

                    _context.Classes.Add(kelas);
                    await _context.SaveChangesAsync();

                    return _mapper.Map<KelasDto>(kelas);
                }



        public async Task<KelasDto> UpdateClassAsync(int id, KelasDto kelasDto)
        {
            var kelas = await _context.Classes
                .Include(c => c.Pengajar)
                .FirstOrDefaultAsync(c => c.id_kelas == id);
            if (kelas == null)
                throw new KeyNotFoundException("Class not found");

            _mapper.Map(kelasDto, kelas);

            // Penanganan relasi many-to-many (Pengajar)
            kelas.Pengajar.Clear();
            var pengajar = await _context.Teachers
                .Where(g => kelasDto.pengajarIds.Contains(g.id_teacher))
                .ToListAsync();
            foreach (var guru in pengajar)
            {
                kelas.Pengajar.Add(guru);
            }

            await _context.SaveChangesAsync();
            return _mapper.Map<KelasDto>(kelas);
        }

        public async Task<bool> DeleteClassAsync(int id)
        {
            var kelas = await _context.Classes.FindAsync(id);
            if (kelas == null)
            {
                return false;
            }

            _context.Classes.Remove(kelas);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<KelasDto> AssignTeacherAsync(int id_kelas, int id_guru)
        {
            var kelas = await _context.Classes
                .Include(k => k.Pengajar)

                .FirstOrDefaultAsync(k => k.id_kelas == id_kelas);
            if (kelas == null)
            {
                throw new KeyNotFoundException($"Kelas dengan ID {id_kelas} tidak ditemukan.");
            }

            var guru = await _context.Teachers.FindAsync(id_guru);
            if (guru == null)
            {
                throw new KeyNotFoundException($"Guru dengan ID {id_guru} tidak ditemukan.");
            }

            kelas.id_guru = id_guru;
            kelas.Pengajar.Clear();
            kelas.Pengajar.Add(guru);
            await _context.SaveChangesAsync();
            return _mapper.Map<KelasDto>(kelas);
        }

        public async Task<KelasDto> UnassignTeacherAsync(int id_kelas)
        {
            var kelas = await _context.Classes
                .Include(k => k.Pengajar)
                .FirstOrDefaultAsync(k => k.id_kelas == id_kelas);
            if (kelas == null)
            {
                throw new KeyNotFoundException($"Kelas dengan ID {id_kelas} tidak ditemukan.");
            }

            kelas.id_guru = null;
            kelas.Pengajar.Clear();
            await _context.SaveChangesAsync();
            return _mapper.Map<KelasDto>(kelas);
        }
    }
}