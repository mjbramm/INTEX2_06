using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace INTEX2_06.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public LoginController(
            UserManager<IdentityUser> userMgr,
            SignInManager<IdentityUser> signInMgr,
            RoleManager<IdentityRole> roleMgr)
        {
            userManager = userMgr;
            signInManager = signInMgr;
            roleManager = roleMgr;
        }
    }
}