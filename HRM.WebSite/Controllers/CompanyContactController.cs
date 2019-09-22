using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.Services;
using HRM.ViewModels.Company;

namespace HRM.WebSite.Controllers
{
    public class CompanyContactController : Controller
    {
        private readonly ICompanyContactService service;

        public CompanyContactController(ICompanyContactService service)
        {
            this.service = service;
        }

        // GET: CompanyContact
        public ActionResult Index()
        {
            var list = service.GetCompanyContacts();
            return View(list);
        }

        // GET: CompanyContact/Details/5
        public ActionResult Details(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // GET: CompanyContact/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompanyContact/Create
        [HttpPost]
        public ActionResult Create(CompanyContactViewModel model)
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

        // GET: CompanyContact/Edit/5
        public ActionResult Edit(int id)
        {
           var model = service.GetInfo(id);
            return View(model);
        }

        // POST: CompanyContact/Edit/5
        [HttpPost]
        public ActionResult Edit(CompanyContactViewModel model)
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

        // GET: CompanyContact/Delete/5
        public ActionResult Delete(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // POST: CompanyContact/Delete/5
        [HttpPost]
        public ActionResult Delete(CompanyContactViewModel model)
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
