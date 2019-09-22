using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HRM.Services;
using HRM.ViewModels.Salary;

namespace HRM.WebSite.Controllers
{
    public class SalaryLevelController : Controller
    {
        private readonly ISalaryLevelService service;
        private readonly IWorkTitleService _workTitleService;

        public SalaryLevelController(ISalaryLevelService service, IWorkTitleService workTitleService)
        {
            this.service = service;
            this._workTitleService = workTitleService;
        }

        // GET: SalaryLevel
        public ActionResult Index()
        {
            var list = service.GetSalaryLevels();
            return View(list);
        }

        // GET: SalaryLevel/Details/5
        public ActionResult Details(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // GET: SalaryLevel/Create
        public ActionResult Create()
        {
            var model = new SalaryLevelViewModel();
            var workTitles = _workTitleService.GetWorkTitles();
            ViewBag.WorkTitles = new SelectList(workTitles, "Id", "Name");
            model.EffectiveDate = DateTime.Now.ToLocalTime();
            return View(model);
        }

        // POST: SalaryLevel/Create
        [HttpPost]
        public ActionResult Create(SalaryLevelViewModel model)
        {
            
            try
            {
                if (ModelState.IsValid)
                {
                    //model.MonthlySalary = model.MonthlySalary.ToString().de;
                    service.Insert(model);
                    service.Save();
                    return RedirectToAction("Index");
                }

                var workTitles = _workTitleService.GetWorkTitles();
                ViewBag.WorkTitles = new SelectList(workTitles, "Id", "Name");
                return View(model);
                
            }
            catch
            {
                return View();
            }
        }

        // GET: SalaryLevel/Edit/5
        public ActionResult Edit(int id)
        {
            var workTitles = _workTitleService.GetWorkTitles();
            ViewBag.WorkTitles = new SelectList(workTitles, "Id", "Name");

            var model = service.GetInfo(id);
            return View(model);
        }

        // POST: SalaryLevel/Edit/5
        [HttpPost]
        public ActionResult Edit(SalaryLevelViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.Update(model);
                    service.Save();
                    return RedirectToAction("Index");
                }

                return View(model);
               
            }
            catch
            {
                return View();
            }
        }

        // GET: SalaryLevel/Delete/5
        public ActionResult Delete(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // POST: SalaryLevel/Delete/5
        [HttpPost]
        public ActionResult Delete(SalaryLevelViewModel model)
        {
            try
            {
                service.Delete(model);
                service.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
