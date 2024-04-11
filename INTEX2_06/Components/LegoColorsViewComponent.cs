using Microsoft.AspNetCore.Mvc;
using INTEX2_06.Models;

namespace INTEX2_06.Components
{
    public class LegoColorsViewComponent : ViewComponent
    {

        private ILegoRepository _legoRepo;
        //Constructor 
        public LegoColorsViewComponent(ILegoRepository temp)
        {
            _legoRepo = temp;

        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedLegoColor = RouteData?.Values["legoColor"];

            var legoColors = _legoRepo.Legos
                .Select(x => x.primary_color)
                .Distinct()
                .OrderBy(x => x);

            return View(legoColors);

        }
    }
}
