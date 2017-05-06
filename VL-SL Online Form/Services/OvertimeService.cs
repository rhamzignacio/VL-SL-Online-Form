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
                    }
                    else
                    {
                        var ot = db.OvertimeForm.FirstOrDefault(r => r.ID == _overtime.ID);

                        if (ot != null)
                        {
                            ot.Status = _overtime.Status;

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