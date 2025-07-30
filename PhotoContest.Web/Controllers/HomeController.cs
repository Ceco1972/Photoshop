using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoContest.Service.Contracts;
using PhotoContest.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoContest.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IContestService contestService;

        public HomeController(ILogger<HomeController> logger, IContestService contestService)
        {
            _logger = logger;
            this.contestService = contestService;
        }

        public IActionResult Index()
        {
            this.contestService.ChangeStatus();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        } 
        public IActionResult Contacts()
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
