using System;
using System.Linq;
using Data.Context;
using Domain.Entities;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new BlogContext();
            test.Users.Add(new User() {Name = "Janne"});

            test.Posts.Add(new Post()
            {
                Title = "Genesis blog post",
                Text =
                    "Lorem Ipsum is simply dummy text1 of the printing and typesetting industry. Lorem Ipsum has been the industry\'s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and",
                UserId = 1
            });
            test.SaveChanges();
            Console.WriteLine(test.Posts.First().Text);
            Console.ReadKey();

        }
    }
}