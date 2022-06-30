using Final_Exam_Back_End.Areas.FinalAdmin.Extensions;
using Final_Exam_Back_End.Areas.FinalAdmin.Utilities;
using Final_Exam_Back_End.DAL;
using Final_Exam_Back_End.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Exam_Back_End.Areas.FinalAdmin.Controllers
{
    [Area("FinalAdmin")]
    public class TeamController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TeamController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            List<Team> teams = await _context.Teams.ToListAsync();
            return View(teams);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Create(Team team)
        {
            if (!ModelState.IsValid) return View();

            if (team.Photo!=null)
            {
                if (!team.Photo.IsOkay(1))
                {
                    ModelState.AddModelError("Photo", "Zehmet olmasa duzgun sekil formasi secin(secidyiniz sekilin olcusu 1mb dan boyuk ola bilmez!)");
                    return View();
                }

                team.Image = team.Photo.FileCreate(_env.WebRootPath, @"assets\img\team");
            }
            else
            {
                ModelState.AddModelError("Photo", "Hec bir sekil secmemisiniz!");
                return View();
            }

            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int id)
        {
            Team team = await _context.Teams.FirstOrDefaultAsync(d => d.Id == id);
            if (team == null) return NotFound();

            return View(team);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Team team = await _context.Teams.FirstOrDefaultAsync(e => e.Id == id);
            if (team == null) return NotFound();
            return View(team);
        }


      

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Edit(Team team,int id)
        {
            if (!ModelState.IsValid) return NotFound();

            Team existedTeam = await _context.Teams.FirstOrDefaultAsync(e => e.Id == id);

            if (id != team.Id) return NotFound();
            
            if (existedTeam==null) return BadRequest();

            existedTeam.Position = team.Position;
            existedTeam.Name = team.Name;
            existedTeam.Subtitle = team.Subtitle;
            existedTeam.FbUrl = team.FbUrl;
            existedTeam.InstaUrl = team.InstaUrl;
            existedTeam.TwitterUrl = team.TwitterUrl;


            if (team.Photo!=null)
            {
                if (team.Photo.IsOkay(1))
                {

                    string path = team.Photo.FileName + _env.WebRootPath + @"~\assets\img\team\" + team.Image;

                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }

                    existedTeam.Image = team.Photo.FileCreate(_env.WebRootPath, @"assets\img\team");
                }

                else
                {
                    ModelState.AddModelError("Photo", "Zehmet olmasa duzgun sekil formasi secin(secidyiniz sekilin olcusu 1mb dan boyuk ola bilmez!)");
                    return View();
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
            
        }

        public async Task<IActionResult> Delete(int id)
        {
            Team team = await _context.Teams.FirstOrDefaultAsync(e => e.Id == id);
            if (team == null) return NotFound();
            return View(team);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [ActionName("Delete")]

        public async Task<IActionResult> DeleteTeam(Team team,int id)
        {
            Team existedTeam = await _context.Teams.FirstOrDefaultAsync(d => d.Id == id);

            if (existedTeam == null) return NotFound();

            string path = team.Photo + _env.WebRootPath + @"~\assets\img\team\" + team.Image;

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

             _context.Remove(existedTeam);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
