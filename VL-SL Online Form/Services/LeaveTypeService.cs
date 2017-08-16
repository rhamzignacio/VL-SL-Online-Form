using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VL_SL_Online_Form.Models;
using System.Data.Entity;

namespace VL_SL_Online_Form.Services
{
    public class LeaveTypeService
    {
        public static List<LeaveTypeModel> GetAll(out string message)
        {
            try
            {
                message = "";

                using (var db = new SLVLOnlineEntities())
                {
                    var query = from l in db.LeaveType
                                select new LeaveTypeModel
                                {
                                    ID = l.ID,
                                    CreateDate = l.CreatedDate,
                                    CreatedBy = l.CreatedBy,
                                    DaysBeforeFilling = l.DaysBeforeFilling,
                                    Description = l.Description,
                                    Type = l.Type
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

        public static void Save(LeaveTypeModel _leaveType, out string message)
        {
            try
            {
                message = "";

                using (var db = new SLVLOnlineEntities())
                {
                    if(_leaveType.ID == null || _leaveType.ID == Guid.Empty)
                    {
                        LeaveType newLeaveType = new LeaveType
                        {
                            ID = Guid.NewGuid(),
                            Description = _leaveType.Description,
                            CreatedBy = UniversalHelpers.CurrentUser.ID,
                            CreatedDate = DateTime.Now,
                            DaysBeforeFilling = _leaveType.DaysBeforeFilling,
                            Type = _leaveType.Type
                        };

                        db.Entry(newLeaveType).State = EntityState.Added;
                    }
                    else //Update
                    {
                        var leave = db.LeaveType.FirstOrDefault(r => r.ID == _leaveType.ID);

                        if(leave != null)
                        {
                            leave.Description = _leaveType.Description;
                            leave.CreatedBy = UniversalHelpers.CurrentUser.ID;
                            leave.CreatedDate = DateTime.Now;
                            leave.DaysBeforeFilling = _leaveType.DaysBeforeFilling;
                            leave.Type = _leaveType.Type;

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