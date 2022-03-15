using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTO.User
{
    public class UserListDto
    {
        public string ID { get; set; }
        public string userName { get; set; }
        public string Name { get; set; }
        public bool isFollowing { get; set; }

        public bool IsCurrentUser { get; set; }
    }
}
