using AutoMapper;
using SchoolManagementSystem.Modules.Teachers.Dtos;
using SchoolManagementSystem.Modules.Teachers.Entities;
using SchoolManagementSystem.Modules.Teachers.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Modules.Teachers.Services
{
    public class TeacherService : ITeacherRepo
    {
        private readonly ITeacherRepo _teacherRepo;
        private readonly IMapper _mapper;

        public TeacherService(ITeacherRepo teacherRepo, IMapper mapper)
        {
            _teacherRepo = teacherRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TeacherDto>> GetAllTeachersAsync()
        {
            return await _teacherRepo.GetAllTeachersAsync();
        }

        public async Task<TeacherDto> GetTeacherByIdAsync(int id)
        {
            var teacher = await _teacherRepo.GetTeacherByIdAsync(id);
            if (teacher == null)
            {
                throw new KeyNotFoundException($"Teacher with ID {id} not found.");
            }
            return teacher;
        }

        public async Task<TeacherDto> CreateTeacherAsync(TeacherDto teacherDto)
        {
            if (string.IsNullOrWhiteSpace(teacherDto.nama_teacher) || 
                string.IsNullOrWhiteSpace(teacherDto.email_teacher))
            {
                throw new ArgumentException("Teacher name and email are required.");
            }

            return await _teacherRepo.CreateTeacherAsync(teacherDto);
        }

        public async Task<TeacherDto> UpdateTeacherAsync(int id, TeacherDto teacherDto)
        {
            var existingTeacher = await _teacherRepo.GetTeacherByIdAsync(id);
            if (existingTeacher == null)
            {
                throw new KeyNotFoundException($"Teacher with ID {id} not found.");
            }

            teacherDto.id_teacher = id;
            return await _teacherRepo.UpdateTeacherAsync(id, teacherDto);
        }

        public async Task<bool> DeleteTeacherAsync(int id)
        {
            var existingTeacher = await _teacherRepo.GetTeacherByIdAsync(id);
            if (existingTeacher == null)
            {
                return false;
            }

            return await _teacherRepo.DeleteTeacherAsync(id);
        }
    }
}