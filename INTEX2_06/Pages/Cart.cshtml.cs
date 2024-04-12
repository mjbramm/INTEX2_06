using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using INTEX2_06.Models;
using INTEX2_06.Infrastructure;

namespace INTEX2_06.Pages
{
    public class CartModel : PageModel
    {
        private ILegoRepository _repo;

        public CartModel(ILegoRepository repo)
        {
            _repo = repo;
        }

        public Cart? Cart { get; set; }
        public string ReturnUrl { get; set; } = "/";

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
            Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
        }

        public IActionResult OnPost(int product_Id, string returnUrl)
        {
            Lego ego = _repo.Legos
                .FirstOrDefault(x => x.product_ID == product_Id);

            if (ego != null)
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.AddItem(ego, 1);
                HttpContext.Session.SetJson("cart", Cart);
            }

            return RedirectToPage(new { returnUrl = returnUrl});
        }

        [HttpPost]
        public IActionResult OnPostRemoveFromCart(int productId)
        {
            Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
            Lego legoToRemove = _repo.Legos.FirstOrDefault(x => x.product_ID == productId);

            if (legoToRemove != null)
            {
                Cart.RemoveLine(legoToRemove);
                HttpContext.Session.SetJson("cart", Cart);
            }

            return RedirectToPage();
        }
    }
}
