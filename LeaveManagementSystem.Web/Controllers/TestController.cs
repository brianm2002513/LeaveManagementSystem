﻿using LeaveManagementSystem.Application.Models;

namespace LeaveManagementSystem.Web.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            var data = new TestViewModel
            {
                Name = "Brian Mbawa",
                DateOfBirth = new DateTime(2002, 12, 26)
            };

            return View(data);
        }
    }
}
