using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystem2.Models
{
    public class AppliantsForInterview
    {
        public Guid ApplicantId { get; set; }
        public DateTime InterviewDate { get; set; }
        public string ApplicantName { get; set; }
      //  public int Score { get; set; }
        public string Status { get; set; }
    }
}
