using INTEX2_06.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using INTEX2_06.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace INTEX2_06.Controllers
{
    public class HomeController : Controller
    {
        private ILegoRepository _repo;
        private UserManager<AppUser> _userManager;

        public HomeController(ILegoRepository temp, UserManager<AppUser> userMgr)
        {
            _userManager = userMgr;
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

        [HttpGet]
        public async Task<IActionResult> PaymentInfo()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PaymentInfo(CreateOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser currentUser = await _userManager.GetUserAsync(User);

                Order order = new Order
                {
                    transaction_ID = new Random().Next(100000, 999999),
                    date = DateTime.Now.Date,
                    day_of_week = DateTime.Now.ToString("ddd"),
                    time = DateTime.Now.Hour,
                    entry_mode = "CVC",
                    amount = model.amount,
                    type_of_transaction = "Online",
                    country_of_transaction = "USA",
                    shipping_address = model.shipping_address,
                    bank = model.bank,
                    type_of_card = model.type_of_card,
                    UserID = currentUser.Id
                };

                await _repo.AddOrder(order);

                return RedirectToAction("OrderConfirmation", "Home");
            }
            
            return View(model);
        }

        public async Task<IActionResult> OrderConfirmation()
        {
            return View();
        }

        public async Task<IActionResult> OrderUnderReview()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmOrderDetails()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmOrderCancel()
        {
            return View();
        }
    }
}