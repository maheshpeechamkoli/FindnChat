using System.Threading.Tasks;
using FindnChitChat.Data;
using FindnChitChat.Dto;
using FindnChitChat.Model;
using Microsoft.AspNetCore.Mvc;

namespace FindnChitChat.Controllers 
{
    [Route ("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController (IAuthRepository repo) 
        {
            _repo = repo;
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
    }
}