using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.Services;
using HRM.ViewModels.Employee;

namespace HRM.WebSite.Controllers
{
    public class SearchController : Controller
    {
        private readonly IEmployeeSearchService _employeeSearchService;
        public SearchController(IEmployeeSearchService employeeSearchService)
        {
            _employeeSearchService = employeeSearchService;
        }

        // GET: Search
        public string Index()
        {
            string result = "SearchByAge: {0}, \n SearchByBirthDay: {1}, \n SearchByGender: {2}, \n SearchByEducation : {3}";

            var count1 = _employeeSearchService.SearchByAge(20, 21).Count;
            var count2 = _employeeSearchService.SearchByBirthDay(DateTime.Now.AddYears(21), DateTime.Now).Count;
            var count3 = _employeeSearchService.SearchByGender(1).Count;
            var count4 = _employeeSearchService.SearchByEducation(1).Count;
            return string.Format(result, count1, count2, count3, count4);
        }

        [HttpGet]
        public ActionResult SearchByAge(int? from, int? to)
        {
            var employees = new List<EmployeeViewModel>();

            if (from.HasValue && to.HasValue)
            {
                employees = _employeeSearchService.SearchByAge(from.Value, to.Value); 
            }

            return View(employees);

        }

        public ActionResult SearchByBirthDay(string from, string to)
        {
            var employees = new List<EmployeeViewModel>();

            if (!string.IsNullOrEmpty(from) || !string.IsNullOrEmpty(to))
            {
                employees = _employeeSearchService.SearchByBirthDay(Convert.ToDateTime(from, new CultureInfo("vi-VN", false).DateTimeFormat), Convert.ToDateTime(to, new CultureInfo("vi-VN", false).DateTimeFormat));
            }

            return View(employees);
        }

        public ActionResult SearchByGender(int? gender)
        {
            var employees = new List<EmployeeViewModel>();

            if (gender.HasValue)
            {
                employees = _employeeSearchService.SearchByGender(gender.Value);
            }

            return View(employees);
        }

        public ActionResult SearchByEducation(int? education)
        {
            var employees = new List<EmployeeViewModel>();

            if (education.HasValue)
            {
                employees = _employeeSearchService.SearchByEducation(education.Value);
            }

            return View(employees);
        }


    }
}
