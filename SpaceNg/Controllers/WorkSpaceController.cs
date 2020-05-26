using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SpaceNg.Entities;
using SpaceNg.Models.RequestModels;

namespace SpaceNg.Controllers
{
    [Controller]
    public class WorkSpaceController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public WorkSpaceController(UserManager<ApplicationUser>userManager,
        SignInManager<ApplicationUser>signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestModel model)
        {
             var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View();
            }
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(CreateUserAccountModel model)
        {
            ApplicationUser user = new ApplicationUser

            {
                UserName = model.Name,
                Email = model.Email,

            };

            var result = await _userManager.CreateAsync(user, model.Password);

            return RedirectToAction("Index", "Home");
        }

    }
}