using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using HRM.Common.Enum;
using HRM.Services;
using HRM.ViewModels.Contract;
using HRM.WebSite.Attributes;

namespace HRM.WebSite.Controllers
{
    [AuthorizeUser(new[] { UserRoles.Administrator })]
    public class ContractController : Controller
    {
        private readonly IContractService service;
        private readonly IEmployeeService employee;
        private readonly IContractTypeService contractTypeService;

        public ContractController(IContractService service,
            IEmployeeService employee,
            IContractTypeService contractTypeService)
        {
            this.service = service;
            this.employee = employee;
            this.contractTypeService = contractTypeService;
        }

        // GET: ContractHistory
        public ActionResult Index(long? employeeId)
        {

            ContractCollectionViewModel model = new ContractCollectionViewModel {
                Employees = employee.GetAll(),
                Contracts = service.GetContracts()
            };

            if (employeeId.HasValue)
                model.Contracts = service.GetContractByEmployee(employeeId.Value);

            return View(model);
        }

        // GET: ContractHistory/Details/5
        public ActionResult Details(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // GET: ContractHistory/Create
        public ActionResult Create()
        {
            var employees = employee.GetEmployeeSelectListItems().Select(x => new {
                Id = x.Id,
                Name = x.LastName + " - " + x.FirstName
            }).ToList();

            ViewBag.Employees = new SelectList(employees, "Id", "Name");

            var contractTypes = this.contractTypeService.GetContractTypes().Select(x => new {
                Id = x.Id,
                Name = x.Name
            });

            ViewBag.ContractTypes = new SelectList(contractTypes, "Id", "Name");

            return View();
        }

        // POST: ContractHistory/Create
        [HttpPost]
        public ActionResult Create(ContractViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var f = Request.Files["Document"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string pic = System.IO.Path.GetFileName(f.FileName);//System.IO.Path.GetFileName(f.FileName);
                        var avatarpath = "/Uploads/Lichsuhopdongs";
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
                    return RedirectToAction("Index", "Contract", new { employeeId = model.EmployeeId });
                }

                var employees = employee.GetEmployeeSelectListItems().Select(x => new {
                    Id = x.Id,
                    Name = x.LastName + " - " + x.FirstName
                }).ToList();

                ViewBag.Employees = new SelectList(employees, "Id", "Name");

                var contractTypes = this.contractTypeService.GetContractTypes().Select(x => new {
                    Id = x.Id,
                    Name = x.Name
                });
                ViewBag.ContractTypes = new SelectList(contractTypes, "Id", "Name");

                return View(model);

            }
            catch (Exception e)
            {
                return View(model);
            }
        }

        // GET: ContractHistory/Edit/5
        public ActionResult Edit(int id)
        {
            var employees = employee.GetEmployeeSelectListItems().Select(x => new {
                Id = x.Id,
                Name = x.LastName + " - " + x.FirstName
            }).ToList();

            ViewBag.Employees = new SelectList(employees, "Id", "Name");

            var contractTypes = this.contractTypeService.GetContractTypes().Select(x => new {
                Id = x.Id,
                Name = x.Name
            });

            ViewBag.ContractTypes = new SelectList(contractTypes, "Id", "Name");

            var model = service.GetInfo(id);
            return View(model);
        }

        // POST: ContractHistory/Edit/5
        [HttpPost]
        public ActionResult Edit(ContractViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var f = Request.Files["Document"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string pic = System.IO.Path.GetFileName(f.FileName);//System.IO.Path.GetFileName(f.FileName);
                        var avatarpath = "/Uploads/Lichsuhopdongs";
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
                    return RedirectToAction("Index", "Contract", new { employeeId = model.EmployeeId });
                }

                var employees = employee.GetEmployeeSelectListItems().Select(x => new {
                    Id = x.Id,
                    Name = x.LastName + " - " + x.FirstName
                }).ToList();

                ViewBag.Employees = new SelectList(employees, "Id", "Name");

                var contractTypes = this.contractTypeService.GetContractTypes().Select(x => new {
                    Id = x.Id,
                    Name = x.Name
                });

                ViewBag.ContractTypes = new SelectList(contractTypes, "Id", "Name");

                return View(model);

            }
            catch (Exception ex)
            {
                return View(model);
            }
        }

        // GET: ContractHistory/Delete/5
        public ActionResult Delete(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // POST: ContractHistory/Delete/5
        [HttpPost]
        public ActionResult Delete(ContractViewModel model)
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
