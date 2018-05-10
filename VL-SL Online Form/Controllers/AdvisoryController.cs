using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VL_SL_Online_Form.Services;
using VL_SL_Online_Form.Models;

namespace VL_SL_Online_Form.Controllers
{
    public class AdvisoryController : Controller
    {
        // GET: Advisory
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetAll()
        {
            var advisory = AdvisoryService.GetAdvisory(out string message);

            return Json(new { advisory, message });
        }

        [HttpPost]
        public JsonResult SaveAdvisory(AdvisoryModel advisory)
        {

            AdvisoryService.Save(advisory, out string message);

            return Json(message);
        }
    }
}