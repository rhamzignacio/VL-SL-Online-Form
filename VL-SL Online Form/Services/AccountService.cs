using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using VL_SL_Online_Form.Models;

namespace VL_SL_Online_Form.Services
{
    public class AccountService
    {
        public static UserModel ValidateLogin(string username, string password, out string message)
        {
            message = "";

            try
            {
                using (var db = new SLVLOnlineEntities())
                {
                    var user = db.UserAccount.FirstOrDefault(r => r.Username.ToLower() == username.ToLower());

                    if (user != null)
                    {
                        if (user.Password.ToLower() == password.ToLower())
                        {
                            if (user.Status == "N")
                                message = "User is deactivated";
                            else
                            {
                                UserModel userModel = new UserModel
                                {
                                    Username = user.Username,
                                    Status = user.Status,
                                    FirstName = user.FirstName,
                                    MiddleInitial = user.MiddleInitial,
                                    LastName = user.LastName,
                                    BirthDate = user.Birthdate,
                                    Gender = user.Gender,
                                    CivilStatus = user.CivilStatus,
                                    BirthPlace = user.BirthPlace,
                                    Position = user.Position,
                                    DateHired = user.DateHired,
                                    ContactNo = user.ContactNo,
                                    DeptID = user.DeptID
                                };

                                return userModel;
                            }
                        }
                        else
                            message = "Invalid Password";
                    }
                    else
                        message = "Invalid Username";

                    return null;
                }
            }
            catch(Exception error)
            {
                message = error.Message;

                return null;
            }
        }

        public static void LogoutFromSession(out string message)
        {
            try
            {
                message = "";

                FormsAuthentication.SignOut();
            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }

        public static bool LoginToSession(UserModel user)
        {
            try
            {
                HttpContext.Current.Session["session_status"] = "online";

                PrincipalSerializeModel serializeModel = new PrincipalSerializeModel();

                serializeModel.Username = user.Username;

                serializeModel.Password = user.Password;

                serializeModel.SessionID = HttpContext.Current.Session.SessionID;

                serializeModel.Status = user.Status;

                JavaScriptSerializer serializer = new JavaScriptSerializer();

                string userData = serializer.Serialize(serializeModel);

                FormsAuthenticationTicket authenticationSLVL = new FormsAuthenticationTicket(1, user.Username, DateTime.Now, DateTime.Now.AddMinutes(30), true, userData);

                string encryptedTicket = FormsAuthentication.Encrypt(authenticationSLVL);

                HttpCookie authenticationCookie_SLVL = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                HttpResponse response = HttpContext.Current.Response;

                response.Cookies.Add(authenticationCookie_SLVL);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}