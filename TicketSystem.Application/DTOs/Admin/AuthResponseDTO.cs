namespace TicketSystem.Application.DTOs.Admin
{
    public class AuthResponseDTO
    {
        public string Email { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public DateTime ExpireTime { get; set; }
        public string Message { get; set; }
        public bool Succeeded { get; set; }
    }
}
