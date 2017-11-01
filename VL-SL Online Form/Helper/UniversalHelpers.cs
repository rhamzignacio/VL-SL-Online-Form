using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using VL_SL_Online_Form.Models;

namespace VL_SL_Online_Form
{
    public class UniversalHelpers
    {
        public static UserModel CurrentUser
        {
            get
            {
                UserModel user = null;

                HttpCookie authCookie_slvl = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

                if(authCookie_slvl != null)
                {
                    FormsAuthenticationTicket authTicket_slvl = FormsAuthentication.Decrypt(authCookie_slvl.Value);

                    JavaScriptSerializer serializer = new JavaScriptSerializer();

                    PrincipalSerializeModel serializeModel = serializer.Deserialize<PrincipalSerializeModel>(authTicket_slvl.UserData);

                    Principal newUser = new Principal(authTicket_slvl.Name);

                    newUser.Username = serializeModel.Username;

                    newUser.SessionID = serializeModel.SessionID;

                    newUser.Status = serializeModel.Status;

                    HttpContext.Current.User = newUser;

                    using (var db = new SLVLOnlineEntities())
                    {
                        var query = from a in db.UserAccount
                                    //REMOVE COMMENT IF WILL ACTIVATE EMAIL NOTIFICATION
                                    //
                                    //join grp in db.ApproverGroup on a.DeptID equals grp.ID into qG
                                    //from g in qG.DefaultIfEmpty()
                                    //join first in db.UserAccount on g.FirstApprover equals first.ID into qF
                                    //from f in qF.DefaultIfEmpty()
                                    //join second in db.UserAccount on g.SecondApprover equals second.ID into qS
                                    //from s in qS.DefaultIfEmpty()
                                    where a.Username == newUser.Username 
                                    select new UserModel
                                    {
                                        ID = a.ID,
                                        Username = a.Username,
                                        BirthDate = a.Birthdate,
                                        BirthPlace = a.BirthPlace,
                                        CivilStatus = a.CivilStatus,
                                        ContactNo = a.ContactNo,
                                        DateHired = a.DateHired,
                                        FirstName = a.FirstName,
                                        Gender = a.Gender,
                                        LastName = a.LastName,
                                        MiddleInitial = a.MiddleInitial,
                                        Position = a.Position,
                                        Status = a.Status,
                                        Email = a.Email,
                                        Type = a.Type,
                                        SickLeaveCount = a.SickLeaveCount,
                                        VacationLeaveCount = a.VacationLeavCount,
                                        //FirstApproverEmail = f.Email,
                                        //SecondApproverEmail = s.Email
                                    };

                        user = query.FirstOrDefault();

                        var app = db.ApproverGroup.FirstOrDefault(r => r.FirstApprover == user.ID || r.SecondApprover == user.ID);

                        if (app != null)
                            user.IfApprover = "Y";
                        else
                            user.IfApprover = "N";
                    }
                }

                return user;
            }
            set { }
        }
    }
}