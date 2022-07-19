using System;
using System.ComponentModel.DataAnnotations;

namespace ExamModel
{
    public class Menu
    {
        [Key]
        public string Mark { get; set; }
        public string MarkRef { get; set; }

        public int Step { get; set; }
        public int Ord { get; set; }
        public string Src { get; set; }
        public string MenuNm { get; set; }
        public byte IsUse { get; set; }

        public int RegIdx { get; set; }

        public DateTime RegDt { get; set; }
        public int UpdIdx { get; set; }

        public DateTime UpdDt { get; set; }

    }
}
