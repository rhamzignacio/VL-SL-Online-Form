using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VL_SL_Online_Form.Models;
using System.Data.Entity;

namespace VL_SL_Online_Form.Services
{
    public class LeaveService
    {
        public static int GetMinDateForLeave(Guid _leaveID ,out string message)
        {
            try
            {
                message = "";

                using (var db = new SLVLOnlineEntities())
                {
                    var leave = db.LeaveType.FirstOrDefault(r => r.ID == _leaveID);

                    if (leave != null)
                        return leave.DaysBeforeFilling;
                    else
                        return 0;
                }
            }
            catch(Exception error)
            {
                message = error.Message;

                return 0;
            }
        }

        public static List<LeaveTypeDropdownModel> GetLeaveType(out string message)
        {
            try
            {
                message = "";

                using (var db = new SLVLOnlineEntities())
                {
                    var query = from l in db.LeaveType 
                                orderby l.Description ascending
                                select new LeaveTypeDropdownModel
                                {
                                    Label = l.Description,
                                    Value = l.ID
                                };

                    return query.ToList();
                }
            }
            catch(Exception error)
            {
                message = error.Message;

                return null;
            }
        }

        public static List<LeaveFormModel> GetForApproval (out string message)
        {
            try
            {
                message = "";

                using (var db = new SLVLOnlineEntities())
                {
                    var leave = from l in db.LeaveForm
                                join u in db.UserAccount on l.CreatedBy equals u.ID
                                join g in db.ApproverGroup on u.DeptID equals g.ID
                                join t in db.LeaveType on l.Type equals t.ID
                                where g.FirstApprover == UniversalHelpers.CurrentUser.ID ||
                                g.SecondApprover == UniversalHelpers.CurrentUser.ID
                                orderby l.StartDate descending
                                select new LeaveFormModel
                                {
                                    ID = l.ID,
                                    StartDate = l.StartDate,
                                    EndDate = l.EndDate,
                                    CreatedBy = l.CreatedBy,
                                    CreatedDate = l.CreatedDate,
                                    Reason = l.Reason,
                                    Status = l.Status,
                                    Type = l.Type,
                                    ShowCreatedBy = u.FirstName + " " + u.LastName,
                                    DeclineReason = l.DeclineReason,
                                    ShowType = t.Description
                                };

                    return leave.ToList();
                }
            }
            catch(Exception error)
            {
                message = error.Message;

                return null;
            }
        }

