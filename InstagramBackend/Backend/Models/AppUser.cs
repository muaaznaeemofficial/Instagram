using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Backend.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Follow> Follower { get; set; }
        public ICollection<Follow> Followee { get; set; }

    }
}
