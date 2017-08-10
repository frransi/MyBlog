using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Post
    {
        public string Text { get; set; }
        public string Title { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
