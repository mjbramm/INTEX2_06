﻿using INTEX2_06.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using INTEX2_06.Models.ViewModels;
using Microsoft.AspNet.Identity;

namespace INTEX2_06.Controllers
{
    public class HomeController : Controller
    {
        private ILegoRepository _repo;
        //private UserManager<AppUser> userManager;

        public HomeController(ILegoRepository temp) //UserManager<AppUser> userMgr
        {
            //userManager = userMgr;
            _repo = temp;
        }


        public async Task<IActionResult> Index(int pageNum, string? legoCategory)
        {
            return View();
        }

        public async Task<IActionResult> Legostore(int pageNum, string? legoCategory)
        {
            // Check if the user is authenticated
            //if (User.Identity.IsAuthenticated)
            //{
            //    AppUser user = await userManager.GetUserAsync(HttpContext.User);
            //}
            
            int pageSize = 5;

            var blah = new LegosListViewModel
            {
                Legos = _repo.Legos
                    .Where(x => x.category == legoCategory || legoCategory == null)
                    .OrderBy(x => x.name)
                    .Skip((2 - 1) * pageSize)
                    .Take(pageSize),

                PaginationInfo = new PaginationInfo
                {
                    CurrentPage = pageNum,
                    ItemsPerPage = pageSize,
                    TotalItems = legoCategory == null ? _repo.Legos.Count() : _repo.Legos.Where(x => x.category == legoCategory).Count()

                },

                CurrentLegoCategory = legoCategory
            };

            return View(blah);
        }

        public async Task<IActionResult> Privacy()
        {
            return View(Privacy);
        }
    }
}