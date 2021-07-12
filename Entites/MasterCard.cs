using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystem2.Entites
{
    [Table("MasterCard")]
    public class MasterCard : Payment
    {
        [Required]
        public int CardNumber { set; get; }
        [Required]
        public DateTime ExpirationDate { set; get; }
        [Required]
        public string Cvv { set; get; }

    }
}
