using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SocialEngineeringExercise.Models
{
    public class SocialEnginnringReply
    {
        [DisplayName("序號")]
        [Description("提供回覆的唯一更新的值")]
        public Guid SocialEnginnringGuid { get; set; }

        [DisplayName("員工編號")]
        public string EmployeeNo { get; set; }

        [DisplayName("員工姓名")]
        public string EmployeeName { get; set; }

        [DisplayName("員工Email")]
        public string EmployeeEmail { get; set; }

        [DisplayName("寄送日期")]
        public DateTime SendDate { get; set; }

        [DisplayName("備註")]
        public string Remark { get; set; }

        [DisplayName("是否被Click")]
        [Description("提供回覆的唯一更新的值")]
        public bool IsClick { get; set; }

        [DisplayName("被Click日期")]
        public DateTime ClickDate { get; set; }
    }
}