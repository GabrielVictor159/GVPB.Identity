
using AutoMapper;
using GVPB.Identity.Domain.Models;

namespace GVPB.Identity.Infraestructure.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            MapDomains();
        }
        public void MapDomains()
        {
            CreateMap<User, Database.Entities.User>().ReverseMap();
            CreateMap<RequestUser, Database.Entities.RequestUser>().ReverseMap();
            CreateMap<Log, Database.Entities.Log>().ReverseMap();
        }
    }
}

