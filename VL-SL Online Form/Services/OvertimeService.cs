using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VL_SL_Online_Form.Models;
using System.Data.Entity;

namespace VL_SL_Online_Form.Services
{
    public class OvertimeService
    {
        public static List<OvertimeFormModel> GetForApproval(out string message)
        {
            try
            {
                message = "";

                using (var db = new SLVLOnlineEntities())
                {
                    var ot = from o in db.OvertimeForm
                             join u in db.UserAccount
                             on o.CreatedBy equals u.ID
                             orderby o.EffectiveDate descending
                             select new OvertimeFormModel
                             {
                                 ID = o.ID,
                                 EffectiveDate = o.EffectiveDate,
                                 Status = o.Status,
                                 StartTime = o.StartTime,
                                 EndTime = o.EndTime,
                                 CreatedBy = o.CreatedBy,
                                 CreatedDate = o.CreatedDate,
                                 Reason = o.Reason,
                                 ShowCreatedBy = u.FirstName + " " + u.LastName
                             };

                    return ot.ToList();
                }
            }
            catch(Exception error)
            {
                message = error.Message;

                return null;
            }
        }

        public static List<OvertimeFormModel> GetOverTimePerUser(out string message)
        {
            try
            {
                message = "";

                using (var db = new SLVLOnlineEntities())
                {
                    var ot = from o in db.OvertimeForm
                             join u in db.UserAccount
                             on o.CreatedBy equals u.ID
                             where o.CreatedBy == UniversalHelpers.CurrentUser.ID && o.Status != "X"
                             orderby o.EffectiveDate descending
                             select new OvertimeFormModel
                             {
                                 ID = o.ID,
                                 EffectiveDate = o.EffectiveDate,
                                 Status = o.Status,
                                 StartTime = o.StartTime,
                                 EndTime = o.EndTime,
                                 CreatedBy = o.CreatedBy,
                                 CreatedDate = o.CreatedDate,
                                 Reason = o.Reason,
                                 ShowCreatedBy = u.FirstName + " " + u.LastName
                             };

                    return ot.ToList();
                }
            }
            catch(Exception error)
            {
                message = error.Message;

                return null;
            }
        }

        public static void SaveUpdate(OvertimeFormModel _overtime, out string message)
        {
            try
            {
                message = "";

                using (var db = new SLVLOnlineEntities())
                {
                    if (_overtime.ID == Guid.Empty || _overtime.ID == null)
                    {
                        OvertimeForm newOvertime = new OvertimeForm
                        {
                            ID = Guid.NewGuid(),
                            EffectiveDate = _overtime.EffectiveDate,
                            StartTime = _overtime.StartTime,
                            EndTime = _overtime.EndTime,
                            CreatedBy = UniversalHelpers.CurrentUser.ID,
                            CreatedDate = DateTime.Now,
                            Reason = _overtime.Reason,
                            Status = "P"
                        };

                        db.Entry(newOvertime).State = EntityState.Added;

                        //========FIRST APPROVER EMAIL==============
                        EmailService.SendEmail("Overtime For Approval", UniversalHelpers.CurrentUser.FirstName + " " + UniversalHelpers.CurrentUser.LastName +
                            " filed overtime and waiting for your approval", UniversalHelpers.CurrentUser.FirstApproverEmail);

                        //========SECOND APPROVER EMAIL=============
                        EmailService.SendEmail("Overtime For Approval", UniversalHelpers.CurrentUser.FirstName + " " + UniversalHelpers.CurrentUser.LastName +
                            " filed overtime and waiting for your approval", UniversalHelpers.CurrentUser.SecondApproverEmail);
                    }
                    else
                    {
                        var ot = db.OvertimeForm.FirstOrDefault(r => r.ID == _overtime.ID);

                        if (ot != null)
                        {
                            var user = db.UserAccount.FirstOrDefault(r => r.ID == ot.CreatedBy);

                            ot.Status = _overtime.Status;

                            if(_overtime.Status == "D")
                            {
                                EmailService.SendEmail("Filed Overtime Declined", "Your filed overtime has been declined by " + UniversalHelpers.CurrentUser.FirstName + " "
                                    + UniversalHelpers.CurrentUser.LastName, user.Email);
                            }
                            else if (_overtime.Status == "A")
                            {
                                EmailService.SendEmail("Filed Overtime Approved", "Your filed overtime has been approved by " + UniversalHelpers.CurrentUser.FirstName + " "
                                    + UniversalHelpers.CurrentUser.LastName, user.Email);
                            }
                            else if(_overtime.Status == "X")
                            {
                                EmailService.SendEmail("Filed Overtime of " + UniversalHelpers.CurrentUser.FirstName + " has been canceled", "Filed overtime of " +
                                    UniversalHelpers.CurrentUser.FirstName + " " + UniversalHelpers.CurrentUser.LastName + " has been canceled for some reasons", UniversalHelpers.CurrentUser.FirstApproverEmail);

                                EmailService.SendEmail("Filed Overtime of " + UniversalHelpers.CurrentUser.FirstName + " has been canceled", "Filed overtime of " +
                                   UniversalHelpers.CurrentUser.FirstName + " " + UniversalHelpers.CurrentUser.LastName + " has been canceled for some reasons", UniversalHelpers.CurrentUser.SecondApproverEmail);
                            }

                            db.Entry(ot).State = EntityState.Modified;
                        }
                    }

                    db.SaveChanges();
                }
            }
            catch (Exception error)
            {
                message = error.Message;
            }
        }
    }
}