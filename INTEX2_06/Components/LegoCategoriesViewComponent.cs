using Microsoft.AspNetCore.Mvc;
using INTEX2_06.Models;

namespace INTEX2_06.Components
{
    public class LegoCategoriesViewComponent : ViewComponent
    {

        private ILegoRepository _legoRepo;
        //Constructor 
        public LegoCategoriesViewComponent(ILegoRepository temp) 
        { 
            _legoRepo = temp;

        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedLegoCategory = RouteData?.Values["legoCategory"];

            var legoCategories = _legoRepo.Legos
                .Select(x => x.category)
                .Distinct()
                .OrderBy(x => x);

            return View(legoCategories);

        }
    }
}
