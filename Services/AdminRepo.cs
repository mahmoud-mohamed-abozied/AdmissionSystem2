using AdmissionSystem2.Entites;
using AdmissionSystem2.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystem2.Services
{
    public class AdminRepo : IAdminRepo
    {
        private AdmissionSystemDbContext _AdmissionSystemDbContext;

        public AdminRepo(AdmissionSystemDbContext admissionSystemDbContext)
        {
            _AdmissionSystemDbContext = admissionSystemDbContext;
        }
        public IEnumerable<Application> GetApplication(ResourceParameters resourceParameters)
        {
            return _AdmissionSystemDbContext.Appliaction
                .Skip(resourceParameters.PageSize*(resourceParameters.PageNumber-1))
                .Take(resourceParameters.PageSize)
                .ToList();
        }
    }
}
