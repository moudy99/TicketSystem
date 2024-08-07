using TicketSystem.Application.Interfaces.Repository;

namespace TicketSystem.Application.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IAdminRepository adminRepository { get; }
        public IUserRepository userRepository { get; }

        public ITicketRepository ticketRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
