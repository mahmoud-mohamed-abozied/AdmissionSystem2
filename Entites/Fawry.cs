using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystem2.Entites
{
    [Table("Fawry")]
    public class Fawry : Payment
    {
        public String FawryCode { set; get; }
    }
}

