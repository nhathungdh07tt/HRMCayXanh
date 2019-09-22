using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.Services;
using HRM.ViewModels.Department;

namespace HRM.WebSite.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService service;

        public DepartmentController(IDepartmentService service)
        {
            this.service = service;
        }

        // GET: Department
        public ActionResult Index()
        {
            var list = service.GetDepartments();
            return View(list);
        }

        // GET: Department/Details/5
        public ActionResult Details(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // GET: Department/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Department/Create
        [HttpPost]
        public ActionResult Create(DepartmentViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Code = service.GetDepartments().Count()+1;
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

        // GET: Department/Edit/5
        public ActionResult Edit(int id)
        {
           var model = service.GetInfo(id);
            return View(model);
        }

        // POST: Department/Edit/5
        [HttpPost]
        public ActionResult Edit(DepartmentViewModel model)
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

        // GET: Department/Delete/5
        public ActionResult Delete(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // POST: Department/Delete/5
        [HttpPost]
        public ActionResult Delete(DepartmentViewModel model)
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
