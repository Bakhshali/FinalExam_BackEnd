﻿using Final_Exam_Back_End.Models;
using Final_Exam_Back_End.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Exam_Back_End.Areas.FinalAdmin.Controllers
{
    [Area("FinalAdmin")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser>signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]


        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid) return View();

            AppUser user = new AppUser
            {
                Name = register.Name,
                Surname = register.Surname,
                UserName = register.Username,
                Email = register.Email
            };

            if (register.Condition==true)
            {
                IdentityResult result = await _userManager.CreateAsync(user, register.Password);

                if (!result.Succeeded)
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("Photo", "Sozlesmeni qebul etmeden register ola bilmersiniz!");
                return View();
            }

            return RedirectToAction("Index", "Team");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Login(LoginVM login)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser user = await _userManager.FindByNameAsync(login.Username);

            if (login.RememberMe==true)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, login.Password, true, true);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Username ve ya Password yanlisdir");
                    return View();
                }
            }
            else
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, login.Password, false, true);

                if (!result.Succeeded)
                {
                    if (result.IsLockedOut)
                    {
                        ModelState.AddModelError("", "Sifreni 3 defe yanlis girdiyiniz ucun 5 deq-lik bloklandiniz");
                        return View();
                    }

                    ModelState.AddModelError("", "Username ve ya Password yanlisdir");
                    return View();
                }
            }

            return RedirectToAction("Index", "Team");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Team");
        }
    }
}
