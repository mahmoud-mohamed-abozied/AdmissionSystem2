using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystem2.Entites
{
    public class Application
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime AdmissionDate { get; set; }
        public String Status { set; get; }
        public ICollection<ParentInfo> ParentInfo { set; get; } = new List<ParentInfo>();
        public ICollection<EmergencyContact> EmergencyContact { set; get; } = new List<EmergencyContact>();
        public ICollection<Document> Documents { set; get; } = new List<Document>();
        public AdmissionDetails AdmissionDetails { set; get; }

        public ICollection<Sibling> Sibling { set; get; }
            = new List<Sibling>();

        public MedicalHistory MedicalHistory { set; get; }
        public Payment Payment { set; get; }

        [ForeignKey("ApplicantId")]
        public Applicant Applicant { get; set; }
        public Guid ApplicantId { get; set; }
    }
}
