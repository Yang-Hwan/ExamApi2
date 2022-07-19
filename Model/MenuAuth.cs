using System.ComponentModel.DataAnnotations;

namespace ExamModel
{
    public class MenuAuth
    {
        [Key]
        public int Auth { get; set; }

        public string UpMark { get; set; }

        public string Mark { get; set; }

        public int UpOrd { get; set; }

        public int Ord { get; set; }

        public byte IsUse { get; set; }
    }
}
