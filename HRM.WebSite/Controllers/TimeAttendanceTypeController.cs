using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.Services;
using HRM.ViewModels.Timekeeping;

namespace HRM.WebSite.Controllers
{
    public class TimeAttendanceTypeController : Controller
    {
        private readonly ITimeAttendanceTypeService service;

        public TimeAttendanceTypeController(ITimeAttendanceTypeService service)
        {
            this.service = service;
        }

        // GET: TimeAttendanceType
        public ActionResult Index()
        {
            var list = service.GetTimeAttendanceTypes();
            return View(list);
        }

        // GET: TimeAttendanceType/Details/5
        public ActionResult Details(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // GET: TimeAttendanceType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TimeAttendanceType/Create
        [HttpPost]
        public ActionResult Create(TimeAttendanceTypeViewModel model)
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

        // GET: TimeAttendanceType/Edit/5
        public ActionResult Edit(int id)
        {
           var model = service.GetInfo(id);
            return View(model);
        }

        // POST: TimeAttendanceType/Edit/5
        [HttpPost]
        public ActionResult Edit(TimeAttendanceTypeViewModel model)
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

        // GET: TimeAttendanceType/Delete/5
        public ActionResult Delete(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // POST: TimeAttendanceType/Delete/5
        [HttpPost]
        public ActionResult Delete(TimeAttendanceTypeViewModel model)
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
