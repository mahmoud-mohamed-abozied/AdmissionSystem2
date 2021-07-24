using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystem2.Models
{
    public class EmergencyContactDto
    {
        public Guid Id { get; set; }
        public string Relationship { get; set; }
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public string HomeNumber { get; set; }
    }
}
