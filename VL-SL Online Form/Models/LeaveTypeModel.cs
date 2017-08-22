using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VL_SL_Online_Form.Models
{
    public class LeaveTypeModel
    {
        public Guid ID { get; set; }
        public string Description { get; set; }
        public int DaysBeforeFilling { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string Type { get; set; }
        public string ShowType
        {
            get
            {
                if (Type == "VL")
                    return "Vacation Leave";
                else if (Type == "SL")
                    return "Sick Leave";
                else
                    return "";
            }
        }
        public string Status { get; set; }
        public decimal LeaveDeduction { get; set; }
    }
}