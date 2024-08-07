using TicketSystem.Application.DTOs.Admin;
using TicketSystem.Application.GeneralResponse;

namespace TicketSystem.Application.Interfaces.Services
{
    public interface IAdminService
    {
        Task<GeneralResponse<AuthResponseDTO>> RegisterAdminAsync(RegisterAdminDto registerAdminDto);
        Task<GeneralResponse<AuthResponseDTO>> LoginAdminAsync(LoginAdminDto loginAdminDto);
    }
}
