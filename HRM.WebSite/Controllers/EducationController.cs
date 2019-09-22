using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.Services;
using HRM.ViewModels.Employee;

namespace HRM.WebSite.Controllers
{
    public class EducationController : Controller
    {
        private readonly IEducationService service;

        public EducationController(IEducationService service)
        {
            this.service = service;
        }

        // GET: Education
        public ActionResult Index()
        {
            var list = service.GetEducations();
            return View(list);
        }

        // GET: Education/Details/5
        public ActionResult Details(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // GET: Education/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Education/Create
        [HttpPost]
        public ActionResult Create(EducationViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.Insert(model);
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

        // GET: Education/Edit/5
        public ActionResult Edit(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // POST: Education/Edit/5
        [HttpPost]
        public ActionResult Edit(EducationViewModel model)
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

        // GET: Education/Delete/5
        public ActionResult Delete(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // POST: Education/Delete/5
        [HttpPost]
        public ActionResult Delete(EducationViewModel model)
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
