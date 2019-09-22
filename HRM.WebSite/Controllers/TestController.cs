using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.Domain.Entity;
using HRM.Services;

namespace HRM.WebSite.Controllers
{
    public class TestController : Controller
    {
        private ITestService _testService;

        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            _testService.Add(new Test() {

            });
            _testService.Save();

            return View();
        }
    }
}