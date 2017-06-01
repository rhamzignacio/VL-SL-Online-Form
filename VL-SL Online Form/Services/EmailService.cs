using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VL_SL_Online_Form.Models;
using System.Net;
using System.Net.Mail;

namespace VL_SL_Online_Form.Services
{
    public class EmailService
    {
        public static void SendEmail(string _subject, string _content, string _toMail)
        {
            try
            {
                _subject = "SLVL Online - " + _subject;

                _content += "\n\n\n Please do not reply on this email . . . \n";

                using (var db = new SLVLOnlineEntities())
                {
                    var email = db.EmailCredential.FirstOrDefault(r => r.ID != null);

                    if(email != null)
                    {
                        using(MailMessage mm = new MailMessage(email.Email, _toMail))
                        {
                            mm.Subject = _subject;

                            mm.Body = _content;

                            mm.IsBodyHtml = false;

                            SmtpClient smtp = new SmtpClient();

                            smtp.Host = "smtp.gmail.com";

                            smtp.EnableSsl = true;

                            NetworkCredential networkCredential = new NetworkCredential(email.Email, email.Password);

                            smtp.UseDefaultCredentials = true;

                            smtp.Credentials = networkCredential;

                            smtp.Port = 587;

                            smtp.Send(mm);
                        }
                    }
                }
            }
            catch(Exception error)
            {

            }
        }
    }
}