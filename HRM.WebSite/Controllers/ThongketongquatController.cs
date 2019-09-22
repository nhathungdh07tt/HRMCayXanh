using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.Services;


namespace HRM.WebSite.Controllers
{
    public class ThongketongquatController : Controller            
    {
        private readonly IEmployeeService _thongketongquatService;       
        private readonly ISkillService _skillService;
        private readonly IDepartmentService _departmentService;

        public ThongketongquatController(/*IThongketongquatService thongketongquatService,*/           
            ISkillService skillService,
            IDepartmentService departmentService
            )
        {
            //this._thongketongquatService = thongketongquatService;           
            this._skillService = skillService;
            this._departmentService = departmentService;
        }

        // GET: Thongketongquat
        public ActionResult Index()
        {
            var skills = _skillService.GetSkills();            
            ViewBag.Skills = new SelectList(skills, "Id", "Title");

            return View();
        }
    }
}