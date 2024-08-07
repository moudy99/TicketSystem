using TicketSystem.Application.Interfaces.Repository;
using TicketSystem.Core.Entities;

namespace TicketSystem.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
