using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.Services;
using HRM.ViewModels.Relation;

namespace HRM.WebSite.Controllers
{
    public class RelationController : Controller
    {
        private readonly IRelationService service;
        private readonly IEmployeeService employeeService;

        public RelationController(IRelationService service, IEmployeeService employeeService)
        {
            this.service = service;
            this.employeeService = employeeService;
        }

        // GET: Relation
       
        public ActionResult Index(long? employeeId)
        {

            RelationCollectionViewModel model = new RelationCollectionViewModel {
                Employees = employeeService.GetAll(),
                Relations = service.GetRelations()
            };

            if (employeeId.HasValue)
                model.Relations = service.GetRelationsByEmployee(employeeId.Value);

            return View(model);
        }

        public ActionResult IndexNCT(long? employeeId)
        {

            RelationCollectionViewModel model = new RelationCollectionViewModel {
                Employees = employeeService.GetAll(),
                Relations = service.GetRelationsNCT()
            };

            if (employeeId.HasValue)
                model.Relations = service.GetRelationsByEmployeeNCT(employeeId.Value);

            return View(model);
        }

        // GET: Relation/Details/5
        public ActionResult Details(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // GET: Relation/Create
        public ActionResult Create()
        {
            var employees = employeeService.GetEmployeeSelectListItems().Select(x => new {
                Id = x.Id,
                Name = x.LastName + " - " + x.FirstName
            }).ToList();
            ViewBag.Employees = new SelectList(employees, "Id", "Name");

            var employees1 = employeeService.GetEmployeeSelectListItems1().Select(x => new {
                Id = x.Id,
                Name = x.LastName + " - " + x.FirstName
            }).ToList();
            ViewBag.Employees1 = new SelectList(employees1, "Id", "Name");


            return View();
           
        }

        // POST: Relation/Create
        [HttpPost]
        public ActionResult Create(RelationViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.Insert(model);
                    service.Save();
                    return RedirectToAction("Index");
                }
                var employees = employeeService.GetEmployeeSelectListItems().Select(x => new {
                    Id = x.Id,
                    Name = x.LastName + " - " + x.FirstName
                }).ToList();
                ViewBag.Employees = new SelectList(employees, "Id", "Name");

                var employees1 = employeeService.GetEmployeeSelectListItems1().Select(x => new {
                    Id = x.Id,
                    Name = x.LastName + " - " + x.FirstName
                }).ToList();
                ViewBag.Employees1 = new SelectList(employees, "Id", "Name");
                return View(model);

            }
            catch
            {
                return View();
            }
        }


        public ActionResult CreateNCT()
        {
            var employees = employeeService.GetEmployeeSelectListItems().Select(x => new {
                Id = x.Id,
                Name = x.LastName + " - " + x.FirstName
            }).ToList();
            ViewBag.Employees = new SelectList(employees, "Id", "Name");

            var employees1 = employeeService.GetEmployeeSelectListItems1().Select(x => new {
                Id = x.Id,
                Name = x.LastName + " - " + x.FirstName
            }).ToList();
            ViewBag.Employees1 = new SelectList(employees1, "Id", "Name");


            return View();
        }

        // POST: Relation/Create
        [HttpPost]
        public ActionResult CreateNCT(RelationViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.Insert(model);
                    service.Save();
                    return RedirectToAction("IndexNCT");
                }
                var employees = employeeService.GetEmployeeSelectListItems().Select(x => new {
                    Id = x.Id,
                    Name = x.LastName + " - " + x.FirstName
                }).ToList();
                ViewBag.Employees = new SelectList(employees, "Id", "Name");

                var employees1 = employeeService.GetEmployeeSelectListItems1().Select(x => new {
                    Id = x.Id,
                    Name = x.LastName + " - " + x.FirstName
                }).ToList();
                ViewBag.Employees1 = new SelectList(employees, "Id", "Name");
                return View(model);

            }
            catch
            {
                return View();
            }
        }

        // GET: Relation/Edit/5
        public ActionResult Edit(int id)
        {
            var employees = employeeService.GetEmployeeSelectListItems().Select(x => new {
                Id = x.Id,
                Name = x.LastName + " - " + x.FirstName
            }).ToList();
            ViewBag.Employees = new SelectList(employees, "Id", "Name");

            var employees1 = employeeService.GetEmployeeSelectListItems1().Select(x => new {
                Id = x.Id,
                Name = x.LastName + " - " + x.FirstName
            }).ToList();
            ViewBag.Employees1 = new SelectList(employees, "Id", "Name");

            var model = service.GetInfo(id);
            return View(model);
        }

        // POST: Relation/Edit/5
        [HttpPost]
        public ActionResult Edit(RelationViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.Update(model);
                    service.Save();
                    return RedirectToAction("Index");
                }
                var employees = employeeService.GetEmployeeSelectListItems().Select(x => new {
                    Id = x.Id,
                    Name = x.LastName + " - " + x.FirstName
                }).ToList();
                ViewBag.Employees = new SelectList(employees, "Id", "Name");

                var employees1 = employeeService.GetEmployeeSelectListItems1().Select(x => new {
                    Id = x.Id,
                    Name = x.LastName + " - " + x.FirstName
                }).ToList();
                ViewBag.Employees1 = new SelectList(employees, "Id", "Name");
                return View(model);
               
            }
            catch
            {
                return View();
            }
        }

        // GET: Relation/Delete/5
        public ActionResult Delete(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // POST: Relation/Delete/5
        [HttpPost]
        public ActionResult Delete(RelationViewModel model)
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
