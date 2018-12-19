using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FindnChitChat.Data;
using FindnChitChat.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FindnChitChat.Controllers {

    [Authorize]
    [Route ("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase {
        private readonly IFindingRepository _repo;
        private readonly IMapper _mapper;
        public UserController (IFindingRepository repo, IMapper mapper) {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers () {
            var user = await _repo.GetUsers ();
            
            var userToReturn = _mapper.Map<IEnumerable<UserForListDto>>(user);

            return Ok (userToReturn);
        }

        [HttpGet ("{id}")]
        public async Task<IActionResult> GetUser (int id) {
            var user = await _repo.GetUser (id);

            var userToReturn = _mapper.Map<UserForDetailedDto>(user);

            return Ok (userToReturn);
        }

    }
}