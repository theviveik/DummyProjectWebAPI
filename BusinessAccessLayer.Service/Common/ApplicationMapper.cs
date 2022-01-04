using AutoMapper;
using BusinessAccessLayer.Models;
using DataAccessLayer.DataModel;

namespace BusinessAccessLayer.Service
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Users, UserModel>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
            .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId));
        }

    }
}