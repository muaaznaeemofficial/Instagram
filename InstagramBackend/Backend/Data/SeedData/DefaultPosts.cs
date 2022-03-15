using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Data.SeedData
{
    public static class DefaultPosts
    {

        public static IEnumerable<Post> Posts()
        {
            var posts = new List<Post>();


            for (int i = 0; i < 10; i++)
            {
                var post = new Post()
                {
                    ID = new Guid($"06E95541-8667-40FB-5B0E-08DA0033406{i}"),
                    Text = "Lorem ipsum dolor, sit amet consectetur adipisicing elit. Magni quae placeat modi vitae necessitatibus quos voluptatem perspiciatis, ipsam possimus amet, doloribus dolor, tempora autem omnis culpa blanditiis quam. Aut, odio.",
                    PostTime = DateTime.Now,
                    PostById = "6b8dab10-c398-4e1a-8352-834b5cae2021"

                };
                posts.Add(post);
            }
            return posts;
        }

    }
}
