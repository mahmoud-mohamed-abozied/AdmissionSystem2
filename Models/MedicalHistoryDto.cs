using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystem2.Models
{
    public class MedicalHistoryDto
    {
        public Guid Id { set; get; }
        public string Glass { set; get; }
        public string Hearing { set; get; }
        public string MedicalConditions { set; get; }
        public string PhysiologicalConditions { set; get; }
        public string PhysiologicalNeed { set; get; }
    }
}
