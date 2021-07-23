using AdmissionSystem2.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystem2.Models
{
    public class Application
    {
        public ApplicantDto Applicant { get; set; }
        public IEnumerable<ParentInfoDto> ParentInfo { set; get; } = new List<ParentInfoDto>();
        public IEnumerable<EmergencyContactDto> EmergencyContact { set; get; } = new List<EmergencyContactDto>();
        public IEnumerable<Document> Documents { set; get; } = new List<Document>();
        public AdmissionDetailsDto AdmissionDetails { set; get; }
        public IEnumerable<SiblingDto> Sibling { set; get; }
            = new List<SiblingDto>();
        public MedicalHistoryDto MedicalHistory { set; get; }
        //public Payment Payment { set; get; }
    }
}
