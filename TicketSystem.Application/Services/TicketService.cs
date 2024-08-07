using AutoMapper;
using TicketSystem.Application.DTOs.Ticket;
using TicketSystem.Application.GeneralResponse;
using TicketSystem.Application.Helpers;
using TicketSystem.Application.Interfaces.Services;
using TicketSystem.Application.Interfaces.UnitOfWork;
using TicketSystem.Core.Entities;

namespace TicketSystem.Application.Services
{
    public class TicketService : ITicketService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public TicketService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<GeneralResponse<TicketDto>> CreateTicketAsync(CreateTicketDTO createTicket)
        {
            var existingUser = unitOfWork.userRepository.Find(u => u.PhoneNumber == createTicket.UserPhoneNumber);

            if (existingUser != null)
            {
                var existingTicket = unitOfWork.ticketRepository.Find(t => t.UserId == existingUser.Id);

                if (existingTicket != null)
                {
                    return new GeneralResponse<TicketDto>
                    {
                        Message = "User already has an existing ticket.",
                        Succeeded = false
                    };
                }
                else
                {
                    var ticket = new Ticket
                    {
                        UserId = existingUser.Id,
                        ImagePath = createTicket.TicketImageFile.Length > 0 ? await ImageSavingHelper.SaveOneImageAsync(createTicket.TicketImageFile, "Tickets") : "/Images/Tickets/ticket.png",
                        CreatedAt = DateTime.Now,
                        Number = new Guid().ToString(),

                    };

                    try
                    {
                        unitOfWork.ticketRepository.Add(ticket);
                        await unitOfWork.SaveChangesAsync();

                        var ticketDto = mapper.Map<TicketDto>(ticket);
                        return new GeneralResponse<TicketDto>
                        {
                            Data = ticketDto,
                            Message = "Ticket created successfully.",
                            Succeeded = true
                        };
                    }
                    catch (Exception ex)
                    {
                        return new GeneralResponse<TicketDto>
                        {
                            Message = "Error while creating the ticket.",
                            Succeeded = false,
                            Errors = new List<string> { ex.Message }
                        };
                    }
                }
            }
            else
            {
                var user = mapper.Map<User>(createTicket);

                try
                {
                    unitOfWork.userRepository.Add(user);
                    await unitOfWork.SaveChangesAsync();

                    var ticket = new Ticket
                    {
                        UserId = user.Id,
                        ImagePath = createTicket.TicketImageFile.Length > 0 ? await ImageSavingHelper.SaveOneImageAsync(createTicket.TicketImageFile, "Tickets") : "/Images/Tickets/ticket.png",
                        CreatedAt = DateTime.Now,
                        Number = Guid.NewGuid().ToString()
                    };

                    unitOfWork.ticketRepository.Add(ticket);
                    await unitOfWork.SaveChangesAsync();

                    var ticketDto = mapper.Map<TicketDto>(ticket);
                    return new GeneralResponse<TicketDto>
                    {
                        Data = ticketDto,
                        Message = "User and ticket created successfully.",
                        Succeeded = true
                    };
                }
                catch (Exception ex)
                {
                    return new GeneralResponse<TicketDto>
                    {
                        Message = "Error while creating the user and ticket.",
                        Succeeded = false,
                        Errors = new List<string> { ex.Message }
                    };
                }
            }
        }

        public async Task<GeneralResponse<IEnumerable<TicketDto>>> GetAllTicketsAsync(int page, int pageSize)
        {
            try
            {
                var tickets = unitOfWork.ticketRepository.FindAll();


                var paginatedList = PaginationHelper.Paginate(tickets, page, pageSize);
                var paginationInfo = PaginationHelper.GetPaginationInfo(paginatedList);

                var TicketsDTOs = mapper.Map<IEnumerable<TicketDto>>(paginatedList.Items);

                return new GeneralResponse<IEnumerable<TicketDto>>
                {
                    Data = TicketsDTOs,
                    Message = "Tickets retrieved successfully.",
                    Succeeded = true,
                    PaginationInfo = paginationInfo
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<IEnumerable<TicketDto>>
                {
                    Message = "Error while retrieving tickets.",
                    Succeeded = false,
                    Errors = new List<string> { ex.Message }
                };
            }
        }
        public Task<GeneralResponse<TicketDto>> GetTicketByPhoneNumberAsync(string phoneNumber)
        {
            throw new NotImplementedException();
        }
    }
}
