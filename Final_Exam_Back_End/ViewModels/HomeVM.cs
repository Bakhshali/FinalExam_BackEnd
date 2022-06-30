using Final_Exam_Back_End.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Exam_Back_End.ViewModels
{
    public class HomeVM
    {
        public List<Setting> Settings { get; set; }
        public List<Team> Teams { get; set; }
    }
}
