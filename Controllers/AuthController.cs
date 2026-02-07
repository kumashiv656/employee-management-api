using EmployeeApi.DTOs;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
namespace EmployeeApi.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        public AuthController(IConfiguration config)
        {
            _config = config;
            
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody]LoginDto dto)
        { 
            if(dto == null)
            return BadRequest("invalid request");
            //temp : Replace with DB later
            if(dto.Username != "Admin" || dto.Password != "1234")
            return Unauthorized();

            var token = GenerateToken(dto.Username);
            return Ok(new{Token = token});

            
        }
        private string GenerateToken(string Username)
        {
            var claims = new[]
            {
              new Claim(ClaimTypes.Name , Username),
              new Claim(ClaimTypes.Role , "Admin")  
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience : _config["Jwt:Audience"],
                claims : claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials : creds            );

                return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}