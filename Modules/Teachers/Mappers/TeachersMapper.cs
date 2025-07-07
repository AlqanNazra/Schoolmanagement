using AutoMapper;
using System.Linq;
using SchoolManagementSystem.Modules.Teachers.Dtos;
using SchoolManagementSystem.Modules.Teachers.Entities;

namespace SchoolManagementSystem.Modules.Teachers.Mappers
{
    public class TeachersProfile : Profile
    {
        public TeachersProfile()
        {
            // Entity -> DTO
            CreateMap<Guru, TeacherDto>()
                .ForMember(dest => dest.kelasUtamaIds, opt => opt.MapFrom(src => src.KelasUtama.Select(k => k.id_kelas)))
                .ForMember(dest => dest.kelasDiajarkanIds, opt => opt.MapFrom(src => src.KelasDiajarkan.Select(k => k.id_kelas)));

            // DTO -> Entity
            CreateMap<TeacherDto, Guru>()
                .ForMember(dest => dest.KelasUtama, opt => opt.Ignore())
                .ForMember(dest => dest.KelasDiajarkan, opt => opt.Ignore());
        }
    }
}
