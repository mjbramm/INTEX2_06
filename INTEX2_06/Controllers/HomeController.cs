using INTEX2_06.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using INTEX2_06.Models.ViewModels;

namespace INTEX2_06.Controllers
{
    public class HomeController : Controller
    {
        private IBookRepository _repo;


        private UserManager<AppUser> userManager;

        public HomeController(UserManager<AppUser> userMgr, IBookRepository temp)
        {
            userManager = userMgr;
            _repo = temp;
        }


        //public IActionResult Index(int pageNum, string? bookCategory)
        //{
        //    int pageSize = 10;

        //    var blah = new BooksListViewModel
        //    {
        //        Books = _repo.Books
        //            .Where(x => x.Category == bookCategory || bookCategory == null)
        //            .OrderBy(x => x.Title)
        //            .Skip((pageNum - 1) * pageSize)
        //            .Take(pageSize),

        //        PaginationInfo = new PaginationInfo
        //        {
        //            CurrentPage = pageNum,
        //            ItemsPerPage = pageSize,
        //            TotalItems = bookCategory == null ? _repo.Books.Count() : _repo.Books.Where(x => x.Category == bookCategory).Count()

        //        },

        //        CurrentBookCategory = bookCategory
        //    };

        //    return View(blah);
        //}

        //[Authorize]
        ////[Authorize(Roles = "Manager")]
        //public async Task<IActionResult> Index(int pageNum, string? bookCategory)
        //{
        //    AppUser user = await userManager.GetUserAsync(HttpContext.User);
        //    string message = "Hello " + user.UserName;

        //    return View((object)message);
        //}






        [Authorize]
        public async Task<IActionResult> Index(int pageNum, string? bookCategory)
        {
            // Check if the user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                AppUser user = await userManager.GetUserAsync(HttpContext.User);
                string message = "Hello " + user.UserName;
                return View((object)message);
            }
            
            int pageSize = 10;

            var blah = new BooksListViewModel
            {
                Books = _repo.Books
                    .Where(x => x.Category == bookCategory || bookCategory == null)
                    .OrderBy(x => x.Title)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize),

                PaginationInfo = new PaginationInfo
                {
                    CurrentPage = pageNum,
                    ItemsPerPage = pageSize,
                    TotalItems = bookCategory == null ? _repo.Books.Count() : _repo.Books.Where(x => x.Category == bookCategory).Count()

                },

                CurrentBookCategory = bookCategory
            };

            return View(blah);
        }



        public async Task<IActionResult> Privacy()
        {
            return View(Privacy);
        }
    }
}