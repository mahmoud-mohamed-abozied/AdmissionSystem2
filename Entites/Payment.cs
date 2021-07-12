using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystem2.Entites
{
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PaymentId { set; get; }
        public int SchoolId { set; get; }
        public DateTime Date { set; get; }
        public float Amount { set; get; }
        public string PaymentMethod { set; get; }

        [ForeignKey("ApplicantId")]
        public Applicant Applicant { set; get; }
        public int ApplicantId { set; get; }
    }
}
