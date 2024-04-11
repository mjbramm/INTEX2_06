using INTEX2_06.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using INTEX2_06.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

namespace INTEX2_06.Controllers
{
    public class HomeController : Controller
    {
        private ILegoRepository _repo;
        private AppUser _appUser;
        

        public HomeController(ILegoRepository temp, AppUser user) //UserManager<AppUser> userMgr
        {
            _repo = temp;
            _appUser = user;
        }


        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var ordersAndLineItems = (from order in _repo.Orders
                                          join lineItem in _repo.LineItems on order.transaction_ID equals lineItem.transaction_ID
                                          where order.customer_ID == _appUser.CustomerID
                                          orderby order.date descending
                                          select new
                                          {
                                              Order = order,
                                              LineItem = lineItem
                                          }).FirstOrDefault();

                var productID = ordersAndLineItems.LineItem.product_ID;

                var product = await _repo.Legos.FirstOrDefaultAsync(p => p.product_ID == productID);

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

                //var legos = new LegosListViewModel
                //{
                //    Legos = _repo.Legos
                //        .Where(x => x.product_ID == productID)    
                //        .OrderByDescending(x => x.avg_rating)
                //        .Take(9)
                //};

                //var viewModel = new OrderAndLineItemViewModel
                //{
                //    Order = ordersAndLineItems?.Order,
                //    LineItem = ordersAndLineItems?.LineItem
                //};
                //var orders = new OrderListViewModel
                //{
                //    Orders = _repo.Orders.Where(x => x.customer_ID == _appUser.CustomerID).OrderByDescending(x => x.date).Take(1)
                //};
                //var lineItems = new LineItemListViewModel
                //{
                //    LineItems = _repo.LineItems.Where(x => x.transaction_ID == Orders.transaction_ID).Take(1)
                //};

                return View(viewModel);

            }
            else
            {
                var legos = new LegosListViewModel
                {
                    Legos = _repo.Legos
                        .OrderByDescending(x => x.avg_rating)
                        .Take(9)
                };
                return View(legos);
            }

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

        public async Task<IActionResult> PaymentInfo()
        {
            return View();
        }

        public async Task<IActionResult> OrderConfirmation()
        {
            return View();
        }

        public async Task<IActionResult> OrderUnderReview()
        {
            return View();
        }
    }
}