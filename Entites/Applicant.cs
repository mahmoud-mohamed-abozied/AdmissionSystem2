using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystem2.Entites
{
    public class Applicant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ApplicantId { set; get; }
        public String FirstName { set; get; }
        public String SecondName { set; get; }
        public String LastName { set; get; }
        public String PlaceOfBirth { set; get; }
        public String DateOfBirth { set; get; }
        public String Nationality { set; get; }
        public String Religion { set; get; }
        public String Mobile { set; get; }
        public String Gender { set; get; }
        public String SpokenLanguage { set; get; }
        public String Status { set; get; }
        public string AdmissionDate { get; set; }
        public ICollection<ParentInfo> ParentInfo { set; get; } = new List<ParentInfo>();
        public ICollection<EmergencyContact> EmergencyContact { set; get; } = new List<EmergencyContact>();
        public ICollection<Document> Documents { set; get; } = new List<Document>();
        public AdmissionDetails AdmissionDetails { set; get; }
        public ICollection<Sibling> Sibling { set; get; }
            = new List<Sibling>();
        public MedicalHistory MedicalHistory { set; get; }
        public Payment Payment { set; get; }
        public FamilyStatus Family_Status { get; set; }
    }
}
