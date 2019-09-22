using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.Services;
using HRM.ViewModels.Employee;

namespace HRM.WebSite.Controllers
{
    public class EthnicGroupController : Controller
    {
        private readonly IEthnicGroupService service;

        public EthnicGroupController(IEthnicGroupService service)
        {
            this.service = service;
        }

        // GET: EthnicGroup
        public ActionResult Index()
        {
            var list = service.GetEthnicGroups();
            return View(list);
        }

        // GET: EthnicGroup/Details/5
        public ActionResult Details(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // GET: EthnicGroup/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EthnicGroup/Create
        [HttpPost]
        public ActionResult Create(EthnicGroupViewModel model)
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

        // GET: EthnicGroup/Edit/5
        public ActionResult Edit(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // POST: EthnicGroup/Edit/5
        [HttpPost]
        public ActionResult Edit(EthnicGroupViewModel model)
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

        // GET: EthnicGroup/Delete/5
        public ActionResult Delete(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // POST: EthnicGroup/Delete/5
        [HttpPost]
        public ActionResult Delete(EthnicGroupViewModel model)
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
