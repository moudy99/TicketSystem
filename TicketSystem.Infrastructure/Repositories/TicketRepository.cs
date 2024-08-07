using TicketSystem.Application.Interfaces.Repository;
using TicketSystem.Core.Entities;

namespace TicketSystem.Infrastructure.Repositories
{
    public class TicketRepository : BaseRepository<Ticket>, ITicketRepository
    {
        private readonly ApplicationDbContext context;

        public TicketRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
