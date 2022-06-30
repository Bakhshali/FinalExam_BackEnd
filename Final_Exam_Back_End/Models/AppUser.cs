using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Exam_Back_End.Models
{
    public class AppUser:IdentityUser
    {
        [Required, StringLength(maximumLength: 20)]
        public string Name { get; set; }

        [Required, StringLength(maximumLength: 20)]
        public string Surname { get; set; }
    }
}
