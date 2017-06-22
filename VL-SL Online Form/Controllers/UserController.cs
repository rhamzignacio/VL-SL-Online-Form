
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

        public ActionResult EmailNotificationAccount()
        {
            return View();
        }

        public ActionResult LeaveCredit()
        {
            return View();
        }

        [HttpPost]
        public JsonResult LeaveCreditPerEmployee(Guid ID, int VL, int SL)
        {
            string serverResponse = "";

            UserService.AddCreditPerEmployee(ID, VL, SL, out serverResponse);

            return Json(serverResponse);
        }

        [HttpPost]
        public JsonResult LeaveCreditPerGroup (Guid ID, int VL, int SL)
        {
            string serverResponse = "";

            UserService.AddCreditPerGroup(ID, VL, SL, out serverResponse);

            return Json(serverResponse);
        }

        [HttpPost]
        public JsonResult GetGroupMember(Guid ID)
        {
            string serverResponse = "";

            var users = UniversalService.GetSelectedGroup(ID, out serverResponse);

            return Json(new { errorMessage = serverResponse, users = users });
        }

        [HttpPost]
        public JsonResult GetSingleUser(Guid ID)
        {
            string serverResponse = "";

            var user = new UserModel();

            if(ID != null || ID != Guid.Empty)
                user = UniversalService.GetSelectedUser(ID, out serverResponse);

            return Json(new { errorMessage = serverResponse, user = user });
        }

        [HttpPost]
        public JsonResult GetUserDropdown()
        {
            string serverResponse = "";

            var user = UniversalService.GetUserDropDown(out serverResponse);

            return Json(new { errorMessage = serverResponse, user = user });
        }

        public JsonResult GetGroupDropdown()
        {
            string serverResponse = "";

            var group = UniversalService.GetGroupDropDown(out serverResponse);

            return Json(new { errorMessage = serverResponse, group = group });
        }

        [HttpPost]
        public JsonResult Init()
        {
            string serverResponse = "";

            var users = UserService.GetAll(out serverResponse);

            var userDropdown = UniversalService.GetUserDropDown(out serverResponse);

            return Json(new { message = serverResponse, users = users, userDropdown = userDropdown });
        }

        [HttpPost]
        public JsonResult SaveUser(UserModel user)
        {
            string serverResponse = "";

            UserService.Save(user, out serverResponse);

            return Json(new { message = serverResponse });
        }
    }
}