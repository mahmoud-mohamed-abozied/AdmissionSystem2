using AdmissionSystem2.Entites;
using AdmissionSystem2.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystem2.Services
{
    public interface IAdminRepo
    {
        IEnumerable<Application> GetApplication(ResourceParameters resourceParameters);

    }
}
