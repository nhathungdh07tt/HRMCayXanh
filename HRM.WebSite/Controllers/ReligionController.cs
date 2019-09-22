using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.Services;
using HRM.ViewModels.Employee;

namespace HRM.WebSite.Controllers
{
    public class ReligionController : Controller
    {
        private readonly IReligionService service;

        public ReligionController(IReligionService service)
        {
            this.service = service;
        }

        // GET: Religion
        public ActionResult Index()
        {
            var list = service.GetReligions();
            return View(list);
        }

        // GET: Religion/Details/5
        public ActionResult Details(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // GET: Religion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Religion/Create
        [HttpPost]
        public ActionResult Create(ReligionViewModel model)
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

        // GET: Religion/Edit/5
        public ActionResult Edit(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // POST: Religion/Edit/5
        [HttpPost]
        public ActionResult Edit(ReligionViewModel model)
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

        // GET: Religion/Delete/5
        public ActionResult Delete(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // POST: Religion/Delete/5
        [HttpPost]
        public ActionResult Delete(ReligionViewModel model)
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
