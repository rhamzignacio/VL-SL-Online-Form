using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VL_SL_Online_Form.Models;
using VL_SL_Online_Form.Services;

namespace VL_SL_Online_Form.Controllers
{
    public class CalendarController : Controller
    {
        // GET: Calendar
        public ActionResult Schedule()
        {
            return View();
        }

        public ActionResult CalendarSchedule()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetSched()
        {
            string serverResponse = "";

            var sched = CalendarService.GetCalendarSchedule(out serverResponse);

            return Json(new { errorMessage = serverResponse, schedule = sched });
        }

        [HttpPost]
        public ActionResult SaveHoliday(CalendarModel holiday)
        {
            string serverResponse = "";

            if (holiday != null)
                CalendarService.Save(holiday, out serverResponse);

            return Json(new { errorMessage = serverResponse });
        }

        [HttpPost]
        public ActionResult GetHoliday(DateTime? start = null, DateTime? end = null)
        {
            string serverResponse = "";

            var holiday = CalendarService.GetHoliday(out serverResponse, start, end);

            return Json(new { errorMessage = serverResponse, holiday = holiday });
        }

        [HttpPost]
        public ActionResult DeleteHoliday(CalendarModel holiday)
        {
            string serverResponse = "";

            if(holiday != null)
            {
                holiday.Status = "X";

                CalendarService.Save(holiday, out serverResponse);
            }

            return Json(new { errorMessage = serverResponse });
        }
    }
}