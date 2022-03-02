using Backend.Data;
using Backend.DTO.Follow;
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
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly ILoggerManager _logger;

        public UsersController(AppDbContext db, ILoggerManager logger)
        {
            _db = db;
            _logger = logger;

        }

        [HttpGet("getAll")]
        public IActionResult GetUsers()
        {
            return Ok(_db.Users);
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

                var suggestions = users.Select(user => new FollowSuggestionDto { ID = user.Id, userName = user.UserName });
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
