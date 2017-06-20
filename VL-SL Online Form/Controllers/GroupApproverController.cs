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
        public JsonResult GetGroupMember(Guid ID)
        {
            string serverResponse = "";

            var member = GroupApproverService.GetMembers(ID, out serverResponse);

            var group = GroupApproverService.GetGroup(ID, out serverResponse);

            return Json(new { errorMessage = serverResponse, member = member, group = group });
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

        [HttpPost]
        public JsonResult SaveGroupMember(GroupApproverMemberModel member)
        {
            string serverResponse = "";

            if (member != null)
                GroupApproverService.SaveMember(member, out serverResponse);

            return Json(serverResponse);
        }

        [HttpPost]
        public JsonResult DeleteGroupMember(GroupApproverMemberModel member)
        {
            string serverResponse = "";

            if(member != null)
            {
                member.Status = "X";

                GroupApproverService.SaveMember(member, out serverResponse);
            }

            return Json(serverResponse);
        }
    }
}