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
        public Guid Id { set; get; }
        public string Glass { set; get; }
        public string Hearing { set; get; }
        public string MedicalConditions { set; get; }
        public string PhysiologicalConditions { set; get; }
        public string PhysiologicalNeed { set; get; }

        [ForeignKey("ApplicantId")]
        public Applicant Applicant { set; get; }
        public Guid ApplicantId { set; get; }
    }
}
