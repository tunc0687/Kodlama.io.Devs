using AutoMapper;
using Core.Persistence.Paging;
using Kodlama.io.Devs.Application.Features.Technologies.Commands.CreateTechnology;
using Kodlama.io.Devs.Application.Features.Technologies.Commands.UpdateTechnology;
using Kodlama.io.Devs.Application.Features.Technologies.Dtos;
using Kodlama.io.Devs.Application.Features.Technologies.Models;
using Kodlama.io.Devs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Technologies.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Technology, CreatedTechnologyDto>().ReverseMap();
            CreateMap<Technology, CreateTechnologyCommand>().ReverseMap();

            CreateMap<Technology, DeletedTechnologyDto>().ReverseMap();

            CreateMap<Technology, UpdatedTechnologyDto>().ReverseMap();
            CreateMap<Technology, UpdateTechnologyCommand>().ReverseMap();

            CreateMap<Technology, TechnologyGetByIdDto>().ReverseMap();

            CreateMap<Technology, TechnologyGetListDto>()
                .ForMember(dest => dest.ProgrammingLanguageName, opt => opt.MapFrom(src => src.ProgrammingLanguage.Name))
                .ReverseMap();
            CreateMap<IPaginate<Technology>, TechnologyListModel>().ReverseMap();
        }
    }
}
