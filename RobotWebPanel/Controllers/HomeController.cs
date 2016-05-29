using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RobotWebPanel.Models;

namespace RobotWebPanel.Controllers
{
    public class HomeController : Controller
    {
        readonly ModelContext _dbContext = new ModelContext();
        // GET: Home
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

    }
}