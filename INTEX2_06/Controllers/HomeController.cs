﻿using INTEX2_06.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace INTEX2_06.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<AppUser> userManager;
        public HomeController(UserManager<AppUser> userMgr)
        {
            userManager = userMgr;
        }

        [Authorize]
        //[Authorize(Roles = "Manager")]
        public async Task<IActionResult> Index()
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            string message = "Hello " + user.UserName;
            return View((object)message);
        }

        public async Task<IActionResult> Privacy()
        {
            return View(Privacy);
        }
    }
}