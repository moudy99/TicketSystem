using AutoMapper;
using TicketSystem.Application.DTOs.Admin;
using TicketSystem.Application.DTOs.Ticket;
using TicketSystem.Core.Entities;

namespace TicketSystem.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, RegisterAdminDto>().ReverseMap();

            CreateMap<CreateTicketDTO, User>()
          .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UserName))
          .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.UserPhoneNumber));


            CreateMap<Ticket, TicketDto>().ReverseMap();
        }
    }
}
