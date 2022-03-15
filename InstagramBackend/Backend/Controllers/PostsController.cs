using Backend.Data;
using Backend.DTO.Follow;
using Backend.DTO.Post;
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
using System.Net;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]

    public class PostsController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly ILoggerManager _logger;


        public PostsController(AppDbContext db, ILoggerManager logger)
        {
            _db = db;
            _logger = logger;

        }


        [HttpGet("GetPosts")]
        public IActionResult GetPosts()
        {
            var posts = _db.Posts.Include(p => p.PostBy).OrderByDescending(u => u.PostTime).Select(post => new PostDto { ID = post.ID, PostByName = post.PostBy.Name, PostByUserName = post.PostBy.UserName, Text = post.Text, PostById = post.PostBy.Id });

            return Ok(posts);
        }




        [HttpPost("NewPost")]
        public IActionResult NewPost([FromBody] NewPostDto post)
        {
            var newPost = new Post
            {
                PostById = post.PostById,
                Text = post.Text,
                PostTime = DateTime.Now
            };
            _db.Posts.Add(newPost);
            _db.SaveChanges();

            var user = _db.Users.Where(u => u.Id == post.PostById).FirstOrDefault();
            return Ok(new PostDto { ID = newPost.ID, PostByName = user.Name, Text = newPost.Text, PostByUserName = user.UserName });
        }


    }
}
