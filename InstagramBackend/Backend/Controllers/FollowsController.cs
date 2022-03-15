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

        [HttpPost("followings")]
        public IActionResult Followings([FromBody] UserFollowListDto user)
        {
            try
            {
                var profileUser = _db.Users.Include(x => x.Followee).Where(x => x.Id == user.ProfileUserId).FirstOrDefault();
                var currentUser = _db.Users.Include(x => x.Followee).Where(x => x.Id == user.CurrentUserId).FirstOrDefault();

                var follwingIds = profileUser.Followee.Select(x => x.FollowerId).ToList();
                var followerIds = currentUser.Followee.Select(x => x.FollowerId).ToList();

                var users = _db.Users.Where(x => follwingIds.Contains(x.Id) && x.Id != user.ProfileUserId).ToList();

                var suggestions = users.Select(user => new UserListDto { ID = user.Id, userName = user.UserName, Name = user.Name, isFollowing = followerIds.Contains(user.Id), IsCurrentUser = user.Id == currentUser.Id });
                return Ok(suggestions);
            }
            catch (Exception e)
            {
                _logger.LogError($" {nameof(Followings)} {e.Message}");
                return BadRequest(new { message = "Something bad happened." });
            }
        }
        [HttpPost("followers")]
        public IActionResult GetFollowers([FromBody] UserFollowListDto user)
        {
            try
            {
                var profileUser = _db.Users.Include(x => x.Follower).Include(x => x.Followee).Where(x => x.Id == user.ProfileUserId).FirstOrDefault();
                var currentUser = _db.Users.Include(x => x.Follower).Include(x => x.Followee).Where(x => x.Id == user.CurrentUserId).FirstOrDefault();
                var follwingIds = profileUser.Follower.Select(x => x.FolloweeId).ToList();
                var followerIds = currentUser.Followee.Select(x => x.FollowerId).ToList();

                var users = _db.Users.Where(x => follwingIds.Contains(x.Id) && x.Id != user.ProfileUserId).ToList();

                var suggestions = users.Select(user => new UserListDto { ID = user.Id, userName = user.UserName, Name = user.Name, isFollowing = followerIds.Contains(user.Id), IsCurrentUser = user.Id == currentUser.Id });
                return Ok(suggestions);
            }
            catch (Exception e)
            {
                _logger.LogError($" {nameof(GetFollowers)} {e.Message}");
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
