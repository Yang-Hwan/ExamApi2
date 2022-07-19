using System;
using System.ComponentModel.DataAnnotations;

namespace ExamModel
{
    public class MemInfo
    {
        [Key]
        public int MemIdx { get; set; }

        public string MemNm { get; set; }

        public string Hp { get; set; }

        public int Auth { get; set; }

        public int RegIdx { get; set; }

        public DateTime RegDt { get; set;}
    }
}
