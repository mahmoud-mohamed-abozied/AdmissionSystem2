using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystem2.Entites
{
    public class MedicalHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid MedicalHistoryId { set; get; }
        public bool Glass { set; get; }
        public bool Hearing { set; get; }
        public string MedicalConditions { set; get; }
        public string PhysiologicalConditions { set; get; }
        public bool PhysiologicalNeed { set; get; }

        [ForeignKey("ApplicantId")]
        public Applicant Applicant { set; get; }
        public int ApplicantId { set; get; }
    }
}
