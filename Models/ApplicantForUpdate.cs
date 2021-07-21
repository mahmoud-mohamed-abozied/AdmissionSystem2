using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystem2.Models
{
    public class ApplicantForUpdate
    {
        public Guid ID { get; set; }
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
        public DateTime AdmissionDate { get; set; }
        public string Email { get; set; }
    }
}
