using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystem2.Models
{
    public class MedicalHistoryDto
    {
        public Guid MedicalHistoryId { set; get; }
        public bool Glass { set; get; }
        public bool Hearing { set; get; }
        public string MedicalConditions { set; get; }
        public string PhysiologicalConditions { set; get; }
        public bool PhysiologicalNeed { set; get; }
    }
}
