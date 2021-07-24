using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class AccountsController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly AttendanceDbContext _context;

        private AttendanceDbContext db = new AttendanceDbContext();

        //private readonly App

        public AccountsController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager1,AttendanceDbContext context)
        {
            this._userManager = userManager;
            this._signInManager = signInManager1;
            this._context = context;
        }
         public AccountsController()
        {
                
        }
        // GET: Accounts
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(AdminRegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
               var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

       [HttpGet]
        public ActionResult Login(string returnUrl="")
        {
            var model = new LoginViewModel { ReturnUrl = returnUrl };

            return View(model);
        }


        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = db.Employees.Where(x => x.UserName == model.EmailAddress &&  x.Password == model.Password).FirstOrDefault();
                    //await _signInManager.PasswordSignInAsync(model.EmailAddress,
                   //model.Password, model.RememberMe, false);

                if (result!=null)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Attendances");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "User does not have admin rights");
                    return View(model);
                }
            }
            ModelState.AddModelError("", "Invalid login attempt");
            return View(model);
        }
    }
}