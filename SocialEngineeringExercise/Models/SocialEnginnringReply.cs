using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialEngineeringExercise.Models
{
    public class SocialEnginnringReply
    {
        public Guid SocialEnginnringGuid { get; set; }
        public string EmployeeNo { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeEmail { get; set; }
        public DateTime SendDate { get; set; }
        public string Remark { get; set; }
        public bool IsClick { get; set; }
        public DateTime ClickDate { get; set; }
    }
}