using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystem2.Entites
{
    public class InterviewCriteria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string StartDate { get; set; }
        public string StartTime{ get; set; }
        public string EndDate { get; set; }
        public string EndTime { get; set; }
        public int InterviewDuration {get; set;}
        public int BreakTime { get; set; }
        public int NumberOfInterviewers { get; set; }

    }
}
