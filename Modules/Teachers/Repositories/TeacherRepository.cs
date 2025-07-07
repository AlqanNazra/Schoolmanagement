using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Configurations.AppDbContext;
using SchoolManagementSystem.Modules.Teachers.Entities;
using SchoolManagementSystem.Modules.Teachers.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolManagementSystem.Common.Requests;
using SchoolManagementSystem.Common.Responses;

namespace SchoolManagementSystem.Modules.Teachers.Repositories
{
    public class TeacherRepository : ITeacherRepo
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TeacherRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedResponse<List<TeacherDto>>> GetAllTeachersAsync(PaginationParams paginationParams)
        {
            var queary = _context.Teachers.AsQueryable();
            var totalCount = await queary.CountAsync();

            var teachers = await queary
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize)
                .ToListAsync();

            var teacherDtos = _mapper.Map<List<TeacherDto>>(teachers);
            return new PagedResponse<List<TeacherDto>>(teacherDtos, paginationParams.PageNumber, paginationParams.PageSize, totalCount);
        }

        public async Task<TeacherDto> GetTeacherByIdAsync(int id)
        {
            var teacher = await _context.Teachers
                .FirstOrDefaultAsync(t => t.id_teacher == id);
            if (teacher == null)
            {
                throw new KeyNotFoundException($"Guru dengan ID {id} tidak ditemukan.");
            }
            return _mapper.Map<TeacherDto>(teacher);
        }

        public async Task<TeacherDto> CreateTeacherAsync(TeacherDto teacherDto)
        {
            if (string.IsNullOrWhiteSpace(teacherDto.nama_teacher) || string.IsNullOrWhiteSpace(teacherDto.email_teacher))
            {
                throw new ArgumentException("Nama dan email guru wajib diisi.");
            }

            var teacher = _mapper.Map<Guru>(teacherDto);
            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();
            return _mapper.Map<TeacherDto>(teacher);
        }

        public async Task<TeacherDto> UpdateTeacherAsync(int id, TeacherDto teacherDto)
        {
            var teacher = await _context.Teachers
                .FirstOrDefaultAsync(t => t.id_teacher == id);
            if (teacher == null)
            {
                throw new KeyNotFoundException($"Guru dengan ID {id} tidak ditemukan.");
            }

            if (string.IsNullOrWhiteSpace(teacherDto.nama_teacher) || string.IsNullOrWhiteSpace(teacherDto.email_teacher))
            {
                throw new ArgumentException("Nama dan email guru wajib diisi.");
            }

            _mapper.Map(teacherDto, teacher);
            await _context.SaveChangesAsync();
            return _mapper.Map<TeacherDto>(teacher);
        }

        public async Task<bool> DeleteTeacherAsync(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return false;
            }

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}