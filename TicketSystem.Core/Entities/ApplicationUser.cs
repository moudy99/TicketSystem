using Microsoft.AspNetCore.Identity;
using TicketSystem.Core.Enums;

namespace TicketSystem.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public UserRole Role { get; set; }
    }
}
