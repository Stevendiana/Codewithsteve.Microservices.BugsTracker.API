using AutoMapper;
using Codewithsteve.Microservices.BugsTracker.API.Controllers.Resources;
using Codewithsteve.Microservices.BugsTracker.Models;
using System.Linq;

namespace Codewithsteve.Microservices.BugsTracker.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to API Resource
            CreateMap<Bug, BugViewModel>()
            .ForMember(dto => dto.BugId, map => map.MapFrom(src => src.BugId))
            .ForMember(dto => dto.ClientName, opt => opt.MapFrom(src => src.Client.Name));


            CreateMap<Client, ClientViewModel>()
             .ForMember(dto => dto.ClientId, map => map.MapFrom(src => src.ClientId))
             .ForMember(dto => dto.TotalBugs, opt => opt.MapFrom(src => src.Bugs.Count()));



            // API Resource to Domain
            CreateMap<BugData, Bug>();
            CreateMap<ClientData, Client>();




        }
    }
}
