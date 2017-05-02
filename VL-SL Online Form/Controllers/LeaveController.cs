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

        [HttpPost]
        public JsonResult GetFiledForms()
        {
            string serverResponse = "";

            var filedLeave = LeaveService.GetLeavePerUser(out serverResponse);

            var filedOvertime = OvertimeService.GetOverTimePerUser(out serverResponse);

            return Json(new { leave = filedLeave, overtime = filedOvertime, errorMessage = serverResponse });
        }

        [HttpPost]
        public JsonResult SaveLeave(LeaveFormModel leave)
        {
            string serverResponse = "";

            if (leave != null)
                LeaveService.SaveUpdate(leave, out serverResponse);

            return Json(new { errorMessage = serverResponse });
        }

        [HttpPost]
        public JsonResult SaveOvertime (OvertimeFormModel overtime)
        {
            string serverResponse = "";

            if (overtime != null)
                OvertimeService.SaveUpdate(overtime, out serverResponse);

            return Json(new { errorMessage = serverResponse });
        }
    }
}