using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VL_SL_Online_Form.Models;
using System.Data.Entity;

namespace VL_SL_Online_Form.Services
{
    public class CalendarService
    {
        public static List<CalendarModel> GetHoliday(out string message, DateTime? start = null, DateTime? end = null)
        {
            try
            {
                message = "";

                using (var db = new SLVLOnlineEntities())
                {
                    var holiday = from h in db.Holiday
                                  select new CalendarModel
                                  {
                                      ID = h.ID,
                                      CreatedBy = h.CreatedBy,
                                      CreatedDate = h.CreatedDate,
                                      start = h.StartDate,
                                      end = h.EndDate,
                                      title = h.Description,
                                      Status = "Y"
                                  };

                    return holiday.ToList();
                }
            }
            catch(Exception error)
            {
                message = error.Message;

                return null;
            }
        }

        public static List<ScheduleModel> GetCalendarSchedule(out string message)
        {
            try
            {
                message = "";

                using (var db = new SLVLOnlineEntities())
                {
                    List<ScheduleModel> returnSchedule = new List<ScheduleModel>();

                    var group = db.ApproverGroup.Where(r => r.FirstApprover == UniversalHelpers.CurrentUser.ID || r.SecondApprover == UniversalHelpers.CurrentUser.ID);

                    group.ToList().ForEach(item =>
                    {
                        var query = from l in db.LeaveForm
                                    join m in db.ApproverGroupMember on l.CreatedBy equals m.UserID
                                    join u in db.UserAccount on m.UserID equals u.ID
                                    where m.GroupID == item.ID
                                    select new ScheduleModel
                                    {
                                        title = u.FirstName + " " + u.LastName + " " + "On Leave",
                                        start = l.StartDate,
                                        end = l.EndDate,
                                        color = "#a3c6c4"
                                    };


                        returnSchedule.AddRange(query.ToList());
                    });

                    //var user = db.UserAccount.Where(r => r.FirstApprover == UniversalHelpers.CurrentUser.ID || r.SecondApprover == UniversalHelpers.CurrentUser.ID);

                    //user.ToList().ForEach(item =>
                    //{
                    //    var query = from l in db.LeaveForm
                    //                where l.CreatedBy == item.ID && l.Status == "A"
                    //                select new ScheduleModel
                    //                {
                    //                    title = item.FirstName + " " + item.LastName + " " + "On leave",
                    //                    start = l.StartDate,
                    //                    end = l.EndDate
                    //                };

                    //    returnSchedule.AddRange(query.ToList());
                    //});

                    var holiday = from h in db.Holiday
                                  select new ScheduleModel
                                  {
                                      title = h.Description,
                                      start = h.StartDate,
                                      end = h.EndDate,
                                      color = "#b394c8"
                                  };

                    returnSchedule.AddRange(holiday.ToList());

                    return returnSchedule;
                }
            }
            catch(Exception error)
            {
                message = error.Message;

                return null;
            }
        }

        public static void Save(CalendarModel model, out string message)
        {
            try
            {
                message = "";

                using (var db = new SLVLOnlineEntities())
                {
                    if(model.ID == null || model.ID == Guid.Empty)
                    {
                        Holiday newHoliday = new Holiday
                        {
                            ID = Guid.NewGuid(),
                            Description = model.title,
                            StartDate = model.end,
                            EndDate = model.start,
                            CreatedBy = UniversalHelpers.CurrentUser.ID,
                            CreatedDate = DateTime.Now
                        };

                        db.Entry(newHoliday).State = EntityState.Added;
                    }
                    else
                    {
                        var holiday = db.Holiday.FirstOrDefault(r => r.ID == model.ID);

                        if (holiday != null)
                        {
                            if (model.Status == "X")
                                db.Entry(holiday).State = EntityState.Deleted;
                            else
                            {
                                holiday.Description = model.title;
                                holiday.EndDate = model.end;
                                holiday.StartDate = model.start;
                                holiday.CreatedBy = UniversalHelpers.CurrentUser.ID;
                                holiday.CreatedDate = DateTime.Now;

                                db.Entry(holiday).State = EntityState.Modified;
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