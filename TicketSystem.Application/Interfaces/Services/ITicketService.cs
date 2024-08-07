﻿using TicketSystem.Application.DTOs.Ticket;
using TicketSystem.Application.GeneralResponse;

namespace TicketSystem.Application.Interfaces.Services
{
    public interface ITicketService
    {
        Task<GeneralResponse<TicketDto>> CreateTicketAsync(CreateTicketDTO createTicket);
        Task<GeneralResponse<IEnumerable<TicketDto>>> GetAllTicketsAsync(int page, int pageSize);
        Task<GeneralResponse<TicketDto>> GetTicketByPhoneNumberAsync(string phoneNumber);

    }
}
