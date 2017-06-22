using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VL_SL_Online_Form.Models;

namespace VL_SL_Online_Form.Services
{
    public class UniversalService
    { 
        public static List<UserModel> GetSelectedGroup (Guid GroupID, out string message)
        {
            try
            {
                message = "";

                using (var db = new SLVLOnlineEntities())
                {
                    var query = from g in db.ApproverGroup
                                join m in db.ApproverGroupMember on g.ID equals m.GroupID
                                join u in db.UserAccount on m.UserID equals u.ID
                                where g.ID == GroupID
                                select new UserModel
                                {
                                    ID = u.ID,
                                    AssignedIDNo = u.AssignedIdNo,
                                    BirthDate = u.Birthdate,
                                    CivilStatus = u.CivilStatus,
                                    BirthPlace = u.BirthPlace,
                                    ContactNo = u.ContactNo,
                                    DateHired = u.DateHired,
                                    Department = u.Department,
                                    Email = u.Email,
                                    FirstApprover = u.FirstApprover,
                                    FirstName = u.FirstName,
                                    Gender = u.Gender,
                                    HDMF = u.HDMF,
                                    LastName = u.LastName,
                                    MiddleInitial = u.MiddleInitial,
                                    Nationality = u.Nationality,
                                    PHIC = u.PHIC,
                                    Position = u.Position,
                                    SecondApprover = u.SecondApprover,
                                    SickLeaveCount = u.SickLeaveCount,
                                    SSS = u.SSS,
                                    Status = u.Status,
                                    TIN = u.TIN,
                                    Username = u.Username,
                                    VacationLeaveCount = u.VacationLeavCount
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

        public static UserModel GetSelectedUser(Guid _ID, out string message)
        {
            try
            {
                message = "";

                using (var db = new SLVLOnlineEntities())
                {
                    var query = from u in db.UserAccount
                                where u.ID == _ID
                                select new UserModel
                                {
                                    ID = u.ID,
                                    AssignedIDNo = u.AssignedIdNo,
                                    BirthDate = u.Birthdate,
                                    CivilStatus = u.CivilStatus,
                                    BirthPlace = u.BirthPlace,
                                    ContactNo = u.ContactNo,
                                    DateHired = u.DateHired,
                                    Department = u.Department,
                                    Email = u.Email,
                                    FirstApprover = u.FirstApprover,
                                    FirstName = u.FirstName,
                                    Gender = u.Gender,
                                    HDMF = u.HDMF,
                                    LastName = u.LastName,
                                    MiddleInitial = u.MiddleInitial,
                                    Nationality = u.Nationality,
                                    PHIC = u.PHIC,
                                    Position = u.Position,
                                    SecondApprover = u.SecondApprover,
                                    SickLeaveCount = u.SickLeaveCount,
                                    SSS = u.SSS,
                                    Status = u.Status,
                                    TIN = u.TIN,
                                    Username = u.Username,
                                    VacationLeaveCount = u.VacationLeavCount
                                };

                    return query.FirstOrDefault();
                }
            }
            catch(Exception error)
            {
                message = error.Message;

                return null;
            }
        }
        public static List<GroupDropDownModel> GetGroupDropDown(out string message)
        {
            try
            {
                message = "";

                using (var db = new SLVLOnlineEntities())
                {
                    var query = from g in db.ApproverGroup
                                orderby g.Name ascending
                                select new GroupDropDownModel
                                {
                                    ID = g.ID,
                                    Name = g.Name
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