using Microsoft.AspNetCore.Mvc;
using TicketSystem.Application.DTOs.Admin;
using TicketSystem.Application.GeneralResponse;
using TicketSystem.Application.Interfaces.Services;

namespace TicketSystem.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService adminService;

        public AdminController(IAdminService adminService)
        {
            this.adminService = adminService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAdmin(RegisterAdminDto registerAdminDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new GeneralResponse<object>
                {
                    Succeeded = false,
                    Message = "Invalid registration data",
                    Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList()
                });
            }

            try
            {
                var response = await adminService.RegisterAdminAsync(registerAdminDto);
                if (response.Succeeded)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GeneralResponse<object>
                {
                    Succeeded = false,
                    Message = "An error occurred while registering the admin",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAdmin(LoginAdminDto loginAdminDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new GeneralResponse<object>
                {
                    Succeeded = false,
                    Message = "Invalid login data",
                    Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList()
                });
            }

            try
            {
                var response = await adminService.LoginAdminAsync(loginAdminDto);
                if (response.Succeeded)
                {
                    return Ok(response);
                }
                return Unauthorized(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GeneralResponse<object>
                {
                    Succeeded = false,
                    Message = "An error occurred while logging in the admin",
                    Errors = new List<string> { ex.Message }
                });
            }
        }
    }
}