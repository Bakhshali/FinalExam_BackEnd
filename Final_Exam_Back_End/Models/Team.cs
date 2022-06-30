using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Exam_Back_End.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Image { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public string Subtitle { get; set; }

        public string FbUrl { get; set; }
        public string InstaUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string SkypeUrl { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }

    }
}
