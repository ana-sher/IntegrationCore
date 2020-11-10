using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IntegrationCore.Models.DB;
using IntegrationCore.Models.DTO;

namespace IntegrationCore.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SystemDefinition, SystemDefinitionDto>().ReverseMap();
            CreateMap<FieldDefinition, FieldDefinitionDto>().ReverseMap();
            CreateMap<Integration, IntegrationDto>().ReverseMap();
            CreateMap<TypeDefinition, IntegrationTypeDefinitionDto>().ReverseMap();
            CreateMap<TypeDefinition, TypeDefinitionDto>().ReverseMap();
            CreateMap<ConnectionFieldDefinition, ConnectionFieldDefinitionDto>().ReverseMap();
            CreateMap<FieldConnection, FlatFieldConnectionDto>().ReverseMap();
        }
    }
}
