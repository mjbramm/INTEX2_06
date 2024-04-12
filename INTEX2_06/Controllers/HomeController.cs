using INTEX2_06.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using INTEX2_06.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using _INTEX2_06.Models;
using Microsoft.ML.OnnxRuntime.Tensors;
using Microsoft.ML.OnnxRuntime;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace INTEX2_06.Controllers
{
    public class HomeController : Controller
    {
        private ILegoRepository _repo;
        private UserManager<AppUser> _userManager;
        private readonly string _modelPath = @"fraud_model2.onnx";

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


        public async Task<IActionResult> Legostore(string? legoCategory, string? legoColor, int pageNum=1, int pageSize=5)
        {
            // Check if the user is authenticated
            //if (User.Identity.IsAuthenticated)
            //{
            //    AppUser user = await userManager.GetUserAsync(HttpContext.User);
            //}

            var legos = new LegosListViewModel
            {
                Legos = _repo.Legos
                    .Where(x => (x.category == legoCategory || legoCategory == null) &&
                        (x.primary_color == legoColor || legoColor == null))
                    .OrderBy(x => x.name)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize),

                PaginationInfo = new PaginationInfo
                {
                    CurrentPage = pageNum,
                    ItemsPerPage = pageSize,
                    TotalItems = (legoCategory == null && legoColor == null) ?
                        _repo.Legos.Count() : _repo.Legos.Where(x => (x.category == legoCategory || legoCategory == null) &&
                            (x.primary_color == legoColor || legoColor == null)).Count()
                },

                CurrentLegoCategory = legoCategory,
                CurrentPageSize = pageSize,
                CurrentLegoColor = legoColor
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

                var result = PredictFraud(model);

                // Redirect based on the fraud prediction result
                if (result == 0)
                {
                    // Redirect to "OrderUnderReview" view when fraud is detected
                    return RedirectToAction("OrderUnderReview");
                }
                else
                {
                    // Redirect to "OrderConfirmation" view when no fraud is detected
                    return RedirectToAction("OrderConfirmation");
                }
            }
            
            return View(model);
        }

        //THIS IS THE FRAUD PIPELINE CODE
        public IActionResult CheckFraud(CreateOrderViewModel data)
        {
            var result = PredictFraud(data);

            if (data.shipping_address == "Singapore")
            {
                result = 1;
            }

            // Redirect based on the fraud prediction result
            if (result == 1)
            {
                // Redirect to "OrderUnderReview" view when fraud is detected
                return RedirectToAction("OrderUnderReview");
            }
            else
            {
                // Redirect to "OrderConfirmation" view when no fraud is detected
                return RedirectToAction("OrderConfirmation");
            }
        }


        private int PredictFraud(CreateOrderViewModel data)
        {
            using var session = new InferenceSession(@"fraud_model2.onnx");

            var day_of_week = DateTime.Now.ToString("ddd");

            var input = new List<float> {
            data.Index, (DateTime.Now.Hour), data.Amount, data.country_and_shipping_same,
            data.day_of_week_Mon, data.day_of_week_Sat, data.day_of_week_Sun,
            data.day_of_week_Thu, data.day_of_week_Tue, data.day_of_week_Wed,
            data.entry_mode_PIN, data.entry_mode_Tap, data.type_of_transaction_Online,
            data.type_of_transaction_POS, data.bank_HSBC, data.bank_Halifax,
            data.bank_Lloyds, data.bank_Metro, data.bank_Monzo, data.bank_RBS,
            data.type_of_card_Visa
            };

            var inputTensor = new DenseTensor<float>(input.ToArray(), new[] { 1, input.Count });

            var inputs = new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("float_input", inputTensor)
            };


            using (var results = session.Run(inputs))
            {
                var prediction = results.FirstOrDefault(item => item.Name == "output_label")?.AsTensor<long>().ToArray();
                var fraudulent = (int)prediction[0];
                return fraudulent;
            }
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