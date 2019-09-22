using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.Services;
using HRM.ViewModels.Decision;

namespace HRM.WebSite.Controllers
{
    public class DecisionTypeController : Controller
    {
        private readonly IDecisionTypeService service;

        public DecisionTypeController(IDecisionTypeService service)
        {
            this.service = service;
        }

        // GET: DecisionType
        public ActionResult Index()
        {
            var list = service.GetDecisionTypes();
            return View(list);
        }

        // GET: DecisionType/Details/5
        public ActionResult Details(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // GET: DecisionType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DecisionType/Create
        [HttpPost]
        public ActionResult Create(DecisionTypeViewModel model)
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

        // GET: DecisionType/Edit/5
        public ActionResult Edit(int id)
        {
           var model = service.GetInfo(id);
            return View(model);
        }

        // POST: DecisionType/Edit/5
        [HttpPost]
        public ActionResult Edit(DecisionTypeViewModel model)
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

        // GET: DecisionType/Delete/5
        public ActionResult Delete(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // POST: DecisionType/Delete/5
        [HttpPost]
        public ActionResult Delete(DecisionTypeViewModel model)
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
