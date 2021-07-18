using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystem2.Entites
{
    public class Interview
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InterviewId { get; set; }
        public string InterviewerName { get; set; }
        public int ApplicantId { get; set; }
        public string InterviewType { get; set; }
        public string AcadmicYear { get; set; }



    }
}
