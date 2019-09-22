using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.Services;
using HRM.ViewModels.Salary;
using HRM.ViewModels.Timekeeping;

namespace HRM.WebSite.Controllers
{
    public class TimekeepingController : Controller
    {
        private readonly ITimekeepingService service;
        private readonly ISalaryHistoryService _salaryHistoryService;
        private readonly IEmployeeService employee;
        private readonly ITimeAttendanceTypeService timeAttendanceType;
        private readonly IDepartmentService _departmentService;


        public TimekeepingController(ITimekeepingService service, IDepartmentService departmentService, IEmployeeService employee, ISalaryHistoryService salaryHistoryService, ITimeAttendanceTypeService timeAttendanceType)
        {
            this.service = service;
            this.employee = employee;
            this._salaryHistoryService = salaryHistoryService;
            this.timeAttendanceType = timeAttendanceType;
            this._departmentService = departmentService;
        }

        // GET: Timekeeping
        public ActionResult Index()
        {
            var timeAttendanceTypes = timeAttendanceType.GetTimeAttendanceTypes();
            ViewBag.TimeAttendanceTypes = new SelectList(timeAttendanceTypes, "Id", "Code");
            long k = (long)Session["maphongban"];

            SalaryHistoryCollectionViewModel model = new SalaryHistoryCollectionViewModel {
                Employees = employee.GetAll(),
                SalaryHistorys = _salaryHistoryService.GetSalaryHistorys1().Where(x => x.DepartmentId == k).ToList()
            };
           
            return View(model);
        }

        // GET: Timekeeping/Details/5
        public ActionResult Details(long id)
        {
            
           


            var model = service.GetInfo(id);
            return View(model);
        }

        // GET: Timekeeping/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Timekeeping/Create
        [HttpPost]
        public ActionResult Create(TimekeepingViewModel model)
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

        // GET: Timekeeping/Edit/5
        public ActionResult Edit(long id)
        {
            TimekeepingViewModel timekeepingViewModel = service.GetInfo(id);

            if (timekeepingViewModel == null)
            {
                return RedirectToAction("Index", "Timekeeping");
            }
            var departments = _departmentService.GetDepartments();
            ViewBag.Departments = new SelectList(departments, "Id", "Name");

            var contens = new List<KeyValuePair<string, string>> {
                new KeyValuePair<string, string>("BT", "Bình thường"),
                new KeyValuePair<string, string>("TC", "Tăng ca")
            };
            ViewBag.Contens = new SelectList(contens, "Key", "Value");
            var timeAttendanceTypes = timeAttendanceType.GetTimeAttendanceTypes();
            ViewBag.TimeAttendanceTypes = new SelectList(timeAttendanceTypes, "Id", "Code");

            var model = service.GetInfo(id);
            return View(model);
        }

        // POST: Timekeeping/Edit/5
        [HttpPost]
        public ActionResult Edit(TimekeepingViewModel model)
        {
            //model.EmployeeId = model.Employee.Id;
            //model.DepartmentId = 1;
            //model.TimeAttendanceTypeId = model.TimeAttendanceType.Id;
            try
            {
                if (ModelState.IsValid)
                {
                    service.Update(model);
                    service.Save();
                    return RedirectToAction("Index");
                }
                var departments = _departmentService.GetDepartments();
                ViewBag.Departments = new SelectList(departments, "Id", "Name");

                var contens = new List<KeyValuePair<string, string>> {
                new KeyValuePair<string, string>("BT", "Bình thường"),
                new KeyValuePair<string, string>("TC", "Tăng ca")
            };
                ViewBag.Contens = new SelectList(contens, "Key", "Value");

                var timeAttendanceTypes = timeAttendanceType.GetTimeAttendanceTypes();
                ViewBag.TimeAttendanceTypes = new SelectList(timeAttendanceTypes, "Id", "Code");

                return View(model);
               
            }
            catch
            {
                return View();
            }
        }

        // GET: Timekeeping/Delete/5
        public ActionResult Delete(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // POST: Timekeeping/Delete/5
        [HttpPost]
        public ActionResult Delete(TimekeepingViewModel model)
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

        [HttpPost]
        public ActionResult KiemTraChamCong(TimekeepingViewModel model)
        {
          
           

            var tontai = service.GetTimekeepings().Where(x => x.EmployeeId == model.EmployeeId && x.Date == model.Date);
            if (tontai != null && tontai.Any())
                return Json(new { daCham = true, chamCong = string.Join(", ", tontai.Select(c => c.TimeAttendanceType.Code)) });
            return Json(new { daCham = false, chamCong = string.Empty });
        }

        [HttpPost]
        public ActionResult Luu(TimekeepingViewModel model)
        {
                      

            service.Insert(model);
            service.Save();          
            return Json(1);
        }

        public ActionResult HistoryTimekeeping(int? department, int? employeess, DateTime? Date)
        {
            int _depart = department.HasValue ? department.Value : -1;
            int _employeess = employeess.HasValue ? employeess.Value : -1;
            var departments = _departmentService.GetDepartments();
            ViewBag.Departments = new SelectList(departments, "Id", "Name");

            var employees = employee.GetEmployeeSelectListItems().Select(x => new {
                Id = x.Id,
                Name = x.LastName + " - " + x.FirstName
            }).ToList();
            ViewBag.Employees = new SelectList(employees, "Id", "Name");

            if(department>0 || employeess>0)
            {
                var model = service.Search(_depart, _employeess, Date);
                return View(model);
            }
            else
            {
                var model = service.GetTimekeepings();
                return View(model);
            }                  
           
        }

        public ActionResult ReportTimekeeping()
        {
            //var timekeepings = service.GetTimekeepings();
            //ViewBag.Timekeepings = new SelectList(timekeepings, "Id", "Date");

            var monthInt = DateTime.Now.Month;
            var model1 = service.GetTimekeepings().Where(x => x.Date.Value.Month == monthInt).ToList();
                var model = model1
                        .GroupBy(x => new {
                            //x.MucTieuName,
                            x.Employee.LastName,
                            x.Employee.FullName,
                            x.Department.Name,
                            x.Content,
                            x.TimeAttendanceType.Code
                        },
                        x => new {
                            x.Date,
                            //x.NgayChamCong,
                            x.TimeAttendanceType.Code
                        }, (key, g) => new { key = key, NgayChamCong = g.ToList() });

            return View(model);
            

        }
    }
}
