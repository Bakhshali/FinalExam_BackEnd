using Final_Exam_Back_End.DAL;
using Final_Exam_Back_End.Models;
using Final_Exam_Back_End.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Exam_Back_End.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult>  Index()
        {
            HomeVM model = new HomeVM
            {
                Settings = await _context.Settings.ToListAsync(),
                Teams = await _context.Teams.ToListAsync()
            };
            return View(model);
        }
    }
}
