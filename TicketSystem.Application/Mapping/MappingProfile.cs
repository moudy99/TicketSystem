using AutoMapper;
using TicketSystem.Application.DTOs.Admin;
using TicketSystem.Core.Entities;

namespace TicketSystem.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, RegisterAdminDto>().ReverseMap();
        }
    }
}
