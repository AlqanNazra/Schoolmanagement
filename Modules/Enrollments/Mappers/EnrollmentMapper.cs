using AutoMapper;
using SchoolManagementSystem.Modules.Students.Entities;
using SchoolManagementSystem.Modules.Classes.Entities;
using SchoolManagementSystem.Modules.Enrollments.Entities;
using SchoolManagementSystem.Modules.Enrollments.Dtos;

namespace SchoolManagementSystem.Modules.Enrollments.Mappers
{
    public class EnrollmentProfile : Profile
    {
        public EnrollmentProfile()
        {
            CreateMap<Pendaftaran, EnrollmentDto>()
                .ForMember(dest => dest.id_enrollment, opt => opt.MapFrom(src => src.id_enrollment))
                .ForMember(dest => dest.id_student, opt => opt.MapFrom(src => src.id_student))
                .ForMember(dest => dest.id_kelas, opt => opt.MapFrom(src => src.id_kelas));


            CreateMap<EnrollmentDto, Pendaftaran>()
                .ForMember(dest => dest.id_enrollment, opt => opt.Ignore()) // Ignore id_enrollment karena dihasilkan otomatis
                .ForMember(dest => dest.id_student, opt => opt.MapFrom(src => src.id_student))
                .ForMember(dest => dest.id_kelas, opt => opt.MapFrom(src => src.id_kelas));
        }
    }
}