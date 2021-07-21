using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystem2.Models
{
    public class InterviewCriteriaForCreation
    {
        public string StartDate { get; set; }
        public string StartTime { get; set; }
        public string EndDate { get; set; }
        public string EndTime { get; set; }
        public int InterviewDuration { get; set; }
        public int BreakTime { get; set; }
        public int NumberOfInterviewers { get; set; }
    }
}
