using Backend.Data;
using Backend.DTO.Follow;
using Backend.Models;
using LoggerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [Authorize(AuthenticationSchemes = "Bearer")]

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

        [HttpGet("getUsers")]
        public IActionResult GetUsers()
        {
            return Ok(_db.Users);
        }
    }
}
