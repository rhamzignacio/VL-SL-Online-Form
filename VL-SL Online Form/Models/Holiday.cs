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
    
    public partial class Holiday
    {
        public System.Guid ID { get; set; }
        public Nullable<System.DateTime> start { get; set; }
        public Nullable<System.DateTime> end { get; set; }
        public string title { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}
