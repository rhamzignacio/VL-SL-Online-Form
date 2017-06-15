using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VL_SL_Online_Form.Models;
using System.Data.Entity;

namespace VL_SL_Online_Form.Services
{
    public class GroupApproverService
    {
        public static List<GroupApproverMemberModel> GetMembers(Guid _groupID, out string message)
        {
            try
            {
                message = "";

                using (var db = new SLVLOnlineEntities())
                {
                    var query = from m in db.ApproverGroupMember
                                join g in db.ApproverGroup on m.GroupID equals g.ID
                                join u in db.UserAccount on m.UserID equals u.ID
                                orderby u.FirstName ascending
                                where m.GroupID == _groupID
                                select new GroupApproverMemberModel
                                {
                                    ID = m.ID,
                                    GroupID = m.GroupID,
                                    GroupName = g.Name,
                                    Status = "Y",
                                    UserID = m.UserID,
                                    UserName = u.FirstName + " " + u.LastName
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

        public static GroupApproverModel GetSingleGroup(Guid _ID, out string message)
        {
            try
            {
                message = "";

                using (var db = new SLVLOnlineEntities())
                {
                    var query = from g in db.ApproverGroup
                                join u in db.UserAccount on g.FirstApprover equals u.ID into qU
                                from user in qU.DefaultIfEmpty()
                                join u2 in db.UserAccount on g.SecondApprover equals u2.ID into qU2
                                from user2 in qU2.DefaultIfEmpty()
                                where g.ID == _ID
                                select new GroupApproverModel
                                {
                                    ID = g.ID,
                                    Name = g.Name,
                                    FirstApprover = g.FirstApprover,
                                    FirstApproverName = user.FirstName + " " + user.LastName,
                                    SecondApprover = g.SecondApprover,
                                    SecondApproverName = user2.FirstName + " " + user2.LastName,
                                    Status = "Y"
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

        public static List<GroupApproverDropDownModel> GetGroupDropDown(out string message)
        {
            try
            {
                message = "";

                using (var db = new SLVLOnlineEntities())
                {
                    var query = from g in db.ApproverGroup
                                orderby g.Name ascending
                                select new GroupApproverDropDownModel
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

        public static void SaveMember(GroupApproverMemberModel _member, out string message)
        {
            try
            {
                message = "";

                using (var db = new SLVLOnlineEntities())
                {
                    if(_member.ID == Guid.Empty || _member.ID == null)//NEW
                    {
                        ApproverGroupMember newMember = new ApproverGroupMember
                        {
                            ID = Guid.NewGuid(),
                            GroupID = _member.GroupID,
                            UserID = _member.UserID
                        };

                        db.Entry(newMember).State = EntityState.Added;  
                    }
                    else //UPDATE
                    {
                        var member = db.ApproverGroupMember.FirstOrDefault(r => r.ID == _member.ID);

                        if (member != null)
                        {
                            if (_member.Status == "X")
                                db.Entry(member).State = EntityState.Deleted;
                        }
                    }

                    db.SaveChanges();
                }
            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }

        public static void SaveGroup (GroupApproverModel _group, out string message)
        {
            try
            {
                message = "";

                using (var db = new SLVLOnlineEntities())
                {
                    if (_group.ID == Guid.Empty || _group.ID == null) //NEW
                    {
                        ApproverGroup newGroup = new ApproverGroup
                        {
                            ID = Guid.NewGuid(),
                            Name = _group.Name,
                            FirstApprover = _group.FirstApprover,
                            SecondApprover = _group.SecondApprover
                        };

                        db.Entry(newGroup).State = EntityState.Added;
                    }
                    else //UPDATE
                    {
                        var group = db.ApproverGroup.FirstOrDefault(r => r.ID == _group.ID);

                        if(group != null)
                        {
                            if (_group.Status == "X")
                            {
                                db.Entry(group).State = EntityState.Deleted;
                            }
                            else
                            {
                                group.Name = _group.Name;

                                group.FirstApprover = _group.FirstApprover;

                                group.SecondApprover = _group.SecondApprover;

                                db.Entry(group).State = EntityState.Modified;
                            }
                        }
                    }

                    db.SaveChanges();
                }
            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }
    }
}