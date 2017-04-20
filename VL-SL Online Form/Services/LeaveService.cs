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
        public static void ChangeStatus(Guid _ID, string _status, out string message)
        {
            try
            {
                message = "";

                using (var db = new SLVLOnlineEntities())
                {
                    var leave = db.LeaveForm.FirstOrDefault(r => r.ID == _ID);

                    if(leave != null)
                    {
                        leave.Status = _status;

                        db.Entry(leave).State = EntityState.Modified;

                        db.SaveChanges();
                    }
                }
            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }

        public static void Save(LeaveFormModel _leave, out string message)
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
                            Status = _leave.Status,
                            Type = _leave.Type
                        };

                        db.Entry(newLeave).State = EntityState.Added;
                    }
                    else
                    {
                        var leave = db.LeaveForm.FirstOrDefault(r => r.ID == _leave.ID);

                        if(leave != null)
                        {
                            if (_leave.Status == "X")
                                db.Entry(leave).State = EntityState.Deleted;
                            else
                            {
                                leave.CreatedBy = UniversalHelpers.CurrentUser.ID;
                                leave.CreatedDate = DateTime.Now;
                                leave.Reason = _leave.Reason;
                                leave.StartDate = _leave.StartDate;
                                leave.EndDate = _leave.EndDate;
                                leave.Type = _leave.Type;

                                db.Entry(leave).State = EntityState.Modified;
                            }
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