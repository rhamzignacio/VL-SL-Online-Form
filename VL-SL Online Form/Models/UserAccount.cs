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
        public string Status { get; set; }
        public Nullable<System.DateTime> Birthdate { get; set; }
        public string Gender { get; set; }
        public string CivilStatus { get; set; }
        public string BirthPlace { get; set; }
        public string Position { get; set; }
        public Nullable<System.DateTime> DateHired { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }
        public double SickLeaveCount { get; set; }
        public double VacationLeavCount { get; set; }
        public Nullable<System.Guid> DeptID { get; set; }
        public double SoloParentLeaveCount { get; set; }
    }
}
