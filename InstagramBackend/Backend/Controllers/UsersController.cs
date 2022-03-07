using Backend.Data;
using Backend.DTO.Follow;
using Backend.DTO.User;
using Backend.Models;
using LoggerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]

    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly ILoggerManager _logger;

        public UsersController(AppDbContext db, ILoggerManager logger)
        {
            _db = db;
            _logger = logger;

        }

        [HttpGet("getAll/{id}")]
        public IActionResult GetUsers(string id)
        {
            try
            {
                var users = _db.Users;

                var currentUser = users.Include(x => x.Followee).Where(u => u.Id == id).FirstOrDefault();
                var followerIds = currentUser.Followee.Select(x => x.FollowerId).ToList();

                var userList = users.Where(u => u.Id != currentUser.Id).Select(user => new UserListDto
                {
                    ID = user.Id,
                    Name = user.Name,
                    userName = user.UserName,
                    isFollowing = followerIds.Contains(user.Id)
                });

                return Ok(userList);
            }
            catch (Exception e)
            {
                _logger.LogError($" {nameof(GetUsers)} {e.Message}");
                return BadRequest(new { message = "Something bad happened." });
            }

        }

        //[Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("suggestions/{id}")]
        public IActionResult GetSuggestions(string id)
        {

            try
            {

                var currentUser = _db.Users.Include(x => x.Followee).Where(x => x.Id == id).FirstOrDefault();
                var followerIds = currentUser.Followee.Select(x => x.FollowerId).ToList();

                var users = _db.Users.Where(x => !followerIds.Contains(x.Id) && x.Id != id).ToList();

                var suggestions = users.Select(user => new FollowSuggestionDto { ID = user.Id, userName = user.UserName, Name = user.Name });
                return Ok(suggestions);
            }
            catch (Exception e)
            {
                _logger.LogError($" {nameof(GetSuggestions)} {e.Message}");
                return BadRequest(new { message = "Something bad happened." });
            }

        }


    }
}
