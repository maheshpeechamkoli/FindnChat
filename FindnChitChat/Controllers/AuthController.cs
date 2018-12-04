using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FindnChitChat.Data;
using FindnChitChat.Dto;
using FindnChitChat.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FindnChitChat.Controllers 
{
    [Route ("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        public AuthController (IAuthRepository repo, IConfiguration config) 
        {
            _repo = repo;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userDto)
        {
            //validate 

            userDto.UserName = userDto.UserName.ToLower();

            if(await _repo.UserExists(userDto.UserName))
                return BadRequest("Username already exist");

            var userToCreate = new User 
            {
                UserName = userDto.UserName
            };

            var creatuser = await _repo.Register(userToCreate, userDto.Password);

            return StatusCode(201); 
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userDto)
        {
            var userFromRepo = await _repo.Login(userDto.UserName.ToLower(), userDto.Password);

            if(userFromRepo == null)
                return Unauthorized();
            
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name,userFromRepo.UserName)
            };

            var key =new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescripter = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescripter);

            return Ok(new {
                token = tokenHandler.WriteToken(token)
            });

        }
    }
}