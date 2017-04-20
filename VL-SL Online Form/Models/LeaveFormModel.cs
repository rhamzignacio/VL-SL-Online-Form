using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VL_SL_Online_Form.Models
{
    public class LeaveFormModel
    {
        public Guid ID { get; set; }
        public string Type { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
    }
}