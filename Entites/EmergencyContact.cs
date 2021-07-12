using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystem2.Entites
{
    public class EmergencyContact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Relationship { get; set; }
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public string HomeNumber { get; set; }
        [ForeignKey("ApplicantId")]
        public Applicant Applicant { get; set; }
        public int ApplicantId { get; set; }
    }
}
