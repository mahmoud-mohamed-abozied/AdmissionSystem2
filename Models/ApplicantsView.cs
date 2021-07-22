using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystem2.Models
{
    public class ApplicantsView
    {
        public Guid ApplicantId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public DateTime AdmissionDate { get; set; }
        public string Status { get; set; }
    }
}
