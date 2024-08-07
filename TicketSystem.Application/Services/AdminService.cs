using AutoMapper;
using TicketSystem.Application.DTOs.Admin;
using TicketSystem.Application.GeneralResponse;
using TicketSystem.Application.Interfaces.Services;
using TicketSystem.Application.Interfaces.UnitOfWork;
using TicketSystem.Core.Entities;

namespace TicketSystem.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AdminService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<GeneralResponse<AuthResponseDTO>> LoginAdminAsync(LoginAdminDto loginAdminDto)
        {
            var result = await unitOfWork.adminRepository.AdminLogin(loginAdminDto);
            if (result.Succeeded)
            {
                return new GeneralResponse<AuthResponseDTO>()
                {
                    Data = result,
                    Message = result.Message,
                    Succeeded = true
                };
            }
            else
            {
                return new GeneralResponse<AuthResponseDTO>()
                {
                    Message = result.Message,
                    Succeeded = false
                };
            }
        }

        public async Task<GeneralResponse<AuthResponseDTO>> RegisterAdminAsync(RegisterAdminDto registerAdminDto)
        {
            ApplicationUser user = mapper.Map<ApplicationUser>(registerAdminDto);
            var result = await unitOfWork.adminRepository.AdminRegister(user, registerAdminDto.Password);
            if (result.Succeeded)
            {
                return new GeneralResponse<AuthResponseDTO>()
                {
                    Data = result,
                    Message = "Admin registered successfully",
                    Succeeded = true
                };
            }
            else
            {
                return new GeneralResponse<AuthResponseDTO>()
                {
                    Message = result.Message,
                    Succeeded = false
                };
            }
        }
    }
}
