using Microsoft.AspNetCore.Http;

namespace TicketSystem.Application.DTOs.Ticket
{
    public class CreateTicketDTO
    {
        public string UserName { get; set; }
        public string UserPhoneNumber { get; set; }
        public IFormFile? TicketImageFile { get; set; }
    }
}
