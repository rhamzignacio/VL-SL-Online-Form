//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VL_SL_Online_Form.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserAccount
    {
        public System.Guid ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> Birthdate { get; set; }
        public string Gender { get; set; }
        public string CivilStatus { get; set; }
        public string BirthPlace { get; set; }
        public string Nationality { get; set; }
        public string AssignedIdNo { get; set; }
        public string Position { get; set; }
        public Nullable<System.DateTime> DateHired { get; set; }
        public string TIN { get; set; }
        public string SSS { get; set; }
        public string HDMF { get; set; }
        public string PHIC { get; set; }
        public string ContactNo { get; set; }
        public Nullable<System.Guid> FirstApprover { get; set; }
        public Nullable<System.Guid> SecondApprover { get; set; }
        public string TimeIn { get; set; }
        public string TimeOut { get; set; }
    }
}
