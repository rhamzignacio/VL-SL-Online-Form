using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VL_SL_Online_Form.Models
{
    public class GroupApproverModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public Guid FirstApprover { get; set; }
        public string FirstApproverName { get; set; }
        public Guid? SecondApprover { get; set; }
        public string SecondApproverName { get; set; }
        public string Status { get; set; }
    }

    public class GroupApproverMemberModel
    {
        public Guid ID { get; set; }
        public Guid GroupID { get; set; }
        public Guid UserID { get; set; }

        public string GroupName { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }
    }

    public class GroupApproverDropDownModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
    }
}