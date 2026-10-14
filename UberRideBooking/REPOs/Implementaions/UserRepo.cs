using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UberRideBooking.Data;
using UberRideBooking.DTOs.UserDTO;
using UberRideBooking.Models;
using UberRideBooking.REPOs.Interfaces;

namespace UberRideBooking.REPOs.Implementaions
{
    public class UserRepo : GenericRepo<User>, IUserRepo
    {
        private readonly AppDbContext _context;
        public IConfiguration _configuration;

        public UserRepo(AppDbContext context, IConfiguration configuration) : base(context)
        {
            _context = context;
            _configuration = configuration;
        }
        public ResponseDTO GenrateToken(User user)
        {
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new List<System.Security.Claims.Claim>
            {
                new Claim(ClaimTypes.Name, user.FullName),
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Email, user.Email),
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, user.Role)
            };
            var tokenobj = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signingCredentials
            );
            return new ResponseDTO
            {
                Token = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler().WriteToken(tokenobj),
                Expiration = DateTime.Now.AddMinutes(30)
            };
        }
        public async Task<ResponseDTO> Login(LoginDTO loginDTO)
        {
            var u = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDTO.Email && u.Password == loginDTO.Password);
            if (u == null)
            {
                return null;
            }
            var token = GenrateToken(u);
            return token;
        }

        public async Task<ResponseDTO> Register(RegisterDTO loginDTO)
        {
            var us = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDTO.Email);
            if(us != null)
            {
                return null;
            }
            var u = new User
            {
                FullName = loginDTO.FullName,
                Email = loginDTO.Email,
                Password = loginDTO.Password,
                Phone = loginDTO.Phone,
                Role = loginDTO.Role
            };
            await _context.Users.AddAsync(u);
            var token = GenrateToken(u);
            return token;
        }
    }
}
