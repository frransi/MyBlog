﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace blog.ViewModels
{
    public class EditPostViewModel
    {
        [MaxLength(30)]
        [Required]
        public string Title { get; set; }

        [MaxLength(1000)]
        [Required]
        public string Content { get; set; }

        public int Id { get; set; }
    }
}
