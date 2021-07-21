using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystem2.Entites
{
    public class FamilyStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Guardian { get; set; }
        public string GuardianAddress { get; set; }
        public string LanguageSpoken { get; set; }
        public string MaritalStatus { get; set; }
        [ForeignKey("ApplicantId")]
        public Applicant Applicant { get; set; }
        public Guid ApplicantId { get; set; }


    }
}
