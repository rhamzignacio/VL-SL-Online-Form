using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VL_SL_Online_Form.Models
{
    public class LeaveTypeDropdownModel
    {
        public string Value { get; set; }
        public string Label { get; set; }
    }

    public class LeaveFormModel
    {
        public Guid ID { get; set; }
        public string Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public string DeclineReason { get; set; }

        public string ShowType
        {
            get
            {
                if (Type != null)
                {
                    if (Type == "VL")
                        return "Vacation Leave";
                    else if (Type == "VL-H")
                        return "Vacation Leave Half Day";
                    else if (Type == "SL")
                        return "Sick Leave";
                    else if (Type == "SL-H")
                        return "Sick Leave Half Day";
                    else if (Type == "EL")
                        return "Emergency Leave";
                    else if (Type == "EL-H")
                        return "Emergency Leave Half Day";
                    else if (Type == "ML")
                        return "Maternity Leave";
                    else if (Type == "PL")
                        return "Paternity Leave";
                    else if (Type == "WL")
                        return "Leave without Pay";
                    else
                        return "";
                }

                return "";
            }
        }

        public string ShowCreatedDate
        {
            get
            {
                if (CreatedDate != null)
                    return DateTime.Parse(CreatedDate.ToString()).ToShortDateString();
                else
                    return "";
            }
        }

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

        public string ShowCreatedBy { get; set; }

        public string ShowStartDate
        {
            get
            {
                if (StartDate != null)
                    return DateTime.Parse(StartDate.ToString()).ToShortDateString();
                else
                    return "";
            }
        }

        public string ShowEndDate
        {
            get
            {
                if (EndDate != null)
                    return DateTime.Parse(EndDate.ToString()).ToShortDateString();
                else
                    return "";
            }
        }
    }
}