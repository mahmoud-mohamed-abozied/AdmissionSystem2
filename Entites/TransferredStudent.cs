using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystem2.Entites
{
    public class TransferredStudent : AdmissionDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public String SchoolName { get; set; }
        public String LastYearScore { get; set; }
        public String Curriculum { get; set; }

    }
}
