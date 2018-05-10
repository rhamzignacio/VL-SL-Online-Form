using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VL_SL_Online_Form.Models;

namespace VL_SL_Online_Form.Services
{
    public class AdvisoryService
    {
        public static List<AdvisoryModel> GetAdvisory(out string message)
        {
            try
            {
                message = "";

                using (var db = new SLVLOnlineEntities())
                {
                    var advisory = from a in db.Advisory
                                   join u in db.UserAccount on a.ModifiedBy equals u.ID
                                   orderby a.ModifiedDate descending
                                   select new AdvisoryModel
                                   {
                                       ID = a.ID,
                                       Description = a.Description,
                                       ModifiedDate = a.ModifiedDate,
                                       ModifiedBy = a.ModifiedBy,
                                       ShowModifiedBy = u.FirstName + " " + u.LastName,
                                       Status = 1
                                   };

                    return advisory.ToList();
                }
            }
            catch (Exception error)
            {
                message = error.Message;

                return null;
            }
        }

        public static void Save(AdvisoryModel _advisory, out string message)
        {
            try
            {
                message = "";

                using (var db = new SLVLOnlineEntities())
                {
                    if (_advisory.ID == Guid.Empty)
                    {
                        Advisory newAdvisory = new Advisory
                        {
                            ID = Guid.NewGuid(),
                            Description = _advisory.Description,
                            ModifiedBy = UniversalHelpers.CurrentUser.ID,
                            ModifiedDate = DateTime.Now
                        };

                        db.Entry(newAdvisory).State = System.Data.Entity.EntityState.Added;
                    }
                    else
                    {
                        var adv = db.Advisory.FirstOrDefault(r => r.ID == _advisory.ID);

                        if (adv != null && _advisory.Status == 1)
                        {
                            adv.Description = _advisory.Description;

                            adv.ModifiedBy = UniversalHelpers.CurrentUser.ID;

                            adv.ModifiedDate = DateTime.Now;

                            db.Entry(adv).State = System.Data.Entity.EntityState.Modified;
                        }
                        else if (_advisory.Status == 0)
                        {
                            db.Entry(adv).State = System.Data.Entity.EntityState.Deleted;
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