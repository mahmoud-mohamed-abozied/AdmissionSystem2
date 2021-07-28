using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystem2.Models
{
    public class AdmissionDetailsDto
    {
        public Guid Id { get; set; }
        public string Section { get; set; }
        public string Stage { get; set; }
        public string Grade { get; set; }
        public string AcademicYear { get; set; }
        public string NewStudent { get; set; }

    }
}
