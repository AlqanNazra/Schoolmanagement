using AutoMapper;
using SchoolManagementSystem.Modules.Classes.Dtos;
using SchoolManagementSystem.Modules.Classes.Entities;

namespace SchoolManagementSystem.Modules.Classes.Mappers
{
    public class ClassProfile : Profile
    {
        public ClassProfile()
        {
            CreateMap<Kelas, KelasDto>()
                .ForMember(dest => dest.id_kelas, opt => opt.MapFrom(src => src.id_kelas))
                .ForMember(dest => dest.nama_kelas, opt => opt.MapFrom(src => src.nama_kelas))
                .ForMember(dest => dest.id_guru, opt => opt.MapFrom(src => src.id_guru));


            CreateMap<KelasDto, Kelas>()
                .ForMember(dest => dest.id_kelas, opt => opt.Ignore())
                .ForMember(dest => dest.nama_kelas, opt => opt.MapFrom(src => src.nama_kelas))
                .ForMember(dest => dest.id_guru, opt => opt.MapFrom(src => src.id_guru))
                .ForMember(dest => dest.Pengajar, opt => opt.Ignore()); // pengajar tetap harus handle manual  
        }
    }
}
