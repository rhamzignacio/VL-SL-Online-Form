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

        [HttpPost]
        public JsonResult SaveLeave(LeaveFormModel leave)
        {
            string serverResponse = "";

            if (leave != null)
                LeaveService.Save(leave, out serverResponse);

            return Json(new { errorMessage = serverResponse });
        }
    }
}