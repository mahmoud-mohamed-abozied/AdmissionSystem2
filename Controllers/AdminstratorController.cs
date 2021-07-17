using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystem2.Controllers
{
    public class AdminstratorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
