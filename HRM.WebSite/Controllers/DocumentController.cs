using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.Services;
using HRM.ViewModels.Document;

namespace HRM.WebSite.Controllers
{
    public class DocumentController : Controller
    {
        private readonly IDocumentService service;
        private readonly IEmployeeService employee; 
        private readonly IDocumentTypeService documentTypeService;
        private readonly IDepartmentService departmentService;
        public DocumentController(IDocumentService service,
             IEmployeeService employee,
            IDocumentTypeService documentTypeService,
            IDepartmentService departmentService
            )
        {
            this.service = service;
            this.employee = employee;
            this.documentTypeService = documentTypeService;
            this.departmentService = departmentService;
        }

        // GET: Document
        public ActionResult Index()
        {
            //DocumentViewModel model = new DocumentViewModel();
            var model = service.GetDocuments();
            //model.Department = departmentService.GetDepartments();
             return View(model);
        }

        public ActionResult IndexCVD()
        {
            var model = service.GetDocuments1();
            //model.Department = departmentService.GetDepartments();
            return View(model);
        }

        // GET: Document/Details/5
        public ActionResult Details(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // GET: Document/Create
        public ActionResult Create()
        {
            var employees = employee.GetEmployeeSelectListItems().Select(x => new {
                Id = x.Id,
                Name = x.LastName + " - " + x.FirstName
            }).ToList();

            ViewBag.Employees = new SelectList(employees, "Id", "Name");

            var documentTypes = this.documentTypeService.GetDocumentTypes().Select(x => new {
                Id = x.Id,
                Name = x.Name
            });

            ViewBag.DocumentTypes = new SelectList(documentTypes, "Id", "Name");

            var departments = this.departmentService.GetDepartments().Select(x => new {
                Id = x.Id,
                Name = x.Name
            });

            ViewBag.Departments = new SelectList(departments, "Id", "Name");

            return View();
        }

        // POST: Document/Create
        [HttpPost]
        public ActionResult Create(DocumentViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var f = Request.Files["Link"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string pic = System.IO.Path.GetFileName(f.FileName);//System.IO.Path.GetFileName(f.FileName);
                        var avatarpath = "/Uploads/Congvandens";
                        string path = System.IO.Path.Combine(Server.MapPath(avatarpath), pic);
                        // file is uploaded
                        f.SaveAs(path);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            f.InputStream.CopyTo(ms);
                            byte[] array = ms.GetBuffer();
                        }
                        model.Link = string.Join("/", avatarpath, pic);
                    }

                    service.Insert(model);
                    service.Save();
                    return RedirectToAction("Index");
                }


                var employees = employee.GetEmployeeSelectListItems().Select(x => new {
                    Id = x.Id,
                    Name = x.LastName + " - " + x.FirstName
                }).ToList();

                ViewBag.Employees = new SelectList(employees, "Id", "Name");

                var documentTypes = this.documentTypeService.GetDocumentTypes().Select(x => new {
                    Id = x.Id,
                    Name = x.Name
                });

                ViewBag.DocumentTypes = new SelectList(documentTypes, "Id", "Name");

                var departments = this.departmentService.GetDepartments().Select(x => new {
                    Id = x.Id,
                    Name = x.Name
                });

                ViewBag.Departments = new SelectList(departments, "Id", "Name");