        public static List<LeaveFormModel> GetLeavePerUser(out string message)
        {
            try
            {
                message = "";

                using (var db = new SLVLOnlineEntities())
                {
                    var leave = from l in db.LeaveForm
                                join u in db.UserAccount on l.CreatedBy equals u.ID
                                join t in db.LeaveType on l.Type equals t.ID
                                where l.CreatedBy == UniversalHelpers.CurrentUser.ID && l.Status != "X"
                                orderby l.StartDate descending
                                select new LeaveFormModel
                                {
                                    ID = l.ID,
                                    StartDate = l.StartDate,
                                    EndDate = l.EndDate,
                                    CreatedBy = l.CreatedBy,
                                    CreatedDate = l.CreatedDate,
                                    Reason = l.Reason,
                                    Status = l.Status,
                                    Type = l.Type,
                                    ShowCreatedBy = u.FirstName + " " + u.LastName,
                                    DeclineReason = l.DeclineReason,
                                    ShowType = t.Description
                                };
                    return leave.ToList();
                }
            }
            catch(Exception error)
            {
                message = error.Message;

                return null;
            }
        }
        public static void SaveUpdate(LeaveFormModel _leave, out string message)
        {
            try
            {
                message = "";

                var leaveDays = _leave.EndDate.Subtract(_leave.StartDate).TotalDays + 1;


                using (var db = new SLVLOnlineEntities())
                {
                    if(_leave.ID == Guid.Empty || _leave.ID == null)
                    {          
                        var leaveType = db.LeaveType.FirstOrDefault(r => r.ID == _leave.Type);

                        if (leaveType.Type == "SL")
                        { 
                            if(UniversalHelpers.CurrentUser.SickLeaveCount < (leaveDays * double.Parse(leaveType.LeaveDeduction.ToString())))
                            {
                                message = "Insufficient Sick Leave Credit";
                            }
                        }
                        else if(leaveType.Type == "VL" || leaveType.Type == "EL")
                        {
                            if (UniversalHelpers.CurrentUser.VacationLeaveCount < (leaveDays * double.Parse(leaveType.LeaveDeduction.ToString())))
                            {
                                message = "Insufficient Vacation Leave Credit";
                            }
                        }

                        Guid? CreatedBy = Guid.Empty;

                        if (_leave.FileForUser == null || _leave.FileForUser == Guid.Empty)
                            CreatedBy = UniversalHelpers.CurrentUser.ID;
                        else
                            CreatedBy = _leave.FileForUser;

                        LeaveForm newLeave = new LeaveForm
                        {
                            ID = Guid.NewGuid(),
                            CreatedBy = CreatedBy,
                            CreatedDate = DateTime.Now,
                            EndDate = _leave.EndDate,
                            Reason = _leave.Reason,
                            StartDate = _leave.StartDate,
                            Status = "P",
                            Type = _leave.Type
                        };

                        db.Entry(newLeave).State = EntityState.Added;

                        db.SaveChanges();

                        //========FIRST APPROVER EMAIL==============
                        EmailService.SendEmail("Leave For Approval", UniversalHelpers.CurrentUser.FirstName + " " + UniversalHelpers.CurrentUser.LastName + " filed " +
                            _leave.ShowType + " and waiting for your approval", UniversalHelpers.CurrentUser.FirstApproverEmail);

                        //========SECOND APPROVER EMAIL=============
                        EmailService.SendEmail("Leave For Approval", UniversalHelpers.CurrentUser.FirstName + " " + UniversalHelpers.CurrentUser.LastName + " filed " +
                            _leave.ShowType + " and waiting for your approval", UniversalHelpers.CurrentUser.SecondApproverEmail);

           
                    }
                    else
                    {
                        var leave = db.LeaveForm.FirstOrDefault(r => r.ID == _leave.ID);

                        var leaveType = db.LeaveType.FirstOrDefault(r => r.ID == _leave.Type);

                        if (leave != null)
                        {
                            var user = db.UserAccount.FirstOrDefault(r => r.ID == leave.CreatedBy);

                            if (_leave.Status == "D")
                            {
                                EmailService.SendEmail("Filed Leave Declined", "Your filed Leave has been declined by " + UniversalHelpers.CurrentUser.FirstName + " "
                                    + UniversalHelpers.CurrentUser.LastName, user.Email);
                            }
                            else if (_leave.Status == "A")
                            {
                                EmailService.SendEmail("Filed Leave Approved", "Your filed Leave has been approved by " + UniversalHelpers.CurrentUser.FirstName + " "
                                    + UniversalHelpers.CurrentUser.LastName, user.Email);

                                if (leaveType.Type == "SL")
                                    user.SickLeaveCount = user.SickLeaveCount - (leaveDays * double.Parse(leaveType.LeaveDeduction.ToString()));
                                else if (leaveType.Type == "VL" || leaveType.Type == "EL")
                                    user.VacationLeavCount = user.VacationLeavCount - (leaveDays * double.Parse(leaveType.LeaveDeduction.ToString()));
                            }
                            else if (_leave.Status == "X")
                            {
                                EmailService.SendEmail("Filed Leave of " + UniversalHelpers.CurrentUser.FirstName + " has been canceled", "Filed Leave of " +
                                    UniversalHelpers.CurrentUser.FirstName + " " + UniversalHelpers.CurrentUser.LastName + " has been canceled for some reasons", UniversalHelpers.CurrentUser.FirstApproverEmail);

                                EmailService.SendEmail("Filed Leave of " + UniversalHelpers.CurrentUser.FirstName + " has been canceled", "Filed Leave of " +
                                   UniversalHelpers.CurrentUser.FirstName + " " + UniversalHelpers.CurrentUser.LastName + " has been canceled for some reasons", UniversalHelpers.CurrentUser.SecondApproverEmail);

                                if(leave.Status == "A")
                                {
                                    if (leaveType.Type == "SL")
                                        user.SickLeaveCount = user.SickLeaveCount + (leaveDays * double.Parse(leaveType.LeaveDeduction.ToString()));
                                    else if (leaveType.Type == "VL" || leaveType.Type == "EL")
                                        user.VacationLeavCount = user.VacationLeavCount + (leaveDays * double.Parse(leaveType.LeaveDeduction.ToString()));
                                }
                            }

                            leave.Status = _leave.Status;

                            leave.DeclineReason = _leave.DeclineReason;

                            db.Entry(leave).State = EntityState.Modified;

                            db.SaveChanges();
                        }
                    }
                   
                }
            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }
    }
}