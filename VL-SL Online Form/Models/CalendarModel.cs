using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VL_SL_Online_Form.Models
{
    public class CalendarModel
    {
        public Guid ID { get; set; }
        public DateTime? start { get; set; }
        public DateTime? end { get; set; }
        public string title { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}