                return View(model);
                
            }
            catch
            {
                return View();
            }
        }

        public ActionResult CreateCVD()
        {
            var employees = employee.GetEmployeeSelectListItems().Select(x => new {
                Id = x.Id,
                Name = x.LastName + " - " + x.FirstName
            }).ToList();

            ViewBag.Employees = new SelectList(employees, "Id", "Name");

            var documentTypes = this.documentTypeService.GetDocumentTypes().Select(x => new {
                Id = x.Id,
                Name = x.Name
            });

            ViewBag.DocumentTypes = new SelectList(documentTypes, "Id", "Name");

            var departments = this.departmentService.GetDepartments().Select(x => new {
                Id = x.Id,
                Name = x.Name
            });

            ViewBag.Departments = new SelectList(departments, "Id", "Name");

            return View();
        }
        [HttpPost]
        public ActionResult CreateCVD(DocumentViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var f = Request.Files["Link"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string pic = System.IO.Path.GetFileName(f.FileName);//System.IO.Path.GetFileName(f.FileName);
                        var avatarpath = "/Uploads/Congvandens";
                        string path = System.IO.Path.Combine(Server.MapPath(avatarpath), pic);
                        // file is uploaded
                        f.SaveAs(path);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            f.InputStream.CopyTo(ms);
                            byte[] array = ms.GetBuffer();
                        }
                        model.Link = string.Join("/", avatarpath, pic);
                    }

                    service.Insert(model);
                    service.Save();
                    return RedirectToAction("IndexCVD");
                }


                var employees = employee.GetEmployeeSelectListItems().Select(x => new {
                    Id = x.Id,
                    Name = x.LastName + " - " + x.FirstName
                }).ToList();

                ViewBag.Employees = new SelectList(employees, "Id", "Name");

                var documentTypes = this.documentTypeService.GetDocumentTypes().Select(x => new {
                    Id = x.Id,
                    Name = x.Name
                });

                ViewBag.DocumentTypes = new SelectList(documentTypes, "Id", "Name");

                var departments = this.departmentService.GetDepartments().Select(x => new {
                    Id = x.Id,
                    Name = x.Name
                });

                ViewBag.Departments = new SelectList(departments, "Id", "Name");

                return View(model);

            }
            catch
            {
                return View();
            }
        }

        // GET: Document/Edit/5
        public ActionResult Edit(int id)
        {
            var employees = employee.GetEmployeeSelectListItems().Select(x => new {
                Id = x.Id,
                Name = x.LastName + " - " + x.FirstName
            }).ToList();

            ViewBag.Employees = new SelectList(employees, "Id", "Name");

            var documentTypes = this.documentTypeService.GetDocumentTypes().Select(x => new {
                Id = x.Id,
                Name = x.Name
            });

            ViewBag.DocumentTypes = new SelectList(documentTypes, "Id", "Name");

            var departments = this.departmentService.GetDepartments().Select(x => new {
                Id = x.Id,
                Name = x.Name
            });

            ViewBag.Departments = new SelectList(departments, "Id", "Name");

            var model = service.GetInfo(id);
            return View(model);
        }

        // POST: Document/Edit/5
        [HttpPost]
        public ActionResult Edit(DocumentViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var f = Request.Files["Link"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string pic = System.IO.Path.GetFileName(f.FileName);//System.IO.Path.GetFileName(f.FileName);
                        var avatarpath = "/Uploads/Congvandens";
                        string path = System.IO.Path.Combine(Server.MapPath(avatarpath), pic);
                        // file is uploaded
                        f.SaveAs(path);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            f.InputStream.CopyTo(ms);
                            byte[] array = ms.GetBuffer();
                        }
                        model.Link = string.Join("/", avatarpath, pic);
                    }


                    service.Update(model);
                    service.Save();
                    return RedirectToAction("Index");
                }
                var employees = employee.GetEmployeeSelectListItems().Select(x => new {
                    Id = x.Id,
                    Name = x.LastName + " - " + x.FirstName
                }).ToList();

                ViewBag.Employees = new SelectList(employees, "Id", "Name");

                var documentTypes = this.documentTypeService.GetDocumentTypes().Select(x => new {
                    Id = x.Id,
                    Name = x.Name
                });

                ViewBag.DocumentTypes = new SelectList(documentTypes, "Id", "Name");

                var departments = this.departmentService.GetDepartments().Select(x => new {
                    Id = x.Id,
                    Name = x.Name
                });

                ViewBag.Departments = new SelectList(departments, "Id", "Name");
                return View(model);
               
            }
            catch
            {
                return View();
            }
        }


        public ActionResult EditCVDI(int id)
        {
            var employees = employee.GetEmployeeSelectListItems().Select(x => new {
                Id = x.Id,
                Name = x.LastName + " - " + x.FirstName
            }).ToList();

            ViewBag.Employees = new SelectList(employees, "Id", "Name");

            var documentTypes = this.documentTypeService.GetDocumentTypes().Select(x => new {
                Id = x.Id,
                Name = x.Name
            });

            ViewBag.DocumentTypes = new SelectList(documentTypes, "Id", "Name");

            var departments = this.departmentService.GetDepartments().Select(x => new {
                Id = x.Id,
                Name = x.Name
            });

            ViewBag.Departments = new SelectList(departments, "Id", "Name");

            var model = service.GetInfo(id);
            return View(model);
        }

        // POST: Document/Edit/5
        [HttpPost]
        public ActionResult EditCVDI(DocumentViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var f = Request.Files["Link"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string pic = System.IO.Path.GetFileName(f.FileName);//System.IO.Path.GetFileName(f.FileName);
                        var avatarpath = "/Uploads/Congvandens";
                        string path = System.IO.Path.Combine(Server.MapPath(avatarpath), pic);
                        // file is uploaded
                        f.SaveAs(path);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            f.InputStream.CopyTo(ms);
                            byte[] array = ms.GetBuffer();
                        }
                        model.Link = string.Join("/", avatarpath, pic);
                    }


                    service.Update(model);
                    service.Save();
                    return RedirectToAction("IndexCVD");
                }
                var employees = employee.GetEmployeeSelectListItems().Select(x => new {
                    Id = x.Id,
                    Name = x.LastName + " - " + x.FirstName
                }).ToList();

                ViewBag.Employees = new SelectList(employees, "Id", "Name");

                var documentTypes = this.documentTypeService.GetDocumentTypes().Select(x => new {
                    Id = x.Id,
                    Name = x.Name
                });

                ViewBag.DocumentTypes = new SelectList(documentTypes, "Id", "Name");

                var departments = this.departmentService.GetDepartments().Select(x => new {
                    Id = x.Id,
                    Name = x.Name
                });

                ViewBag.Departments = new SelectList(departments, "Id", "Name");
                return View(model);

            }
            catch
            {
                return View();
            }
        }


        // GET: Document/Delete/5
        public ActionResult Delete(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // POST: Document/Delete/5
        [HttpPost]
        public ActionResult Delete(DocumentViewModel model)
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
