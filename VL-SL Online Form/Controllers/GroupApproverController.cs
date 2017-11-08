using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VL_SL_Online_Form.Services;
using VL_SL_Online_Form.Models;

namespace VL_SL_Online_Form.Controllers
{
    public class GroupApproverController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetAllGroup()
        {
            string serverResponse = "";

            var group = GroupApproverService.GetAllGroup(out serverResponse);

            return Json(new { errorMessage = serverResponse, group = group });
        }

        [HttpPost]
        public JsonResult GetGroupApproverDropDown()
        {
            string serverResponse = "";

            var group = GroupApproverService.GetGroupDropDown(out serverResponse);

            return Json(new { errorMessage = serverResponse, group = group });
        }

        [HttpPost]
        public JsonResult GetUserDropDown()
        {
            string serverResponse = "";

            var user = UniversalService.GetUserDropDown(out serverResponse);

            return Json(new { errorMessage = serverResponse, user = user });
        }

        [HttpPost]
        public JsonResult SaveGroupApprover(GroupApproverModel group)
        {
            string serverResponse = "";

            if (group != null)
                GroupApproverService.SaveGroup(group, out serverResponse);

            return Json(serverResponse);
        }

        [HttpPost]
        public JsonResult DeleteGroupApprover(GroupApproverModel group)
        {
            string serverResponse = "";

            if(group != null)
            {
                group.Status = "X";

                GroupApproverService.SaveGroup(group, out serverResponse);
            }

            return Json(serverResponse);
        }
    }
}