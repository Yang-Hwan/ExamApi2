using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ExamModel
{
    public class Auth
    {
        [Key]
        public int AuthIdx { get; set; }
        public string AuthType { get; set; }
    }
}
