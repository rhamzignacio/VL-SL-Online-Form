using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VL_SL_Online_Form.Models;

namespace VL_SL_Online_Form.Services
{
    public class UniversalService
    {
        public static List<UserDropDownModel> GetUserDropDown(out string message)
        {
            try
            {
                message = "";

                using (var db = new SLVLOnlineEntities())
                {
                    var query = from u in db.UserAccount
                                where u.Status == "Y"
                                select new UserDropDownModel
                                {
                                    ID = u.ID,
                                    Name = "(" + u.Department + ") " + u.FirstName + " " + u.LastName
                                };

                    return query.OrderBy(r=>r.Name).ToList();
                }
            }
            catch(Exception error)
            {
                message = error.Message;

                return null;
            }
        }
    }
}