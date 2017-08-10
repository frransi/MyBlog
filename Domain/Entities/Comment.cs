using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Comment
    {
        public string Text { get; set; }
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

    }
}
