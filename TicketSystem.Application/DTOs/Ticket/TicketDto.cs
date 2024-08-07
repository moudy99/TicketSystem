namespace TicketSystem.Application.DTOs.Ticket
{
    public class TicketDto
    {
        public int TicketId { get; set; }
        public string Number { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
    }
}
