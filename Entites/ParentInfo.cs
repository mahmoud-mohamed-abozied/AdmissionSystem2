using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystem2.Entites
{
    public class ParentInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public String FirstName { set; get; }
        public String SecondName { set; get; }
        public String LastName { set; get; }
        public String PlaceOfBirth { set; get; }
        public String DateOfBirth { set; get; }
        public String Nationality { set; get; }
        public String Relegion { set; get; }
        public String Mobile { set; get; }
        public String Gender { set; get; }
        public String Occupation { set; get; }
        public String CompanyName { set; get; }
        public String Email { set; get; }
        public String IdentificationType { set; get; }
        public String IdentificationNumber { set; get; }
        [ForeignKey("ApplicantId")]
        public Applicant Applicant { set; get; }
        public int ApplicantId { set; get; }
    }
}
