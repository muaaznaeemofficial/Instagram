using Backend.Data;
using Backend.DTO.Follow;
using Backend.DTO.Profile;
using LoggerService;
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
    public class ProfileController : ControllerBase
    {

        private readonly AppDbContext _db;
        private readonly ILoggerManager _logger;

        public ProfileController(AppDbContext db, ILoggerManager logger)
        {
            _db = db;
            _logger = logger;

        }
        //[HttpGet("GetProfileData/{id}")]
        //public ActionResult GetProfile(string id)
        //{
        //    var user = _db.Users.Include(u => u.Followee).Include(u => u.Follower).Where(x => x.Id == id).FirstOrDefault();
        //    var users = _db.Users;
        //    var profile = new ProfileDto() { ID = user.Id, UserName = user.UserName, PostCount = 0, FollowersCount = user.Follower.Count, FollowingCount = user.Followee.Count, Name = user.Name, IsFollowed = true };

        //    return Ok(profile);
        //}
        [HttpPost("GetProfileData")]
        public ActionResult GetProfile([FromBody] UserFollowListDto user)
        {
            var users = _db.Users;
            var profileUser = users.Include(u => u.Followee).Include(u => u.Follower).Where(x => x.Id == user.ProfileUserId).FirstOrDefault();
            var currentUser = users.Include(u => u.Followee).Where(x => x.Id == user.CurrentUserId).FirstOrDefault();


            var followerIds = currentUser.Followee.Select(x => x.FollowerId).ToList();


            var profile = new ProfileDto() { ID = profileUser.Id, UserName = profileUser.UserName, PostCount = 0, FollowersCount = profileUser.Follower.Count, FollowingCount = profileUser.Followee.Count, Name = profileUser.Name, IsFollowed = followerIds.Contains(profileUser.Id) };

            return Ok(profile);
        }
    }
}
