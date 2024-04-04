using Microsoft.AspNetCore.Mvc;
using INTEX2_06.Models;

namespace INTEX2_06.Components
{
    public class BookCategoriesViewComponent : ViewComponent
    {

        private IBookRepository _bookRepo;
        //Constructor 
        public BookCategoriesViewComponent(IBookRepository temp) 
        { 
            _bookRepo = temp;

        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedBookCategory = RouteData?.Values["bookCategory"];

            var bookCategories = _bookRepo.Books
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x);

            return View(bookCategories);

        }
    }
}
