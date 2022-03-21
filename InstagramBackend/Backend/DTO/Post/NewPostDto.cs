using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTO.Post
{
    public class NewPostDto
    {
        public string PostById { get; set; }
        public string Text { get; set; }

        public string ImgPath { get; set; }



    }
}
