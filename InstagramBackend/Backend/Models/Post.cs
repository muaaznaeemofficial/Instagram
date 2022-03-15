using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class Post
    {
        public Guid ID { get; set; }
        public string Text { get; set; }


        [ForeignKey("PostBy")]
        public string PostById { get; set; }

        public DateTime PostTime { get; set; }

        public AppUser PostBy { get; set; }
    }
}
