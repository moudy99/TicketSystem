namespace TicketSystem.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public Ticket? Ticket { get; set; }
    }
}
