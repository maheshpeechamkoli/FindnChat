using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using FindnChitChat.Data;
using FindnChitChat.Dto;
using FindnChitChat.Helper;
using FindnChitChat.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FindnChitChat.Controllers 
{

    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [Route ("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase 
    {
        private readonly IFindingRepository _repo;
        private readonly IMapper _mapper;
        public UserController (IFindingRepository repo, IMapper mapper) 
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery]UserParams userParams)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userFromRepo = await _repo.GetUser(currentUserId);
           
            userParams.UserId = currentUserId;
            if (string.IsNullOrEmpty(userParams.Gender))
            {
                userParams.Gender = userFromRepo.Gender == "male" ? "female" : "male";
            }

            var user = await _repo.GetUsers(userParams);
            
            var userToReturn = _mapper.Map<IEnumerable<UserForListDto>>(user);

            //Response header have pagination information 
            Response.Addpagination(user.CurrentPage, user.PageSize, user.TotalCount, user.TotalPages);

            return Ok (userToReturn);
        }

        [HttpGet ("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser (int id) {
            var user = await _repo.GetUser (id);

            var userToReturn = _mapper.Map<UserForDetailedDto>(user);

            return Ok (userToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserForUpdateDto userForUpdateDto)
        {
            if(id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userForRepo = await _repo.GetUser(id);

            _mapper.Map(userForUpdateDto, userForRepo);

            if(await _repo.SaveAll())
                return NoContent();
            
            throw new Exception($"Updating user {id} failed on save");
        }

        [HttpPost("{id}/like/{recipientId}")]
        public async Task<IActionResult> LikeUser(int id, int recipientId)
        {
            if(id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var like = await _repo.GetLike(id, recipientId);

            if(like != null)
                return BadRequest("You already like this User");
            
            if( await _repo.GetUser(recipientId) == null)
                return NotFound();
            like = new Like
            {
                LikerId = id,
                LikeeId = recipientId
            };
            _repo.Add<Like>(like);

            if(await _repo.SaveAll())
                return Ok();

            return BadRequest("Failed Like user");
        }

    }
}