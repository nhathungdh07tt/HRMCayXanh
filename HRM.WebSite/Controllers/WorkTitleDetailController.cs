using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.Services;
using HRM.ViewModels.Work;
using HRM.WebSite.Helpers;

namespace HRM.WebSite.Controllers
{
    public class WorkTitleDetailController : Controller
    {
        private readonly IWorkTitleDetailService service;
        private readonly IWorkTitleService _workTitleService;

        public WorkTitleDetailController(IWorkTitleDetailService service, IWorkTitleService workTitleService)
        {
            this.service = service;
            this._workTitleService = workTitleService;
        }

        // GET: WorkTitleDetail
        public ActionResult Index()
        {
            var list = service.GetWorkTitleDetails();
            return View(list);
        }

        // GET: WorkTitleDetail/Details/5
        public ActionResult Details(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // GET: WorkTitleDetail/Create
        public ActionResult Create()
        {            
            var workTitles = _workTitleService.GetWorkTitles();          
            ViewBag.WorkTitles = new SelectList(workTitles, "Id", "Name");
            return View();
        }

        // POST: WorkTitleDetail/Create
        [HttpPost]
        public ActionResult Create(WorkTitleDetailViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var f = Request.Files["Document"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string pic = System.IO.Path.GetFileName(f.FileName);//System.IO.Path.GetFileName(f.FileName);
                        var avatarpath = "/Uploads/Chitietchucdanh";
                        string path = System.IO.Path.Combine(Server.MapPath(avatarpath), pic);
                        // file is uploaded
                        f.SaveAs(path);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            f.InputStream.CopyTo(ms);
                            byte[] array = ms.GetBuffer();
                        }
                        model.Document = string.Join("/", avatarpath, pic);
                    }

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

        // GET: WorkTitleDetail/Edit/5
        public ActionResult Edit(int id)
        {
            var workTitles = _workTitleService.GetWorkTitles();
            ViewBag.WorkTitles = new SelectList(workTitles, "Id", "Name");
            var model = service.GetInfo(id);
            return View(model);
        }

        // POST: WorkTitleDetail/Edit/5
        [HttpPost]
        public ActionResult Edit(WorkTitleDetailViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var f = Request.Files["Document"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string pic = System.IO.Path.GetFileName(f.FileName);//System.IO.Path.GetFileName(f.FileName);
                        var avatarpath = "/Uploads/Chitietchucdanh";
                        string path = System.IO.Path.Combine(Server.MapPath(avatarpath), pic);
                        // file is uploaded
                        f.SaveAs(path);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            f.InputStream.CopyTo(ms);
                            byte[] array = ms.GetBuffer();
                        }
                        model.Document = string.Join("/", avatarpath, pic);
                    }



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

        // GET: WorkTitleDetail/Delete/5
        public ActionResult Delete(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // POST: WorkTitleDetail/Delete/5
        [HttpPost]
        public ActionResult Delete(WorkTitleDetailViewModel model)
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
