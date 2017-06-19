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
        public ActionResult GroupApprover()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetGroupApproverDropDown()
        {
            string serverResponse = "";

            var group = GroupApproverService.GetGroupDropDown(out serverResponse);

            return Json(new { errorMessage = serverResponse, group = group });
        }

        [HttpPost]
        public JsonResult GetGroupMember(Guid ID)
        {
            string serverResponse = "";

            var member = GroupApproverService.GetMembers(ID, out serverResponse);

            return Json(new { errorMessage = serverResponse, member = member });
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
        public JsonResult SaveGroupMember(GroupApproverMemberModel member)
        {
            string serverResponse = "";

            if (member != null)
                GroupApproverService.SaveMember(member, out serverResponse);

            return Json(serverResponse);
        }
    }
}