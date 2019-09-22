using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.Services;
using HRM.ViewModels.Decision;
using HRM.WebSite.Helpers;

namespace HRM.WebSite.Controllers
{
    public class DecisionController : Controller
    {
        private readonly IDecisionService service;
        private readonly IEmployeeService employee;
        private readonly IDecisionTypeService _decisiontypeService;      

        public DecisionController(IDecisionService service, IEmployeeService employee, IDecisionTypeService decisiontypeService)
        {
            this.service = service;
            this.employee = employee;
            this._decisiontypeService = decisiontypeService;
        }

        // GET: Decision
        public ActionResult Index(long? employeeId)
        {          

            DecisionCollectionViewModel model = new DecisionCollectionViewModel {
                Employees = employee.GetAll(),
                Decisions = service.GetDecisions()
            };

            if (employeeId.HasValue)
                model.Decisions = service.GetDecisionByEmployee(employeeId.Value);

            return View(model);
        }

        // GET: Decision/Details/5
        public ActionResult Details(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // GET: Decision/Create
        public ActionResult Create()
        {
            var employees = employee.GetEmployeeSelectListItems().Select(x => new {
                Id = x.Id,
                Name = x.LastName + " - " + x.FirstName
            }).ToList();
            ViewBag.Employees = new SelectList(employees, "Id", "Name");

            var decisiontypes = _decisiontypeService.GetDecisionTypes();
            ViewBag.DecisionTypes = new SelectList(decisiontypes, "Id", "Name");           

            return View();
        }

        // POST: Decision/Create
        [HttpPost]
        public ActionResult Create(DecisionViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var f = Request.Files["Document"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string pic = System.IO.Path.GetFileName(f.FileName);//System.IO.Path.GetFileName(f.FileName);
                        var avatarpath = "/Uploads/Lichsuquyetdinhs";
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
                    return RedirectToAction("Index", "Decision", new { employeeId = model.EmployeeId });
                }

                var employees = employee.GetEmployeeSelectListItems().Select(x => new {
                    Id = x.Id,
                    Name = x.Id + " - " + x.FirstName + " " + x.LastName
                }).ToList();
                ViewBag.Employees = new SelectList(employees, "Id", "Name");

                var decisiontypes = _decisiontypeService.GetDecisionTypes();
                ViewBag.DecisionTypes = new SelectList(decisiontypes, "Id", "Name");

                return View(model);
                
            }
            catch (Exception e)
            {
                return View(model);
            }
        }

        // GET: Decision/Edit/5
        public ActionResult Edit(int id)
        {
            var employees = employee.GetEmployeeSelectListItems().Select(x => new {
                Id = x.Id,
                Name = x.LastName + " - " + x.FirstName
            }).ToList();
            ViewBag.Employees = new SelectList(employees, "Id", "Name");

            var decisiontypes = _decisiontypeService.GetDecisionTypes();
            ViewBag.DecisionTypes = new SelectList(decisiontypes, "Id", "Name");

            var model = service.GetInfo(id);
            return View(model);
        }

        // POST: Decision/Edit/5
        [HttpPost]
        public ActionResult Edit(DecisionViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var f = Request.Files["Document"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string pic = System.IO.Path.GetFileName(f.FileName);//System.IO.Path.GetFileName(f.FileName);
                        var avatarpath = "/Uploads/Lichsuquyetdinhs";
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
                    return RedirectToAction("Index", "Decision", new { employeeId = model.EmployeeId });
                }

                var employees = employee.GetEmployeeSelectListItems().Select(x => new {
                    Id = x.Id,
                    Name = x.Id + " - " + x.FirstName + " " + x.LastName
                }).ToList();
                ViewBag.Employees = new SelectList(employees, "Id", "Name");

                return View(model);
               
            }
            catch
            {
                return View();
            }
        }

        // GET: Decision/Delete/5
        public ActionResult Delete(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // POST: Decision/Delete/5
        [HttpPost]
        public ActionResult Delete(DecisionViewModel model)
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
