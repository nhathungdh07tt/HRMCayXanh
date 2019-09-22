using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.Services;
using HRM.ViewModels.Department;

namespace HRM.WebSite.Controllers
{
    public class DepartmentEmployeeController : Controller
    {
        private readonly IDepartmentEmployeeService service;

        public DepartmentEmployeeController(IDepartmentEmployeeService service)
        {
            this.service = service;
        }

        // GET: DepartmentEmployee
        public ActionResult Index()
        {
            var list = service.GetDepartmentEmployees();
            return View(list);
        }

        // GET: DepartmentEmployee/Details/5
        public ActionResult Details(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // GET: DepartmentEmployee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DepartmentEmployee/Create
        [HttpPost]
        public ActionResult Create(DepartmentEmployeeViewModel model)
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

        // GET: DepartmentEmployee/Edit/5
        public ActionResult Edit(int id)
        {
           var model = service.GetInfo(id);
            return View(model);
        }

        // POST: DepartmentEmployee/Edit/5
        [HttpPost]
        public ActionResult Edit(DepartmentEmployeeViewModel model)
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

        // GET: DepartmentEmployee/Delete/5
        public ActionResult Delete(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // POST: DepartmentEmployee/Delete/5
        [HttpPost]
        public ActionResult Delete(DepartmentEmployeeViewModel model)
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
