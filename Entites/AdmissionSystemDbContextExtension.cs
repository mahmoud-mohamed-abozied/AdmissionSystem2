using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystem2.Entites
{
    public class AdmissionSystemDbContextExtension
    {
        public static void Initialize(AdmissionSystemDbContext context)
        {
            //   context.Database.EnsureCreated();

            // Look for any students.
            if (context.Applicant.Any())
            {
                return;   // DB has been seeded
            }

        }
    }
}