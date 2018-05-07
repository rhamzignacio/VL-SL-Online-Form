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
                                    BirthDate = u.Birthdate,
                                    CivilStatus = u.CivilStatus,
                                    BirthPlace = u.BirthPlace,
                                    ContactNo = u.ContactNo,
                                    DateHired = u.DateHired,
                                    Email = u.Email,
                                    FirstName = u.FirstName,
                                    Gender = u.Gender,
                                    LastName = u.LastName,
                                    MiddleInitial = u.MiddleInitial,
                                    Position = u.Position,
                                    SickLeaveCount = u.SickLeaveCount,
                                    Status = u.Status,
                                    Username = u.Username,
                                    VacationLeaveCount = u.VacationLeavCount,
                                    SoloParentLeaveCount = u.SoloParentLeaveCount
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
                                    BirthDate = u.Birthdate,
                                    CivilStatus = u.CivilStatus,
                                    BirthPlace = u.BirthPlace,
                                    ContactNo = u.ContactNo,
                                    DateHired = u.DateHired,
                                    Email = u.Email,
                                    FirstName = u.FirstName,
                                    Gender = u.Gender,
                                    LastName = u.LastName,
                                    MiddleInitial = u.MiddleInitial,
                                    Position = u.Position,
                                    SickLeaveCount = u.SickLeaveCount,
                                    Status = u.Status,
                                    Username = u.Username,
                                    VacationLeaveCount = u.VacationLeavCount,
                                    SoloParentLeaveCount = u.SoloParentLeaveCount
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
                                    Name =  u.FirstName + " " + u.LastName
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