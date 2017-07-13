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
        public static List<LeaveTypeDropdownModel> GetLeaveType(out string message)
        {
            try
            {
                message = "";

                using (var db = new SLVLOnlineEntities())
                {
                    var query = from l in db.TypeOfLeave
                                orderby l.Arrangement ascending
                                select new LeaveTypeDropdownModel
                                {
                                    Label = l.Label,
                                    Value = l.Value
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
                                join m in db.ApproverGroupMember on l.CreatedBy equals m.UserID
                                join g in db.ApproverGroup on m.GroupID equals g.ID
                                join u in db.UserAccount on l.CreatedBy equals u.ID
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
                                    DeclineReason = l.DeclineReason
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
                                join u in db.UserAccount
                                on l.CreatedBy equals u.ID
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
                                    DeclineReason = l.DeclineReason
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

                using (var db = new SLVLOnlineEntities())
                {
                    if(_leave.ID == Guid.Empty || _leave.ID == null)
                    {
                        var leaveDays = _leave.EndDate.Subtract(_leave.StartDate).TotalDays + 1;

                        if (_leave.Type == "SL")
                        { 
                            if(UniversalHelpers.CurrentUser.SickLeaveCount < leaveDays)
                            {
                                message = "Insufficient Sick Leave Credit";
                            }
                        }
                        else if(_leave.Type == "VL" || _leave.Type == "EL")
                        {
                            if (UniversalHelpers.CurrentUser.VacationLeaveCount < leaveDays)
                            {
                                message = "Insufficient Vacation Leave Credit";
                            }
                        }
                        else if(_leave.Type == "SL-H")
                        {
                            if (UniversalHelpers.CurrentUser.SickLeaveCount < (leaveDays/2))
                            {
                                message = "Insufficient Sick Leave Credit";
                            }
                        }
                        else if (_leave.Type == "VL-H" || _leave.Type == "EL-H")
                        {
                            if (UniversalHelpers.CurrentUser.VacationLeaveCount < (leaveDays/2))
                            {
                                message = "Insufficient Vacation Leave Credit";
                            }
                        }

                        LeaveForm newLeave = new LeaveForm
                        {
                            ID = Guid.NewGuid(),
                            CreatedBy = UniversalHelpers.CurrentUser.ID,
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

                        if(leave != null)
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

                                if (leave.Type == "SL")
                                    user.SickLeaveCount = user.SickLeaveCount - 1;
                                else if (leave.Type == "SL-H")
                                    user.SickLeaveCount = user.SickLeaveCount -0.5;
                                else if (leave.Type == "VL" || leave.Type == "EL")
                                    user.VacationLeavCount = user.VacationLeavCount - 1;
                                else if (leave.Type == "VL-H" || leave.Type == "EL-H")
                                    user.VacationLeavCount = user.VacationLeavCount - 0.5;
                            }
                            else if (_leave.Status == "X")
                            {
                                EmailService.SendEmail("Filed Leave of " + UniversalHelpers.CurrentUser.FirstName + " has been canceled", "Filed Leave of " +
                                    UniversalHelpers.CurrentUser.FirstName + " " + UniversalHelpers.CurrentUser.LastName + " has been canceled for some reasons", UniversalHelpers.CurrentUser.FirstApproverEmail);

                                EmailService.SendEmail("Filed Leave of " + UniversalHelpers.CurrentUser.FirstName + " has been canceled", "Filed Leave of " +
                                   UniversalHelpers.CurrentUser.FirstName + " " + UniversalHelpers.CurrentUser.LastName + " has been canceled for some reasons", UniversalHelpers.CurrentUser.SecondApproverEmail);

                                if(leave.Status == "A")
                                {
                                    if (leave.Type == "SL")
                                        user.SickLeaveCount = user.SickLeaveCount + 1;
                                    else if (leave.Type == "SL-H")
                                        user.SickLeaveCount = user.SickLeaveCount + 0.5;
                                    else if (leave.Type == "VL" || leave.Type == "EL")
                                        user.VacationLeavCount = user.VacationLeavCount + 1;
                                    else if (leave.Type == "VL-H" || leave.Type == "EL-H")
                                        user.VacationLeavCount = user.VacationLeavCount + 0.5;
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