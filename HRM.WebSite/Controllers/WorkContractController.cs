using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.Services;
using HRM.ViewModels.Work;

namespace HRM.WebSite.Controllers
{
    public class WorkContractController : Controller
    {
        private readonly IWorkContractService service;

        public WorkContractController(IWorkContractService service)
        {
            this.service = service;
        }

        // GET: WorkContract
        public ActionResult Index()
        {
            var list = service.GetWorkContracts();
            return View(list);
        }

        // GET: WorkContract/Details/5
        public ActionResult Details(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // GET: WorkContract/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WorkContract/Create
        [HttpPost]
        public ActionResult Create(WorkContractViewModel model)
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

        // GET: WorkContract/Edit/5
        public ActionResult Edit(int id)
        {
           var model = service.GetInfo(id);
            return View(model);
        }

        // POST: WorkContract/Edit/5
        [HttpPost]
        public ActionResult Edit(WorkContractViewModel model)
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

        // GET: WorkContract/Delete/5
        public ActionResult Delete(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // POST: WorkContract/Delete/5
        [HttpPost]
        public ActionResult Delete(WorkContractViewModel model)
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
