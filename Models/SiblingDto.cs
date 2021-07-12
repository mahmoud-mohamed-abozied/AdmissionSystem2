using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystem2.Models
{
    public class SiblingDto
    {
        public Guid SibilingId { set; get; }
        public string Relationship { set; get; }
        public string SiblingName { set; get; }
        public int Age { set; get; }
        public string SchoolName { set; get; }
    }
}
