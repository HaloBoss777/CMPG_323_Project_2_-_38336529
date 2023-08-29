using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Project2_CMPG323.CORE.DTO;
using Project2_CMPG323.CORE.Models;
using Project2_CMPG323.CORE.Repos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Project2_CMPG323.CORE.Services
{
    public interface IAuthService
    {
        Task<AuthDTO?> Register(RegisterDTO registerdetails);
        Task<AuthDTO?> Login(RegisterDTO registerdetails);
    }

    public class AuthService : IAuthService
    {
        private readonly Project2Context _project2Context;
        private readonly IConfiguration _configuration;

        public AuthService(Project2Context project2Context, IConfiguration configuration)
        {
            _project2Context = project2Context;
            _configuration = configuration;
        }

        public async Task<AuthDTO?> Login(RegisterDTO registerdetails)
        {
            var foundUser = await _project2Context.Users.Where(x => x.Email.ToLower() == registerdetails.Email.ToLower() && x.Password == registerdetails.Password).FirstOrDefaultAsync();

            if(foundUser is not null)
            {
                return new AuthDTO
                {
                    APIToken = GenerateJWTToken(foundUser.UserId, foundUser.Email),
                };
            }

            return null;
        }

        public async Task<AuthDTO?> Register(RegisterDTO registerdetails)
        {
            var foundUser = await _project2Context.Users.Where(x => x.Email.ToLower() == registerdetails.Email.ToLower()).FirstOrDefaultAsync();
        
            if(foundUser is not null)
            {
                return null;
            }

            var newUser = new User
            {
                Email = registerdetails.Email,
                Password = registerdetails.Password,
            };

            await _project2Context.Users.AddAsync(newUser);
            await _project2Context.SaveChangesAsync();

            return new AuthDTO
            {
                APIToken = GenerateJWTToken(newUser.UserId, newUser.Email)
            };
        }

        private string GenerateJWTToken(int UserId, string Email)
        {
            var claims = new List<Claim>
            {
                new Claim("UserId", UserId.ToString()),
                new Claim("Email", Email),
            };

            var newclaims = claims.AsEnumerable<Claim>();

            var Token = new JwtSecurityToken(_configuration["JWT:Issuer"], _configuration["JWT:Audience"], newclaims, expires: DateTime.Now.AddDays(4));

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
