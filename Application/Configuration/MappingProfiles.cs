using AutoMapper;
using Domain;

namespace Application
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ContactUpdateCreateDto, Contact>();

        }
    }
}
