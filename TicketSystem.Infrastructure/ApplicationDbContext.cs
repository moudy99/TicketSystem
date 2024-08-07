using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicketSystem.Core.Entities;

namespace TicketSystem.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<Ticket> tickets { get; set; }
        public DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);



            var users = new User[]
       {
    new User
    {
        Id = 1, Name = "user1", PhoneNumber = "01204016981"

    },
    new User
    {
        Id = 2, Name = "user2", PhoneNumber = "01204016982"

    },
    new User
    {
        Id = 3, Name = "user3", PhoneNumber = "01204016983"

    },
    new User
    {
        Id = 4, Name = "user4", PhoneNumber = "01204016984"

    },
    new User
    {
        Id = 5, Name = "user5", PhoneNumber = "01204016985"
    },
    new User
    {
        Id = 6, Name = "user6", PhoneNumber = "01204016986"
    },
    new User
    {
        Id = 7, Name = "user7", PhoneNumber = "01204016987"
    },
    new User
    {
        Id = 8, Name = "user8", PhoneNumber = "01204016988"

    },
    new User
    {
        Id = 9, Name = "user9", PhoneNumber = "01204016989"

    },
    new User
    {
        Id = 10, Name = "user10", PhoneNumber = "01204016910"

    }
       };
            var tickets = new Ticket[]
            {
    new Ticket { TicketId = 1, Number = "TICKET1", ImagePath = "/Images/Tickets/ticket.png", CreatedAt = DateTime.Now.AddMinutes(-10), UserId = 1 },
    new Ticket { TicketId = 2, Number = "TICKET2", ImagePath = "/Images/Tickets/ticket.png", CreatedAt = DateTime.Now.AddMinutes(-9), UserId = 2 },
    new Ticket { TicketId = 3, Number = "TICKET3", ImagePath = "/Images/Tickets/ticket.png", CreatedAt = DateTime.Now.AddMinutes(-8), UserId = 3 },
    new Ticket { TicketId = 4, Number = "TICKET4", ImagePath = "/Images/Tickets/ticket.png", CreatedAt = DateTime.Now.AddMinutes(-7), UserId = 4 },
    new Ticket { TicketId = 5, Number = "TICKET5", ImagePath = "/Images/Tickets/ticket.png", CreatedAt = DateTime.Now.AddMinutes(-6), UserId = 5 },
    new Ticket { TicketId = 6, Number = "TICKET6", ImagePath = "/Images/Tickets/ticket.png", CreatedAt = DateTime.Now.AddMinutes(-5), UserId = 6 },
    new Ticket { TicketId = 7, Number = "TICKET7", ImagePath = "/Images/Tickets/ticket.png", CreatedAt = DateTime.Now.AddMinutes(-4), UserId = 7 },
    new Ticket { TicketId = 8, Number = "TICKET8", ImagePath = "/Images/Tickets/ticket.png", CreatedAt = DateTime.Now.AddMinutes(-3), UserId = 8 },
    new Ticket { TicketId = 9, Number = "TICKET9", ImagePath = "/Images/Tickets/ticket.png", CreatedAt = DateTime.Now.AddMinutes(-2), UserId = 9 },
    new Ticket { TicketId = 10, Number = "TICKET10", ImagePath = "/Images/Tickets/ticket.png", CreatedAt = DateTime.Now.AddMinutes(-1), UserId = 10 }
            };


            builder.Entity<User>().HasData(users);
            builder.Entity<Ticket>().HasData(tickets);
        }
    }
}
