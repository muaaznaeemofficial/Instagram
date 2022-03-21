using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTO.Post
{
    public class PostDto
    {
        public Guid ID { get; set; }
        public string Text { get; set; }
        public string PostById { get; set; }

        public string ImgPath { get; set; }

        public string PostByUserName { get; set; }
        public string PostByName { get; set; }
    }
}
