using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VL_SL_Online_Form.Models;
using VL_SL_Online_Form.Services;

namespace VL_SL_Online_Form.Controllers
{
    public class LeaveController : Controller
    {
        public ActionResult FileForm()
        {
            return View();
        }

        public ActionResult FiledForms()
        {
            return View();  
        }

        public ActionResult ForApproval()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetFiledForms()
        {
            string serverResponse = "";

            var filedLeave = LeaveService.GetLeavePerUser(out serverResponse);

            var filedOvertime = OvertimeService.GetOverTimePerUser(out serverResponse);

            return Json(new { leave = filedLeave, overtime = filedOvertime, errorMessage = serverResponse });
        }

        [HttpPost]
        public JsonResult GetForApproval()
        {
            string serverResponse = "";

            var leave = LeaveService.GetForApproval(out serverResponse);

            var ot = OvertimeService.GetForApproval(out serverResponse);

            return Json(new { errorMessage = serverResponse, leave = leave, ot = ot });
        }

        #region Overtime
        [HttpPost]
        public JsonResult SaveOvertime (OvertimeFormModel overtime)
        {
            string serverResponse = "";

            if (overtime != null)
                OvertimeService.SaveUpdate(overtime, out serverResponse);

            return Json(new { errorMessage = serverResponse });
        }

        [HttpPost]
        public JsonResult CancelOvertime(OvertimeFormModel overtime)
        {
            string serverResponse = "";

            overtime.Status = "X";

            if (overtime != null)
                OvertimeService.SaveUpdate(overtime, out serverResponse);

            return Json(new { errorMessage = serverResponse });
        }

        [HttpPost]
        public JsonResult DeclineOvertime(OvertimeFormModel overtime)
        {
            string serverResponse = "";

            overtime.Status = "D";

            if (overtime != null)
                OvertimeService.SaveUpdate(overtime, out serverResponse);

            if(serverResponse == "")
            {

            }

            return Json(new { errorMessage = serverResponse });
        }

        [HttpPost]
        public JsonResult ApproveOvertime(OvertimeFormModel overtime)
        {
            string serverResponse = "";

            overtime.Status = "A";

            if (overtime != null)
                OvertimeService.SaveUpdate(overtime, out serverResponse);

            return Json(new { errorMessage = serverResponse });
        }

        #endregion

        #region Leave

        [HttpPost]
        public JsonResult SaveLeave(LeaveFormModel leave)
        {
            string serverResponse = "";

            if (leave != null)
                LeaveService.SaveUpdate(leave, out serverResponse);

            return Json(new { errorMessage = serverResponse });
        }

        [HttpPost]
       public JsonResult ApproveLeave(LeaveFormModel leave)
        {
            string serverResponse = "";

            leave.Status = "A";

            if (leave != null)
                LeaveService.SaveUpdate(leave, out serverResponse);

            return Json(new { errorMessage = serverResponse });
        }

        [HttpPost]
        public JsonResult DeclineLeave(LeaveFormModel leave)
        {
            string serverResponse = "";

            leave.Status = "D";

            if (leave != null)
                LeaveService.SaveUpdate(leave, out serverResponse);

            return Json(new { errorMessage = serverResponse });
        }

        [HttpPost]
        public JsonResult CancelLeave(LeaveFormModel leave)
        {
            string serverResponse = "";

            leave.Status = "X";

            if (leave != null)
                LeaveService.SaveUpdate(leave, out serverResponse);

            return Json(new { errorMessage = serverResponse });
        }
        #endregion

    }
}