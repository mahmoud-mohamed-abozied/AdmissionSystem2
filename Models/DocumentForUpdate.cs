using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystem2.Models
{
    public class DocumentForUpdate
    {
        public Guid DocumentId { get; set; }
        public string DocumentType { get; set; }
        public string DocumentName { get; set; }
        public IFormFile Copy { get; set; }
    }
}
