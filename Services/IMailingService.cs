using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystem2.Services
{
    public interface IMailingService
    {
        Task SendEmailAsync(String mailTo, String Subject, String Body, IList<IFormFile> Attachments = null);
    }
}
