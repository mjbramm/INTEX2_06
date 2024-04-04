using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using INTEX2_06.Models;
using INTEX2_06.Infrastructure;

namespace INTEX2_06.Pages
{
    public class CartModel : PageModel
    {

        private IBookRepository _repo;

        public CartModel(IBookRepository temp) 
        {
            _repo = temp;
        }

        public Cart? Cart { get; set; }
        public string ReturnUrl { get; set; } = "/";



        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
            Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
        }

        public IActionResult OnPost(int bookId, string returnUrl) 
        {
            Book boo = _repo.Books
                .FirstOrDefault(x => x.BookId == bookId);

            if (boo != null)
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.AddItem(boo, 1);
                HttpContext.Session.SetJson("cart", Cart);
            }

            return RedirectToPage(new {returnUrl = returnUrl});
        }
    }
}
