using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VL_SL_Online_Form.Models;
using VL_SL_Online_Form.Services;

namespace VL_SL_Online_Form.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetEmail()
        {
            string serverResponse = "";

            var email = UserService.GetEmail(out serverResponse);

            return Json(new { email = email, errorMessage = serverResponse });
        }

        [HttpPost]
        public JsonResult SaveEmail(EmailAccountModel email)
        {
            string serverResponse = "";

            if (email != null)
                UserService.SaveEmail(email, out serverResponse);

            return Json(serverResponse);
        }

        [HttpPost]
        public JsonResult Login(string username, string password)
        {
            string serverResponse = "";

            UserModel user = AccountService.ValidateLogin(username, password, out serverResponse);

            if(user != null)
            {
                AccountService.LoginToSession(user);
            }

            return Json(serverResponse);
        }

        [HttpPost]
        public JsonResult GetCurrentUser()
        {
            return Json(new { currentUser = UniversalHelpers.CurrentUser });
        }

        [HttpPost]
        public JsonResult Logout()
        {
            string serverResponse = "";

            AccountService.LogoutFromSession(out serverResponse);

            return Json(serverResponse);
        }
    }
}