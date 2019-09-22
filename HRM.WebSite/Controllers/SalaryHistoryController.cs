using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.Services;
using HRM.ViewModels.Salary;
using HRM.WebSite.Helpers;

namespace HRM.WebSite.Controllers
{
    public class SalaryHistoryController : Controller
    {
        private readonly ISalaryHistoryService service;
        private readonly IEmployeeService employee;
        private readonly IWorkTitleDetailService _worktitledetailService;
        private readonly ISalaryLevelService _salarylevelService;
        private readonly IDepartmentService _departmentService;
        private readonly IContractTypeService contractTypeService;

        public SalaryHistoryController(ISalaryHistoryService service,
            IEmployeeService employee,
            IWorkTitleDetailService worktitledetailService,
            ISalaryLevelService salarylevelService,
             IContractTypeService contractTypeService,
            IDepartmentService departmentService)
        {
            this.service = service;
            this.employee = employee;
            this._worktitledetailService = worktitledetailService;
            this._salarylevelService = salarylevelService;
            this._departmentService = departmentService;
            this.contractTypeService = contractTypeService;
        }

        // GET: SalaryHistory

        public ActionResult Index(long? employeeId, long? departmentId, long? WorkTitleDetailId)
        {

            //SalaryHistoryCollectionViewModel model = new SalaryHistoryCollectionViewModel()
            //{
            //    model.Employees = employee.GetAll()
            //    model.SalaryHistorys = service.GetSalaryHistorys()
            //};
            
            SalaryHistoryCollectionViewModel model = new SalaryHistoryCollectionViewModel {
                Employees = employee.GetAll(),
                SalaryHistorys = service.GetSalaryHistorys()
                
            };
          
            if (employeeId.HasValue)            
                model.SalaryHistorys = service.GetSalaryHistoryByEmployee(employeeId.Value);
            if (departmentId.HasValue)
                model.Departments = service.GetSalaryHistoryByDepartment(departmentId.Value);
            if (WorkTitleDetailId.HasValue)
                model.WorkTitleDetails = service.GetSalaryHistoryByWorkTitleDetail(WorkTitleDetailId.Value);


            return View(model);
        }

        // GET: SalaryHistory/Details/5
        public ActionResult Details(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // GET: SalaryHistory/Create
        public ActionResult Create()
        {
            var model = new SalaryHistoryViewModel();
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


            var worktitledetails = _worktitledetailService.GetWorkTitleDetails();
            ViewBag.Worktitledetails = new SelectList(worktitledetails, "Id", "Name");

            var worktitledetail = worktitledetails.FirstOrDefault();
            var worktitledetailId = worktitledetail != null ? worktitledetail.Id : 0;
            var salarylevels = _salarylevelService.GetSalaryLevelByWorkTitle(worktitledetailId).Select(x => new {
                Id = x.Id,
                Name = x.WorkTitleId + " - " + x.Code + " - Tiền lương: " + x.MonthlySalary
            }).ToList();
            ViewBag.Salarylevels = new SelectList(salarylevels, "Id", "Name");

            var departments = _departmentService.GetDepartments();
            ViewBag.Departments = new SelectList(departments, "Id", "Name");

            model.From = DateTime.Today.ToLocalTime();
            model.To = DateTime.Today.ToLocalTime();

            return View(model);
        }

        // POST: SalaryHistory/Create
        [HttpPost]
        public ActionResult Create(SalaryHistoryViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var f = Request.Files["Document"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string pic = System.IO.Path.GetFileName(f.FileName);//System.IO.Path.GetFileName(f.FileName);
                        var avatarpath = "/Uploads/Lichsuluong";
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
                    return RedirectToAction("Index", "SalaryHistory", new { employeeId = model.EmployeeId });
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


                var worktitledetails = _worktitledetailService.GetWorkTitleDetails();
                ViewBag.Worktitledetails = new SelectList(worktitledetails, "Id", "Name");

                var worktitledetail = worktitledetails.FirstOrDefault();
                var worktitledetailId = worktitledetail != null ? worktitledetail.Id : 0;
                var salarylevels = _salarylevelService.GetSalaryLevelByWorkTitle(worktitledetailId).Select(x => new {
                    Id = x.Id,
                    Name = x.WorkTitleId + " - " + x.Code + " - Tiền lương: " + x.MonthlySalary
                }).ToList();
                ViewBag.Salarylevels = new SelectList(salarylevels, "Id", "Name");

                var departments = _departmentService.GetDepartments();
                ViewBag.Departments = new SelectList(departments, "Id", "Name");


                return View(model);

            }
            catch (Exception e)
            {
                return View();
            }
        }

        // GET: SalaryHistory/Edit/5
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


            var worktitledetails = _worktitledetailService.GetWorkTitleDetails();
            ViewBag.Worktitledetails = new SelectList(worktitledetails, "Id", "Name");

            var worktitledetail = worktitledetails.FirstOrDefault();
            var worktitledetailId = worktitledetail != null ? worktitledetail.Id : 0;
            var salarylevels = _salarylevelService.GetSalaryLevelByWorkTitle(worktitledetailId).Select(x => new {
                Id = x.Id,
                Name = x.WorkTitleId + " - " + x.Code + " - Tiền lương: " + x.MonthlySalary
            }).ToList();
            ViewBag.Salarylevels = new SelectList(salarylevels, "Id", "Name");

            var departments = _departmentService.GetDepartments();
            ViewBag.Departments = new SelectList(departments, "Id", "Name");


            var model = service.GetInfo(id);
            return View(model);
        }

