using AutoMapper;
using Routine.Api.Entities;
using Routine.Api.Models;

namespace Routine.Api.Profiles
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            // 创建映射，从company映射到companydto，默认是键名相同的映射
            // 如果键名不一样，则手动映射
            CreateMap<Company, CompanyDto>()
                .ForMember(dest => dest.CompanyName,
                    opt => opt.MapFrom(src=>src.Name));

            CreateMap<CompanyAddDto, Company>();
        }
    }
}
