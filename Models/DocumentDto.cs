using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystem2.Models
{
    public class DocumentDto
    {

        public Guid DocumentId { get; set; }
        public string DocumentType { get; set; }
        public string DocumentName { get; set; }
        public string FilePath { get; set; }
    }
}
