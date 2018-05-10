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
        public static void ChangePassword(string _oldPassword, string _newPassword, out string message)
        {
            try
            {
                message = "";

                using (var db = new SLVLOnlineEntities())
                {
                    var user = db.UserAccount.FirstOrDefault(r => r.ID == UniversalHelpers.CurrentUser.ID && r.Password == _oldPassword);

                    if (user != null)
                    {
                        user.Password = _newPassword;

                        db.Entry(user).State = EntityState.Modified;

                        db.SaveChanges();
                    }
                    else
                        message = "Invalid Password. . .";
                }
            }
            catch (Exception error)
            {
                message = error.Message;
            }
        }

        public static void AddCreditPerGroup(Guid _groupID, int VL, int SL, int soloLeave, out string message)
        {
            try
            {
                message = "";

                using (var db = new SLVLOnlineEntities())
                {
                    var users = db.UserAccount.Where(r => r.DeptID == _groupID).ToList();

                    if(users != null)
                    {
                        users.ToList().ForEach(user =>
                        {
                            user.VacationLeavCount += VL;

                            user.SickLeaveCount += SL;

                            user.SoloParentLeaveCount += soloLeave;

                            db.Entry(user).State = EntityState.Modified;
                        });

                        db.SaveChanges();
                    }
                }
            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }

        public static void AddCreditPerEmployee(Guid _userID, int VL, int SL, int soloLeave, out string message)
        {
            try
            {
                message = "";

                using (var db = new SLVLOnlineEntities())
                {
                    var user = db.UserAccount.FirstOrDefault(r => r.ID == _userID);

                    if(user != null)
                    {
                        user.VacationLeavCount += VL;

                        user.SickLeaveCount += SL;

                        user.SoloParentLeaveCount += soloLeave;

                        db.Entry(user).State = EntityState.Modified;

                        db.SaveChanges();
                    }
                }
            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }

        public static EmailAccountModel GetEmail(out string message)
        {
            try
            {
                message = "";

                using(var db = new SLVLOnlineEntities())
                {
                    var email = from e in db.EmailCredential
                                where e.ID != null && e.ID != Guid.Empty
                                select new EmailAccountModel
                                {
                                    ID = e.ID,
                                    Email = e.Email,
                                };

                    return email.FirstOrDefault();
                }
            }
            catch(Exception error)
            {
                message = error.Message;

                return new EmailAccountModel();
            }
        }

        public static void SaveEmail(EmailAccountModel _email, out string message)
        {
            try
            {
                message = "";

                using (var db = new SLVLOnlineEntities())
                {
                    var email = db.EmailCredential.FirstOrDefault(r => r.ID != null);

                    if(email == null)
                    {
                        EmailCredential newEmail = new EmailCredential
                        {
                            ID = Guid.NewGuid(),
                            Email = _email.Email,
                            Password = _email.Password
                        };

                        db.Entry(newEmail).State = EntityState.Added;
                    }
                    else
                    {
                        _email.Password = _email.Password;

                        _email.Email = _email.Email;

                        db.Entry(_email).State = EntityState.Modified;
                    }

                    db.SaveChanges();
                }
            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }

        public static List<UserModel> GetAll(out string message)
        {
            try
            {
                message = "";

                using (var db = new SLVLOnlineEntities())
                {
                    var query = from u in db.UserAccount
                                join grp in db.ApproverGroup on u.DeptID equals grp.ID into qG
                                from g in qG.DefaultIfEmpty()
                                orderby u.FirstName
                                select new UserModel
                                {
                                    ID = u.ID,
                                    Username = u.Username,
                                    FirstName = u.FirstName,
                                    MiddleInitial = u.MiddleInitial,
                                    LastName = u.LastName,
                                    Status = u.Status,
                                    BirthDate = u.Birthdate,
                                    Position = u.Position,
                                    DateHired = u.DateHired,
                                    ContactNo = u.ContactNo,
                                    Email = u.Email,
                                    Type = u.Type,
                                    DeptID = u.DeptID,
                                    Department = g.Name,
                                    SickLeaveCount = u.SickLeaveCount,
                                    VacationLeaveCount = u.VacationLeavCount,
                                    SoloParentLeaveCount = u.SoloParentLeaveCount
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
                            Birthdate = _user.BirthDate,
                            Gender = _user.Gender,
                            CivilStatus = _user.CivilStatus,
                            BirthPlace = _user.BirthPlace,
                            Position = _user.Position,
                            DateHired = _user.DateHired,
                            ContactNo = _user.ContactNo,
                            Email = _user.Email,
                            Type = _user.Type,
                            DeptID = _user.DeptID,
                            SoloParentLeaveCount = _user.SoloParentLeaveCount,
                            SickLeaveCount = _user.SickLeaveCount,
                            VacationLeavCount = _user.VacationLeaveCount
                        };

                        db.Entry(newUser).State = EntityState.Added;
                    }
                    else //Update
                    {
                        var user = db.UserAccount.FirstOrDefault(r => r.ID == _user.ID);

                        user.Username = _user.Username;
                        user.FirstName = _user.FirstName;
                        user.MiddleInitial = _user.MiddleInitial;
                        user.LastName = _user.LastName;
                        user.Status = _user.Status;
                        user.Birthdate = _user.BirthDate;
                        user.Gender = _user.Gender;
                        user.CivilStatus = _user.CivilStatus;
                        user.BirthPlace = _user.BirthPlace;
                        user.Position = _user.Position;
                        user.DateHired = _user.DateHired;
                        user.ContactNo = _user.ContactNo;
                        user.Email = _user.Email;
                        user.Type = _user.Type;
                        user.DeptID = _user.DeptID;
                        user.VacationLeavCount = _user.VacationLeaveCount;
                        user.SickLeaveCount = _user.SickLeaveCount;
                        user.SoloParentLeaveCount = user.SoloParentLeaveCount;

                        if (_user.Password != null && _user.Password != "")
                            user.Password = _user.Password;

                        db.Entry(user).State = EntityState.Modified;
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