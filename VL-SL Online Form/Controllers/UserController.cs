
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VL_SL_Online_Form.Services;
using VL_SL_Online_Form.Models;

namespace VL_SL_Online_Form.Controllers
{
    public class UserController : Controller
    {
        public ActionResult HRModule()
        {
            return View();
        }

        public JsonResult GetAllUser()
        {
            string serverResponse = "";

            var users = UserService.GetAll(out serverResponse);

            return Json(new { message = serverResponse, users = users });
        }

        public JsonResult SaveUser(UserModel user)
        {
            string serverResponse = "";

            UserService.Save(user, out serverResponse);

            return Json(new { message = serverResponse });
        }
    }
}