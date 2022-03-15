using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTO.Follow
{
    public class UserFollowListDto
    {
        public string ProfileUserId { get; set; }
        public string CurrentUserId { get; set; }

    }
}
