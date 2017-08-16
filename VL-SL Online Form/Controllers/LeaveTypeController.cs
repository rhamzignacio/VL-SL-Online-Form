﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VL_SL_Online_Form.Models;
using VL_SL_Online_Form.Services;

namespace VL_SL_Online_Form.Controllers
{
    public class LeaveTypeController : Controller
    {
        // GET: LeaveType
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetLeaveType()
        {
            string serverResponse = "";

            var type = LeaveTypeService.GetAll(out serverResponse);

            return Json(new { errorMessage = serverResponse, leaveType = type });
        }
    }
}