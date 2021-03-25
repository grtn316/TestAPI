using AutoMapper;
using DC = TestAPI.API.DataContracts;
using S = TestAPI.Services.Model;

namespace TestAPI.IoC.Configuration.AutoMapper.Profiles
{
    public class APIMappingProfile : Profile
    {
        public APIMappingProfile()
        {
            CreateMap<DC.User, S.User>().ReverseMap();
            CreateMap<DC.Address, S.Address>().ReverseMap();
        }
    }
}
