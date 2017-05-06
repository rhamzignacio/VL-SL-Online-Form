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
                                    ShowCreatedBy = u.FirstName + " " + u.LastName
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
                    }
                    else
                    {
                        var leave = db.LeaveForm.FirstOrDefault(r => r.ID == _leave.ID);

                        if(leave != null)
                        {
                            leave.Status = _leave.Status;

                            db.Entry(leave).State = EntityState.Modified;
                        }
                    }

                    db.SaveChanges();
                }
            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }
    }
}