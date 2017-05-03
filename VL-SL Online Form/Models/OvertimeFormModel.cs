using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VL_SL_Online_Form.Models
{
    public class OvertimeFormModel
    {
        public Guid ID { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public string ShowStatus
        {
            get
            {
                if (Status == "P")
                    return "Pending";
                else if (Status == "A")
                    return "Approved";
                else if (Status == "D")
                    return "Declined";
                else if (Status == "X")
                    return "Canceled";
                else
                    return Status;
            }
        }

        public string ShowEffectiveDate
        {
            get
            {
                if (EffectiveDate != null)
                    return DateTime.Parse(EffectiveDate.ToString()).ToShortDateString();
                else
                    return "";
            }
        }
    }
}