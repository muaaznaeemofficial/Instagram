using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTO.Profile
{
    public class ProfileDto
    {
        public string ID { get; set; }
        public string UserName { get; set; }
        public int PostCount { get; set; }
        public int FollowersCount { get; set; }
        public int FollowingCount { get; set; }
        public string Name { get; set; }

        public bool IsFollowed { get; set; }


    }
}