        // POST: SalaryHistory/Edit/5
        [HttpPost]
        public ActionResult Edit(SalaryHistoryViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var f = Request.Files["Document"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string pic = System.IO.Path.GetFileName(f.FileName);//System.IO.Path.GetFileName(f.FileName);
                        var avatarpath = "/Uploads/Lichsuluong";
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
                    return RedirectToAction("Index", "SalaryHistory", new { employeeId = model.EmployeeId });
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


                var worktitledetails = _worktitledetailService.GetWorkTitleDetails();
                ViewBag.Worktitledetails = new SelectList(worktitledetails, "Id", "Name");

                var worktitledetail = worktitledetails.FirstOrDefault();
                var worktitledetailId = worktitledetail != null ? worktitledetail.Id : 0;
                var salarylevels = _salarylevelService.GetSalaryLevelByWorkTitle(worktitledetailId).Select(x => new {
                    Id = x.Id,
                    Name = x.WorkTitleId + " - " + x.Code + " - Tiền lương: " + x.MonthlySalary
                }).ToList();
                ViewBag.Salarylevels = new SelectList(salarylevels, "Id", "Name");

                var departments = _departmentService.GetDepartments();
                ViewBag.Departments = new SelectList(departments, "Id", "Name");

                return View(model);

            }
            catch
            {
                return View();
            }
        }

        // GET: SalaryHistory/Delete/5
        public ActionResult Delete(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // POST: SalaryHistory/Delete/5
        [HttpPost]
        public ActionResult Delete(SalaryHistoryViewModel model)
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

        // GET: SalaryHistory/GetSalaryLevelsBytWorkTitleDetailsId
        public JsonResult GetSalaryLevelsBytWorkTitleDetailsId(long workTitleDetailsId)
        {
            var worktitledetail = _worktitledetailService.Find(workTitleDetailsId);

            var salarylevels = _salarylevelService.GetSalaryLevelByWorkTitle(worktitledetail.WorkTitleId)
             .Select(x => new {
                 id = x.Id,
                 text = x.WorkTitleId + " - " + x.Code + " - Tiền lương: " + x.MonthlySalary
             }).ToList();

            return Json(salarylevels, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Thongketongquat()
        {
            
            return View();
        }
    }
}
