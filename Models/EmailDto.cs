using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystem2.Models
{
    public class EmailDto
    {
        public String ToEmail { set; get; }
        public String Subject { set; get; }
        public String Body { set; get; }
        public IList<IFormFile> Attachments { set; get; }

    }
}
