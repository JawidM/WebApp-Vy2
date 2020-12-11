using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VyModels
{
    public class Admin
    {
        [Required(ErrorMessage = "Email required")]
        [EmailAddress(ErrorMessage = "Enter valid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}