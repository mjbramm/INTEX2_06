using INTEX2_06.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using INTEX2_06.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

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


        public async Task<IActionResult> Index()
        {
            //var legos = _repo.Legos.ToList();
            var legos = new LegosListViewModel
            {
                Legos = _repo.Legos
                   .OrderByDescending(x => x.avg_rating)
                   .Take(9)
            };
            return View(legos);
        }
        [HttpGet]
        public async Task<IActionResult> SingleProduct(int product_ID)
        {
            var product = await _repo.Legos.FirstOrDefaultAsync(p => p.product_ID == product_ID);
            if (product == null)
            {
                return NotFound();
            }

            var recommendationIDs = new List<int>
    {
        product.small_rec_1,
        product.small_rec_2,
        product.small_rec_3,
        product.pop_recommend1,
        product.pop_recommend2,
        product.pop_recommend3
    };

            var recommendedProducts = new List<Lego>();
            foreach (var id in recommendationIDs)
            {
                var recommendedProduct = await _repo.Legos.FirstOrDefaultAsync(p => p.product_ID == id);
                if (recommendedProduct != null)
                {
                    recommendedProducts.Add(recommendedProduct);
                }
            }

            var viewModel = new ProductViewModel
            {
                MainProduct = product,
                Recommendations = recommendedProducts
            };

            return View(viewModel);
        }


        public async Task<IActionResult> Legostore(string? legoCategory, int pageNum=1, int pageSize=5)
        {
            // Check if the user is authenticated
            //if (User.Identity.IsAuthenticated)
            //{
            //    AppUser user = await userManager.GetUserAsync(HttpContext.User);
            //}

            var legos = new LegosListViewModel
            {
                Legos = _repo.Legos
                    .Where(x => x.category == legoCategory || legoCategory == null)
                    .OrderBy(x => x.name)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize),

                PaginationInfo = new PaginationInfo
                {
                    CurrentPage = pageNum,
                    ItemsPerPage = pageSize,
                    TotalItems = legoCategory == null ? _repo.Legos.Count() : _repo.Legos.Where(x => x.category == legoCategory).Count()
                },

                CurrentLegoCategory = legoCategory,
                CurrentPageSize = pageSize
            };

            return View(legos);
        }

        public async Task<IActionResult> Privacy()
        {
            return View();
        }

        public async Task<IActionResult> AboutUs()
        {
            return View();
        }
    }
}