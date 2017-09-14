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
        public string Status { get; set; }

        public string ShowDate
        {
            get
            {
                string temp = "";

                if (start != null)
                    temp += DateTime.Parse(start.ToString()).ToShortDateString();

                if (end != null)
                    temp += " - " + DateTime.Parse(end.ToString()).ToShortDateString();

                return temp;
            }
        }

        public string ShowStart
        {
            get
            {
                if (start != null)
                    return DateTime.Parse(start.ToString()).ToShortDateString();
                else
                    return "";
            }
        }

        public string ShowEnd
        {
            get
            {
                if (end != null)
                    return DateTime.Parse(end.ToString()).ToShortDateString();
                else
                    return "";
            }
        }
    }

    public class ScheduleModel
    {
        public string title { get; set; }
        public DateTime? start { get; set; }
        public DateTime? end { get; set; }
        public string color { get; set; }
    }
}