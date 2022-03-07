using Backend.Data;
using Backend.DTO.Follow;
using Backend.DTO.User;
using Backend.Models;
using LoggerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]

    public class FollowsController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly ILoggerManager _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FollowsController(AppDbContext db, ILoggerManager logger, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _logger = logger;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;


        }



        [HttpPost("doFollow")]

        public IActionResult DoFollow(FollowDto dto)
        {
            try
            {

                var alreadyFollowed = _db.Follows.Where(f => f.FollowerId == dto.FollowerId && f.FolloweeId == dto.FolloweeId).FirstOrDefault();

                if (alreadyFollowed != null)
                {
                    _db.Remove(alreadyFollowed);
                    _db.SaveChanges();
                    return Ok();
                }
                _db.Follows.Add(new Follow() { FolloweeId = dto.FolloweeId, FollowerId = dto.FollowerId });
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($" {nameof(DoFollow)} {e.Message}");
                return BadRequest(new { message = "Something bad happened." });
            }

        }
        [HttpGet("followings/{id}")]
        public IActionResult GetFollowings(string id)
        {
            try
            {
                var currentUser = _db.Users.Include(x => x.Followee).Where(x => x.Id == id).FirstOrDefault();
                var follwingIds = currentUser.Followee.Select(x => x.FollowerId).ToList();
                var followerIds = currentUser.Followee.Select(x => x.FollowerId).ToList();

                var users = _db.Users.Where(x => follwingIds.Contains(x.Id) && x.Id != id).ToList();

                var suggestions = users.Select(user => new UserListDto { ID = user.Id, userName = user.UserName, Name = user.Name, isFollowing = followerIds.Contains(user.Id) });
                return Ok(suggestions);
            }
            catch (Exception e)
            {
                _logger.LogError($" {nameof(GetFollowings)} {e.Message}");
                return BadRequest(new { message = "Something bad happened." });
            }
        }

        [HttpGet("followers/{id}")]
        public IActionResult GetFollowers(string id)
        {
            try
            {
                var currentUser = _db.Users.Include(x => x.Follower).Include(x => x.Followee).Where(x => x.Id == id).FirstOrDefault();
                var follwingIds = currentUser.Follower.Select(x => x.FolloweeId).ToList();
                var followerIds = currentUser.Followee.Select(x => x.FollowerId).ToList();

                var users = _db.Users.Where(x => follwingIds.Contains(x.Id) && x.Id != id).ToList();

                var suggestions = users.Select(user => new UserListDto { ID = user.Id, userName = user.UserName, Name = user.Name, isFollowing = followerIds.Contains(user.Id) });
                return Ok(suggestions);
            }
            catch (Exception e)
            {
                _logger.LogError($" {nameof(GetFollowings)} {e.Message}");
                return BadRequest(new { message = "Something bad happened." });
            }
        }
        [HttpGet("getUsers")]
        public IActionResult GetUsers()
        {
            return Ok(_db.Users);
        }
    }
}
