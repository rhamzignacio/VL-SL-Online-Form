using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace VL_SL_Online_Form.Models
{
    public class UserModel
    {
        public Guid ID { get; set; }
        public string Username { get; set; }
        public string Password { get; private set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public Guid? DeptID { get; set; }
        public string Department { get; set; }
        public string Status { get; set; }
        public string ShowStatus
        {
            get
            {
                if (Status == "Y")
                    return "Active";
                else if (Status == "X")
                    return "Deactivated";
                else
                    return "";
            }
        }
        public DateTime? BirthDate { get; set; }
        public string Gender { get; set; }
        public string CivilStatus { get; set; }
        public string BirthPlace { get; set; }
        public string Position { get; set; }
        public DateTime? DateHired { get; set; }
        public string ContactNo { get; set; }
        public string IfApprover { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }
        public double VacationLeaveCount { get; set; }
        public double SickLeaveCount { get; set; }
        public double SoloParentLeaveCount { get; set; }

        public string FirstApproverEmail { get; set; }
        public string SecondApproverEmail { get; set; }
    }

    public class EmailAccountModel
    {
        public Guid ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

}