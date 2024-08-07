using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using TicketSystem.Application.Configurations;
using TicketSystem.Application.Interfaces.Repository;
using TicketSystem.Application.Interfaces.UnitOfWork;
using TicketSystem.Core.Entities;
using TicketSystem.Infrastructure.Repositories;

namespace TicketSystem.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IOptions<JWT> jWT;
        private readonly IConfiguration _configuration;
        public IAdminRepository adminRepository { get; }
        public UnitOfWork(ApplicationDbContext context, IConfiguration configuration, IOptions<JWT> JWT, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.userManager = userManager;

            this.adminRepository = new AdminRepository(context, configuration, JWT, userManager);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }


    }
}
