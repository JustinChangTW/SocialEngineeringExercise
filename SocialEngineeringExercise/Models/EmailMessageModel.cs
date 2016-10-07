using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace SocialEngineeringExercise.Models
{
    public class EmailMessageModel
    {
        [Key]
        public Guid Guid { get; set; }

        [Description("郵件主旨")]
        public string Subject { get; set; }

        [Description("是否使用HTML編碼顯示")]
        public bool IsBodyHtml { get; set; }

        [Description("郵件內文")]
        public string Body { get; set; }

        [Description("重要性，MailPriority")]
        public string Prority { get; set; }

        [Description("郵件編碼")]
        public string MailEncoding { get; set; }

        [Description("附件位罝")]
        public string Attachment { get; set; }

        [Description("設定該附件為一個內嵌附件(Inline Attachment)")]
        public bool  AttachmentInline { get; set; }
    }
}