using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.Services;
using HRM.ViewModels.Work;

namespace HRM.WebSite.Controllers
{
    public class WorkTitleController : Controller
    {
        private readonly IWorkTitleService service;

        public WorkTitleController(IWorkTitleService service)
        {
            this.service = service;
        }

        // GET: WorkTitle
        public ActionResult Index()
        {
            var list = service.GetWorkTitles();
            return View(list);
        }

        // GET: WorkTitle/Details/5
        public ActionResult Details(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // GET: WorkTitle/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WorkTitle/Create
        [HttpPost]
        public ActionResult Create(WorkTitleViewModel model)
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

        // GET: WorkTitle/Edit/5
        public ActionResult Edit(int id)
        {
           var model = service.GetInfo(id);
            return View(model);
        }

        // POST: WorkTitle/Edit/5
        [HttpPost]
        public ActionResult Edit(WorkTitleViewModel model)
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

        // GET: WorkTitle/Delete/5
        public ActionResult Delete(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // POST: WorkTitle/Delete/5
        [HttpPost]
        public ActionResult Delete(WorkTitleViewModel model)
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
