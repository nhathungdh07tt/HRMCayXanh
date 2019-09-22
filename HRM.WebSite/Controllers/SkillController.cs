using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.Services;
using HRM.ViewModels.Employee;

namespace HRM.WebSite.Controllers
{
    public class SkillController : Controller
    {
        private readonly ISkillService service;

        public SkillController(ISkillService service)
        {
            this.service = service;
        }

        // GET: Skill
        public ActionResult Index()
        {
            var list = service.GetSkills();
            return View(list);
        }

        // GET: Skill/Details/5
        public ActionResult Details(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // GET: Skill/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Skill/Create
        [HttpPost]
        public ActionResult Create(SkillViewModel model)
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

        // GET: Skill/Edit/5
        public ActionResult Edit(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // POST: Skill/Edit/5
        [HttpPost]
        public ActionResult Edit(SkillViewModel model)
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

        // GET: Skill/Delete/5
        public ActionResult Delete(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // POST: Skill/Delete/5
        [HttpPost]
        public ActionResult Delete(SkillViewModel model)
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
