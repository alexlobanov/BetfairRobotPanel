using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using RobotWebPanel.Models;
using Microsoft.Owin.Host.SystemWeb;
using Microsoft.Owin.Security;
using RobotWebPanel.App_Start;
using RobotWebPanel.Enums;

namespace RobotWebPanel.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public AccountController()
        {
        }
        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        // GET: Account
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Robot");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [Authorize]
        public ActionResult Registration()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> DeleteAdmin(string userName)
        {
            var context = new ModelContext();
            var userDelete = await context.AspNetUsers.FirstOrDefaultAsync(users => users.UserName == userName);
            if (userDelete == null)
                return RedirectToAction("Index", "Robot");
            context.AspNetUsers.Remove(userDelete);
            await context.SaveChangesAsync();
            return RedirectToAction("Index", "Robot");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("Index", "Account");
                case SignInStatus.LockedOut:
                    return RedirectToAction("Index", "Account");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model); 
            }
        }

        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Login");
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Registration(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                AspNetUsers user = new AspNetUsers
                {
                    UserName = model.Email,
                    Email = model.Email,
                    AccessLevel = EaccessType.AdminAccess,
                    AccessDateTime = DateTime.Now,
                    CreationDateTime = DateTime.Now
                };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(model);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}