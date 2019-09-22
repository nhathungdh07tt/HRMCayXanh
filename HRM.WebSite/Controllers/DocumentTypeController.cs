using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.Services;
using HRM.ViewModels.Document;

namespace HRM.WebSite.Controllers
{
    public class DocumentTypeController : Controller
    {
        private readonly IDocumentTypeService service;

        public DocumentTypeController(IDocumentTypeService service)
        {
            this.service = service;
        }

        // GET: DocumentType
        public ActionResult Index()
        {
            var list = service.GetDocumentTypes();
            return View(list);
        }

        // GET: DocumentType/Details/5
        public ActionResult Details(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // GET: DocumentType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DocumentType/Create
        [HttpPost]
        public ActionResult Create(DocumentTypeViewModel model)
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

        // GET: DocumentType/Edit/5
        public ActionResult Edit(int id)
        {
           var model = service.GetInfo(id);
            return View(model);
        }

        // POST: DocumentType/Edit/5
        [HttpPost]
        public ActionResult Edit(DocumentTypeViewModel model)
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

        // GET: DocumentType/Delete/5
        public ActionResult Delete(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // POST: DocumentType/Delete/5
        [HttpPost]
        public ActionResult Delete(DocumentTypeViewModel model)
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
