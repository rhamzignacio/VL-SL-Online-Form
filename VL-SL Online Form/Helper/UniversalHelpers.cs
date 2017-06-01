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
                                    join f in db.UserAccount on a.FirstApprover equals f.ID into qF
                                    from first in qF.DefaultIfEmpty()
                                    join s in db.UserAccount on a.SecondApprover equals s.ID into qS
                                    from second in qS.DefaultIfEmpty()
                                    where a.Username == newUser.Username
                                    select new UserModel
                                    {
                                        ID = a.ID,
                                        Username = a.Username,
                                        AssignedIDNo = a.AssignedIdNo,
                                        BirthDate = a.Birthdate,
                                        BirthPlace = a.BirthPlace,
                                        CivilStatus = a.CivilStatus,
                                        ContactNo = a.ContactNo,
                                        DateHired = a.DateHired,
                                        Department = a.Department,
                                        FirstApprover = a.FirstApprover,
                                        FirstName = a.FirstName,
                                        Gender = a.Gender,
                                        HDMF = a.HDMF,
                                        LastName = a.LastName,
                                        MiddleInitial = a.MiddleInitial,
                                        Nationality = a.Nationality,
                                        PHIC = a.PHIC,
                                        Position = a.Position,
                                        SecondApprover = a.SecondApprover,
                                        SSS = a.SSS,
                                        Status = a.Status,
                                        TIN = a.TIN,
                                        Email = a.Email,
                                        FirstApproverEmail = first.Email,
                                        SecondApproverEmail = second.Email,
                                        Type = a.Type
                                    };

                        user = query.FirstOrDefault();

                        var app = db.UserAccount.FirstOrDefault(r => r.FirstApprover == user.ID || r.SecondApprover == user.ID);

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