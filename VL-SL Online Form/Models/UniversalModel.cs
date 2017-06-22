using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VL_SL_Online_Form.Models
{
    public class UniversalModel
    {
    }

    public class UserDropDownModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
    }

    public class GroupDropDownModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
    }
}