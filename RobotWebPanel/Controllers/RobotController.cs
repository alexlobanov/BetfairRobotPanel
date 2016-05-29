using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using RobotWebPanel.Models;
using RobotWebPanel.Models.ViewModel;

namespace RobotWebPanel.Controllers
{
    [System.Web.Mvc.Authorize]
    public class RobotController : Controller
    {
        readonly ModelContext _dbContext = new ModelContext();

        // GET: Robot
        public ActionResult Index()
        {
            var allUsers = _dbContext.RobotUsers;
            return PartialView(allUsers);
        }

        public ActionResult ListAdminUsers()
        {
            var adminUsers = _dbContext.AspNetUsers;
            return PartialView(adminUsers);
        }

        public ActionResult RegistrationUsers()
        {
            Response.SuppressFormsAuthenticationRedirect = true;
            return View();
        }
        
        [System.Web.Mvc.HttpPost]
        public ActionResult RegistrationUsers(RobotRegistationViewModel model)
        {
            if (model.AccessDate.CompareTo(DateTime.Now.Date) < 0)
            {
                ModelState.AddModelError("", "Ошибка в дате доступа");
            }
            if (ModelState.IsValid)
            {
                var robotUser = new RobotModel
                {
                    UserName = model.UserName,
                    AccessDate = model.AccessDate,
                    CountStartsProgramm = 0,
                    CreationDate = DateTime.Now,
                    LastAccessDate = DateTime.Now,
                    LastBalance = 0,
                    UniqGuid = new Guid()
                };
                _dbContext.RobotUsers.Add(robotUser);
                _dbContext.SaveChanges();
            }
            var allUsers = _dbContext.RobotUsers;
            return PartialView("Index",allUsers);
        }

        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> EditRobotUser(int userId, string userName)
        {
            if ((userId == 0) && (string.IsNullOrEmpty(userName)))
                return RedirectToAction("Index","Robot");
            var user = await _dbContext.RobotUsers.FirstOrDefaultAsync(model => (model.UserName == userName) && (model.Id == userId));
            var userViewModel = new RobotRegistationViewModel();
            userViewModel.UserName = user.UserName;
            userViewModel.AccessDate = user.AccessDate;
            userViewModel.CreationTime = user.CreationDate;
            return PartialView(user);
        }


        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> EditRobotUser(RobotRegistationViewModel robotUser)
        {
            var user =
                await
                    _dbContext.RobotUsers.FirstOrDefaultAsync(
                        model =>
                            (model.UserName == robotUser.UserName));
            if (user != null)
                user.AccessDate = robotUser.AccessDate;
            _dbContext.Entry(user).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index","Robot");
        }

        [System.Web.Mvc.HttpDelete]
        public async Task<ActionResult> DeleteUser([FromBody] string userName, [FromBody] string userId)
        {
            var userIdNumber = 0;
            int.TryParse(userId, out userIdNumber);
            var deleteUser =
                await
                    _dbContext.RobotUsers.FirstAsync(
                        model => (model.UserName == userName) && (model.Id == userIdNumber));
            if (deleteUser == null)
                ModelState.AddModelError("", "Invalid delete info.");
            else
            {
                _dbContext.RobotUsers.Remove(deleteUser);
                await _dbContext.SaveChangesAsync();
            }
            return RedirectToAction("Index","Robot");
        }


    }
}