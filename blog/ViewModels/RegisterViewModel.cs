using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace blog.ViewModels
{
    public class RegisterViewModel
    {
        [MaxLength(30, ErrorMessage = "Max 30 tecken")]
        [MinLength(4, ErrorMessage = "Minst 4 tecken")]
        [Required]
        public string Login { get; set; }

        [MinLength(4, ErrorMessage = "Minst 4 tecken")]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [MinLength(4, ErrorMessage = "Minst 4 tecken")]
        [DataType(DataType.Password)]
        [Required]
        [Compare(nameof(Password),ErrorMessage = "Lösenorden matchar inte")]
        public string ConfirmPassword { get; set; }
    }
}
