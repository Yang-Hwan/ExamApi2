using System;
using System.ComponentModel.DataAnnotations;

namespace ExamModel
{
    public class MemWalletLog
    {
        [Key]
        public int LogIdx { get; set; }
        public int MemIdx { get; set; }

        public int Point { get; set; }

        public int UpdIdx { get; set; }

        public DateTime? UpdDt { get; set; }
    }
}
