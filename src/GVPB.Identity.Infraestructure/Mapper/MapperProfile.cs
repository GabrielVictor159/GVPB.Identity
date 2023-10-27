
using AutoMapper;
using GVPB.Identity.Domain.Models;

namespace GVPB.Identity.Infraestructure.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {

        }
        public void MapDomains()
        {
            CreateMap<User, Database.Entities.User>().ReverseMap();
            CreateMap<RequestUser, Database.Entities.RequestUser>().ReverseMap();
        }
    }
}

