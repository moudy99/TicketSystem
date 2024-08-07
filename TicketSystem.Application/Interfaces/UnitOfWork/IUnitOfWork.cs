using TicketSystem.Application.Interfaces.Repository;

namespace TicketSystem.Application.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IAdminRepository adminRepository { get; }

        Task<int> SaveChangesAsync();
    }
}
