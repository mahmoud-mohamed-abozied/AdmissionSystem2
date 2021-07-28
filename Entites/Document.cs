using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystem2.Entites
{
    public class Document
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       
        public Guid DocumentId { get; set; }
        public string DocumentType { get; set; }
        public string DocumentName { get; set; }
        public byte[] Copy { get; set; }
        [ForeignKey("ApplicantId")]
        public Applicant Applicant { get; set; }
        public Guid ApplicantId { get; set; }
    }
}
