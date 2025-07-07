using AutoMapper;
using SchoolManagementSystem.Modules.Students.Entities;

namespace SchoolManagementSystem.Modules.Students.Mappers
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Murid, StudentDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id_student))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.nama))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.email))
                .ForMember(dest => dest.waktu_registrasi, opt => opt.MapFrom(src => src.waktu_registrasi))
                .ForMember(dest => dest.Enrollments, opt => opt.MapFrom(src => src.Enrollments));

            CreateMap<StudentDto, Murid>()
                .ForMember(dest => dest.id_student, opt => opt.Ignore()) // Ignore id_student karena dihasilkan otomatis
                .ForMember(dest => dest.nama, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.waktu_registrasi, opt => opt.Ignore()) // Ignore waktu_registrasi karena diatur di service
                .ForMember(dest => dest.Enrollments, opt => opt.MapFrom(src => src.Enrollments));
        }
    }
}