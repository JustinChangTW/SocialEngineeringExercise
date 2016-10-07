using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialEngineeringExercise.Models
{
    public class SmtpConfigModel
    {
        public string  Host { get; set; }
        public int Port { get; set; }
        public string Id { get; set; }
        public string Password { get; set; }
        public bool EnableSsl { get; set; }
    }
}