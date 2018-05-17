using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using aspbasic.Models;
using aspbasic.service;

namespace aspbasic.Controllers
{
    public class HomeController : Controller
    {

        IAspBasicService aspBasicService;
        public HomeController(IAspBasicService _aspBasicService) {
            this.aspBasicService = _aspBasicService;
        }

        public IActionResult Index()
        {
            ViewData["Orders"] = aspBasicService.GetOrders();
            return View();
        }

        public IActionResult Upload()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Fulfill(int id) 
        {
            // ideally in a CRUD this would be a PUT

            aspBasicService.FulfillOrder(id);   
            return Ok();
        }

  
    }
}
