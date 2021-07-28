using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystem2.Entites
{
    public class AdmissionDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Section { get; set; }
        public string Stage { get; set; }
        public string Grade { get; set; }
        public string NewStudent { get; set; }
        public string AcademicYear { get; set; }
        [ForeignKey("ApplicantId")]
        public Applicant Applicant { get; set; }
        public Guid ApplicantId { get; set; }
    }
}
