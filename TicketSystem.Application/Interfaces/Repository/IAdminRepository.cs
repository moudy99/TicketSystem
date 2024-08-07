using TicketSystem.Application.DTOs.Admin;
using TicketSystem.Core.Entities;

namespace TicketSystem.Application.Interfaces.Repository
{
    public interface IAdminRepository : IBaseRepository<ApplicationUser>
    {
        public Task<AuthResponseDTO> AdminRegister(ApplicationUser user, string password);
        public Task<AuthResponseDTO> AdminLogin(LoginAdminDto LoginAdminDto);
    }
}
