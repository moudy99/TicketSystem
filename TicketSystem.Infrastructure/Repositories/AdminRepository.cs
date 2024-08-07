using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TicketSystem.Application.Configurations;
using TicketSystem.Application.DTOs.Admin;
using TicketSystem.Application.Interfaces.Repository;
using TicketSystem.Core.Entities;
using TicketSystem.Core.Enums;

namespace TicketSystem.Infrastructure.Repositories
{
    public class AdminRepository : BaseRepository<ApplicationUser>, IAdminRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOptions<JWT> jWT;
        private readonly IConfiguration _configuration;
        public AdminRepository(ApplicationDbContext context, IConfiguration configuration, IOptions<JWT> JWT, UserManager<ApplicationUser> userManager) : base(context)
        {
            _context = context;
            _configuration = configuration;
            jWT = JWT;
            this._userManager = userManager;
        }


        public async Task<AuthResponseDTO> AdminRegister(ApplicationUser user, string password)
        {
            if (await _userManager.FindByEmailAsync(user.Email) is not null)
            {
                return new AuthResponseDTO()
                {
                    Message = "Email Already registerd",
                    Succeeded = false
                };
            }

            user.UserName = GenerateUsernameFromEmail(user.Email);
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, UserRole.Admin.ToString());
                var Token = await CreateJwtToken(user);
                var tokenString = new JwtSecurityTokenHandler().WriteToken(Token);
                return new AuthResponseDTO()
                {

                    Message = "Register successful",
                    Email = user.Email,
                    ExpireTime = Token.ValidTo,
                    Token = tokenString,
                    Role = user.Role.ToString(),
                    Succeeded = true

                };
            }
            return new AuthResponseDTO()
            {
                Message = "Register failed",
                Succeeded = false
            };

        }

        public async Task<AuthResponseDTO> AdminLogin(LoginAdminDto LoginAdminDto)
        {
            var user = await _userManager.FindByEmailAsync(LoginAdminDto.Email);
            if (user != null)
            {
                bool found = await _userManager.CheckPasswordAsync(user, LoginAdminDto.Password);
                if (found)
                {
                    var Token = await CreateJwtToken(user);
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(Token);

                    return new AuthResponseDTO()
                    {
                        Message = "Login successful",
                        Email = user.Email,
                        ExpireTime = Token.ValidTo,
                        Token = tokenString,
                        Role = user.Role.ToString(),
                        Succeeded = true

                    };
                }
                else
                {
                    return new AuthResponseDTO()
                    {
                        Message = "Login failed: Incorrect email or password",
                        Succeeded = false
                    };
                }
            }

            return new AuthResponseDTO()
            {
                Message = "Login failed: User not found",
                Succeeded = false
            };
        }


        private string GenerateUsernameFromEmail(string email)
        {
            return email.ToLower();
        }
        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("uid", user.Id)
                }
                .Union(userClaims)
                .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jWT.Value.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var durationDaysString = jWT.Value.DurationInDays;

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: jWT.Value.Issuer,
                audience: jWT.Value.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: signingCredentials
            );


            return jwtSecurityToken;
        }


    }
}
