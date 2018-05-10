using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VL_SL_Online_Form.Models
{
    public class AdvisoryModel
    {
        public Guid ID { get; set; }
        public string Description { get; set; }
        public Guid? ModifiedBy { get; set; }
        public string ShowModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ShowModifiedDate
        {
            get
            {
                if (ModifiedDate != null)
                    return DateTime.Parse(ModifiedDate.ToString()).ToShortDateString();
                else
                    return "";
            }
        }
        public int Status { get; set; }
    }
}