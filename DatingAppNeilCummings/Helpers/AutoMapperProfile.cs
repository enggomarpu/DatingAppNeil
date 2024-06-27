using AutoMapper;
using DatingAppNeilCummings.DTOs;
using DatingAppNeilCummings.Entities;

namespace DatingAppNeilCummings.Helpers
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AppUser, MemberDto>();
			CreateMap<Photo, PhotoDto>();
		}
    }
}