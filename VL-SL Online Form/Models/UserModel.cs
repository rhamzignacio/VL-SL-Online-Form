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
        public string Department { get; set; }
        public string Status { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Gender { get; set; }
        public string CivilStatus { get; set; }
        public string BirthPlace { get; set; }
        public string Nationality { get; set; }
        public string AssignedIDNo { get; set; }
        public string Position { get; set; }
        public DateTime? DateHired { get; set; }
        public string TIN { get; set; }
        public string SSS { get; set; }
        public string HDMF { get; set; }
        public string PHIC { get; set; }
        public string ContactNo { get; set; }
        public Guid? FirstApprover { get; set; }
        public Guid? SecondApprover { get; set; }
    }
}