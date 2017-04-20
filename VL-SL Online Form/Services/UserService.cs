using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VL_SL_Online_Form.Models;
using System.Data.Entity;

namespace VL_SL_Online_Form.Services
{
    public class UserService
    {
        public static List<UserModel> GetAll(out string message)
        {
            try
            {
                message = "";

                using (var db = new SLVLOnlineEntities())
                {
                    var query = from u in db.UserAccount
                                orderby u.FirstName
                                select new UserModel
                                {
                                    ID = u.ID,
                                    Username = u.Username,
                                    FirstName = u.FirstName,
                                    MiddleInitial = u.MiddleInitial,
                                    LastName = u.LastName,
                                    Department = u.Department,
                                    Status = u.Status,
                                    BirthDate = u.Birthdate,
                                    Nationality = u.Nationality,
                                    AssignedIDNo = u.AssignedIdNo,
                                    Position = u.Position,
                                    DateHired = u.DateHired,
                                    TIN = u.TIN,
                                    SSS = u.SSS,
                                    HDMF = u.HDMF,
                                    PHIC = u.PHIC,
                                    ContactNo = u.ContactNo,
                                    FirstApprover = u.FirstApprover,
                                    SecondApprover = u.SecondApprover
                                };

                    return query.ToList();
                }
            }
            catch (Exception error)
            {
                message = error.Message;

                return null;
            }
        }
        public static void Save(UserModel _user, out string message)
        {
            try
            {
                message = "";

                using (var db = new SLVLOnlineEntities())
                {
                    if (_user.ID == Guid.Empty || _user.ID == null)//NEW
                    {
                        UserAccount newUser = new UserAccount
                        {
                            ID = Guid.NewGuid(),
                            Status = "Y",
                            Username = _user.Username,
                            Password = _user.Password,
                            FirstName = _user.FirstName,
                            MiddleInitial = _user.MiddleInitial,
                            LastName = _user.LastName,
                            Department = _user.Department,
                            Birthdate = _user.BirthDate,
                            Gender = _user.Gender,
                            CivilStatus = _user.CivilStatus,
                            BirthPlace = _user.BirthPlace,
                            Nationality = _user.Nationality,
                            AssignedIdNo = _user.AssignedIDNo,
                            Position = _user.Position,
                            DateHired = _user.DateHired,
                            TIN = _user.TIN,
                            SSS = _user.SSS,
                            HDMF = _user.HDMF,
                            PHIC = _user.PHIC,
                            ContactNo = _user.ContactNo,
                            FirstApprover = _user.FirstApprover,
                            SecondApprover = _user.SecondApprover
                        };

                        db.Entry(newUser).State = EntityState.Added;
                    }
                    else //Update
                    {
                        var user = db.UserAccount.FirstOrDefault(r => r.ID == _user.ID);

                        if (_user.Status == "X")
                            db.Entry(user).State = EntityState.Deleted;
                        else
                        {
                            user.Username = _user.Username;
                            user.FirstName = _user.FirstName;
                            user.MiddleInitial = _user.MiddleInitial;
                            user.LastName = _user.LastName;
                            user.Status = _user.Status;
                            user.Birthdate = _user.BirthDate;
                            user.Gender = _user.Gender;
                            user.CivilStatus = _user.CivilStatus;
                            user.BirthPlace = _user.BirthPlace;
                            user.Nationality = _user.Nationality;
                            user.AssignedIdNo = _user.AssignedIDNo;
                            user.Position = _user.Position;
                            user.DateHired = _user.DateHired;
                            user.TIN = _user.TIN;
                            user.SSS = _user.SSS;
                            user.HDMF = _user.HDMF;
                            user.PHIC = _user.PHIC;
                            user.ContactNo = _user.ContactNo;
                            user.FirstApprover = _user.FirstApprover;
                            user.SecondApprover = _user.SecondApprover;

                            db.Entry(user).State = EntityState.Modified;
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