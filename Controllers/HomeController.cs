using AdmissionSystem2.Models;
using AdmissionSystem2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystem2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMailingService _mailingService;
        public HomeController(ILogger<HomeController> logger, IMailingService mailingService)
        {
            _logger = logger;
            _mailingService = mailingService;
        }
        
        [HttpPost("Send")]
        public async Task<IActionResult> SendEmail([FromForm] EmailDto email)
        {
            await _mailingService.SendEmailAsync(email.ToEmail, email.Subject, email.Body, email.Attachments);
            return Ok();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
