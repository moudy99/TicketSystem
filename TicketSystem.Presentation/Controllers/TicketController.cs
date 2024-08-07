using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketSystem.Application.DTOs.Ticket;
using TicketSystem.Application.GeneralResponse;
using TicketSystem.Application.Interfaces.Services;

namespace TicketSystem.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService ticketService;

        public TicketController(ITicketService ticketService)
        {
            this.ticketService = ticketService;
        }

        [HttpPost("createTicket")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GeneralResponse<TicketDto>>> CreateTicket([FromForm] CreateTicketDTO createTicket)
        {
            var response = await ticketService.CreateTicketAsync(createTicket);

            if (response.Succeeded)
            {
                return Ok(response);
            }

            else
            {
                return BadRequest(new
                {
                    Message = response.Message,
                    Errors = response.Errors
                });
            }

        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetAllTickets(int page = 1, int pageSize = 5)
        {
            var response = await ticketService.GetAllTicketsAsync(page, pageSize);

            if (response.Succeeded)
            {
                return Ok(response);
            }
            return BadRequest(new
            {
                Message = response.Message,
                Errors = response.Errors
            });
        }
    }
}
