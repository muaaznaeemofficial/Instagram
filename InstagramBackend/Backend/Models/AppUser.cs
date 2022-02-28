using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Backend.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<Follow> Follower { get; set; }
        public ICollection<Follow> Followee { get; set; }

    }
}
