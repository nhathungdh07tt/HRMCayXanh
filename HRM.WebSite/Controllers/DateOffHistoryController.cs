using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using HRM.Services;
using HRM.ViewModels.Employee;
using Excel = Microsoft.Office.Interop.Excel;


namespace HRM.WebSite.Controllers
{
    public class DateOffHistoryController : Controller
    {
        private readonly IDateOffHistoryService service;
        private readonly IEmployeeService employee;

        public DateOffHistoryController(IDateOffHistoryService service, IEmployeeService employee)
        {
            this.service = service;
            this.employee = employee;
        }


        public ActionResult Index(long? employeeId)
        {          
            var employees = employee.GetEmployeeSelectListItems().Select(x => new {
                Id = x.Id,
                Name = x.LastName + " - " + x.FirstName
            }).ToList();
            ViewBag.Employees = new SelectList(employees, "Id", "Name");

            DateOffHistoryCollectionViewModel model = new DateOffHistoryCollectionViewModel {
                Employees = employee.GetAll(),
                DateOffHistorys = service.GetDateOffHistorys()
            };
            Session["ngaynghinv"] = 0;
            if (employeeId.HasValue)
            {
                model.DateOffHistorys = service.GetDateOffHistoryByEmployee(employeeId.Value);
                Session["ngaynghinv"] = 1;
                Session["mangaynghinv"] = employeeId;
            }
               

            return View(model);
        }

        // GET: DateOffHistory/Details/5
        public ActionResult Details(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // GET: DateOffHistory/Create
        public ActionResult Create()
        {
            var employees = employee.GetEmployeeSelectListItems().Select(x => new {
                Id = x.Id,
                Name = x.LastName + " - " + x.FirstName
            }).ToList();
            ViewBag.Employees = new SelectList(employees, "Id", "Name");

            var nghitus = new List<KeyValuePair<string, string>> {
                new KeyValuePair<string, string>("0", "Từ sáng"),
                new KeyValuePair<string, string>("1", "Từ chiều")
            };
            ViewBag.Nghitus = new SelectList(nghitus, "Key", "Value");

            var denlucs = new List<KeyValuePair<string, string>> {
                new KeyValuePair<string, string>("0", "Hết sáng"),
                new KeyValuePair<string, string>("1", "Hết chiều")
            };
            ViewBag.Denlucs = new SelectList(denlucs, "Key", "Value");

            var pheps = new List<KeyValuePair<string, string>> {
                new KeyValuePair<string, string>("1", "Có phép"),
                new KeyValuePair<string, string>("0", "Không phép")
            };
            ViewBag.Pheps = new SelectList(pheps, "Key", "Value");

            var TotalDate = new List<KeyValuePair<double, string>> {
               new KeyValuePair<double, string>(0.5, "0.5 ngày"),
                new KeyValuePair<double, string>(1, "1 ngày"),
                 new KeyValuePair<double, string>(1.5, "1.5 ngày"),
                new KeyValuePair<double, string>(2, "2 ngày"),
                 new KeyValuePair<double, string>(2.5, "2.5 ngày"),
                new KeyValuePair<double, string>(3, "3 ngày"),
                 new KeyValuePair<double, string>(3.5, "3.5 ngày"),
                new KeyValuePair<double, string>(4, "4 ngày"),
                 new KeyValuePair<double, string>(4.5, "4.5 ngày"),
                new KeyValuePair<double, string>(5, "5 ngày"),
                new KeyValuePair<double, string>(5.5, "5.5 ngày"),
                new KeyValuePair<double, string>(6, "6 ngày"),
                 new KeyValuePair<double, string>(6.5, "6.5 ngày"),
                new KeyValuePair<double, string>(7, "7 ngày"),
                 new KeyValuePair<double, string>(7.5, "7.5 ngày"),
                new KeyValuePair<double, string>(8, "8 ngày"),
                 new KeyValuePair<double, string>(8.5, "8.5 ngày"),
                new KeyValuePair<double, string>(9, "9 ngày"),
                 new KeyValuePair<double, string>(9.5, "9.5 ngày"),
                new KeyValuePair<double, string>(10, "10 ngày"),
                 new KeyValuePair<double, string>(10.5, "10.5 ngày"),
                new KeyValuePair<double, string>(11, "11 ngày"),
                 new KeyValuePair<double, string>(11.5, "11.5 ngày"),
                new KeyValuePair<double, string>(12, "12 ngày")
            };
            ViewBag.TotalDate = new SelectList(TotalDate, "Key", "Value");
            return View();
        }

        // POST: DateOffHistory/Create
        [HttpPost]
        public ActionResult Create(DateOffHistoryViewModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var Employees = employee.GetInfo(model.EmployeeId);
                    double test = Employees.YearDayOff - model.TotalDate;
                    if (model.HasPermission == "1")
                    {
                        if (test < 0)
                        {
                            TempData["AlertMessage"] = Employees.YearDayOff;

                        }
                        else
                        {
                            Employees.YearDayOff = Employees.YearDayOff - model.TotalDate;
                            employee.Update(Employees);
                            service.Insert(model);
                            service.Save();
                            return RedirectToAction("Index", "DateOffHistory", new { employeeId = model.EmployeeId });
                        }
                    }
                    else
                    {
                        service.Insert(model);
                        service.Save();
                        return RedirectToAction("Index", "DateOffHistory", new { employeeId = model.EmployeeId });
                    }

                }


                var employees = employee.GetEmployeeSelectListItems().Select(x => new {
                    Id = x.Id,
                    Name = x.LastName + " - " + x.FirstName
                }).ToList();
                ViewBag.Employees = new SelectList(employees, "Id", "Name");

                var nghitus = new List<KeyValuePair<string, string>> {
                new KeyValuePair<string, string>("0", "Từ sáng"),
                new KeyValuePair<string, string>("1", "Từ chiều")
            };
                ViewBag.Nghitus = new SelectList(nghitus, "Key", "Value");

                var denlucs = new List<KeyValuePair<string, string>> {
                new KeyValuePair<string, string>("0", "Hết sáng"),
                new KeyValuePair<string, string>("1", "Hết chiều")
            };
                ViewBag.Denlucs = new SelectList(denlucs, "Key", "Value");

                var pheps = new List<KeyValuePair<string, string>> {
                new KeyValuePair<string, string>("1", "Có phép"),
                new KeyValuePair<string, string>("0", "Không phép")
            };
                ViewBag.Pheps = new SelectList(pheps, "Key", "Value");
                var TotalDate = new List<KeyValuePair<double, string>> {
                new KeyValuePair<double, string>(0.5, "0.5 ngày"),
                new KeyValuePair<double, string>(1, "1 ngày"),
                 new KeyValuePair<double, string>(1.5, "1.5 ngày"),
                new KeyValuePair<double, string>(2, "2 ngày"),
                 new KeyValuePair<double, string>(2.5, "2.5 ngày"),
                new KeyValuePair<double, string>(3, "3 ngày"),
                 new KeyValuePair<double, string>(3.5, "3.5 ngày"),
                new KeyValuePair<double, string>(4, "4 ngày"),
                 new KeyValuePair<double, string>(4.5, "4.5 ngày"),
                new KeyValuePair<double, string>(5, "5 ngày"),
                new KeyValuePair<double, string>(5.5, "5.5 ngày"),
                new KeyValuePair<double, string>(6, "6 ngày"),
                 new KeyValuePair<double, string>(6.5, "6.5 ngày"),
                new KeyValuePair<double, string>(7, "7 ngày"),
                 new KeyValuePair<double, string>(7.5, "7.5 ngày"),
                new KeyValuePair<double, string>(8, "8 ngày"),
                 new KeyValuePair<double, string>(8.5, "8.5 ngày"),
                new KeyValuePair<double, string>(9, "9 ngày"),
                 new KeyValuePair<double, string>(9.5, "9.5 ngày"),
                new KeyValuePair<double, string>(10, "10 ngày"),
                 new KeyValuePair<double, string>(10.5, "10.5 ngày"),
                new KeyValuePair<double, string>(11, "11 ngày"),
                 new KeyValuePair<double, string>(11.5, "11.5 ngày"),
                new KeyValuePair<double, string>(12, "12 ngày")
            };
                ViewBag.TotalDate = new SelectList(TotalDate, "Key", "Value");

                return View(model);

            }
            catch (Exception e)
            {
                return View(model);
            }
        }

        // GET: DateOffHistory/Edit/5
        public ActionResult Edit(int id)
        {
            Session["yearofdayId"] = id;
            DateOffHistoryViewModel model = service.GetInfo(id);

            var employees = employee.GetEmployeeSelectListItems().Select(x => new {
                Id = x.Id,
                Name = x.LastName + " - " + x.FirstName
            }).ToList();
            ViewBag.Employees = new SelectList(employees, "Id", "Name");

            var nghitus = new List<KeyValuePair<string, string>> {
                new KeyValuePair<string, string>("0", "Từ sáng"),
                new KeyValuePair<string, string>("1", "Từ chiều")
            };
            ViewBag.Nghitus = new SelectList(nghitus, "Key", "Value");

            var denlucs = new List<KeyValuePair<string, string>> {
                new KeyValuePair<string, string>("0", "Hết sáng"),
                new KeyValuePair<string, string>("1", "Hết chiều")
            };
            ViewBag.Denlucs = new SelectList(denlucs, "Key", "Value");

            var pheps = new List<KeyValuePair<string, string>> {
                new KeyValuePair<string, string>("1", "Có phép"),
                new KeyValuePair<string, string>("0", "Không phép")
            };
            ViewBag.Pheps = new SelectList(pheps, "Key", "Value");

            var TotalDates = new List<KeyValuePair<double, string>> {
                new KeyValuePair<double, string>(0.5, "0.5 ngày"),
                new KeyValuePair<double, string>(1, "1 ngày"),
                 new KeyValuePair<double, string>(1.5, "1.5 ngày"),
                new KeyValuePair<double, string>(2, "2 ngày"),
                 new KeyValuePair<double, string>(2.5, "2.5 ngày"),
                new KeyValuePair<double, string>(3, "3 ngày"),
                 new KeyValuePair<double, string>(3.5, "3.5 ngày"),
                new KeyValuePair<double, string>(4, "4 ngày"),
                 new KeyValuePair<double, string>(4.5, "4.5 ngày"),
                new KeyValuePair<double, string>(5, "5 ngày"),
                new KeyValuePair<double, string>(5.5, "5.5 ngày"),
                new KeyValuePair<double, string>(6, "6 ngày"),
                 new KeyValuePair<double, string>(6.5, "6.5 ngày"),
                new KeyValuePair<double, string>(7, "7 ngày"),
                 new KeyValuePair<double, string>(7.5, "7.5 ngày"),
                new KeyValuePair<double, string>(8, "8 ngày"),
                 new KeyValuePair<double, string>(8.5, "8.5 ngày"),
                new KeyValuePair<double, string>(9, "9 ngày"),
                 new KeyValuePair<double, string>(9.5, "9.5 ngày"),
                new KeyValuePair<double, string>(10, "10 ngày"),
                 new KeyValuePair<double, string>(10.5, "10.5 ngày"),
                new KeyValuePair<double, string>(11, "11 ngày"),
                 new KeyValuePair<double, string>(11.5, "11.5 ngày"),
                new KeyValuePair<double, string>(12, "12 ngày")
            };
            ViewBag.TotalDates = new SelectList(TotalDates, "Key", "Value");

            return View(model);
        }

        // POST: DateOffHistory/Edit/5
        [HttpPost]
        public ActionResult Edit(DateOffHistoryViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var DateOffHistorys = service.GetInfo((int)Session["yearofdayId"]);
                    var Employees = employee.GetInfo(model.EmployeeId);

                    if (DateOffHistorys.HasPermission == "1")
                    {
                        double test1 = Employees.YearDayOff + DateOffHistorys.TotalDate;                      
                        double test = model.TotalDate;
                        double test2 = test1 - test;
                        if (model.HasPermission == "1")
                        {
                            if (test2 < 0)
                            {
                                TempData["AlertMessage"] = test1;

                            }
                            else
                            {
                                Employees.YearDayOff = test2;
                                employee.Update(Employees);
                                service.Update(model);
                                service.Save();
                                return RedirectToAction("Index", "DateOffHistory", new { employeeId = model.EmployeeId });
                            }
                        }
                        else
                        {
                            Employees.YearDayOff = test1;
                            employee.Update(Employees);
                            service.Update(model);
                            service.Save();
                            return RedirectToAction("Index", "DateOffHistory", new { employeeId = model.EmployeeId });
                        }
                    }
                  else
                    {
                        double test2 = Employees.YearDayOff - model.TotalDate;
                        if (model.HasPermission == "1")
                        {
                            if (test2 < 0)
                            {
                                TempData["AlertMessage"] = Employees.YearDayOff;

                            }
                            else
                            {
                                Employees.YearDayOff = Employees.YearDayOff - model.TotalDate;
                                employee.Update(Employees);
                                service.Update(model);
                                service.Save();
                                return RedirectToAction("Index", "DateOffHistory", new { employeeId = model.EmployeeId });
                            }
                        }
                        else
                        {
                           
                            service.Update(model);
                            service.Save();
                            return RedirectToAction("Index", "DateOffHistory", new { employeeId = model.EmployeeId });
                        }
                    }
                 
                   
                }
                var employees = employee.GetEmployeeSelectListItems().Select(x => new {
                    Id = x.Id,
                    Name = x.LastName + " - " + x.FirstName
                }).ToList();
                ViewBag.Employees = new SelectList(employees, "Id", "Name");

                var nghitus = new List<KeyValuePair<string, string>> {
                new KeyValuePair<string, string>("0", "Từ sáng"),
                new KeyValuePair<string, string>("1", "Từ chiều")
            };
                ViewBag.Nghitus = new SelectList(nghitus, "Key", "Value");

                var denlucs = new List<KeyValuePair<string, string>> {
                new KeyValuePair<string, string>("0", "Hết sáng"),
                new KeyValuePair<string, string>("1", "Hết chiều")
            };
                ViewBag.Denlucs = new SelectList(denlucs, "Key", "Value");

                var pheps = new List<KeyValuePair<string, string>> {
                new KeyValuePair<string, string>("1", "Có phép"),
                new KeyValuePair<string, string>("0", "Không phép")
            };
                ViewBag.Pheps = new SelectList(pheps, "Key", "Value");

                var TotalDate = new List<KeyValuePair<double, string>> {
                new KeyValuePair<double, string>(0.5, "0.5 ngày"),
                new KeyValuePair<double, string>(1, "1 ngày"),
                 new KeyValuePair<double, string>(1.5, "1.5 ngày"),
                new KeyValuePair<double, string>(2, "2 ngày"),
                 new KeyValuePair<double, string>(2.5, "2.5 ngày"),
                new KeyValuePair<double, string>(3, "3 ngày"),
                 new KeyValuePair<double, string>(3.5, "3.5 ngày"),
                new KeyValuePair<double, string>(4, "4 ngày"),
                 new KeyValuePair<double, string>(4.5, "4.5 ngày"),
                new KeyValuePair<double, string>(5, "5 ngày"),
                new KeyValuePair<double, string>(5.5, "5.5 ngày"),
                new KeyValuePair<double, string>(6, "6 ngày"),
                 new KeyValuePair<double, string>(6.5, "6.5 ngày"),
                new KeyValuePair<double, string>(7, "7 ngày"),
                 new KeyValuePair<double, string>(7.5, "7.5 ngày"),
                new KeyValuePair<double, string>(8, "8 ngày"),
                 new KeyValuePair<double, string>(8.5, "8.5 ngày"),
                new KeyValuePair<double, string>(9, "9 ngày"),
                 new KeyValuePair<double, string>(9.5, "9.5 ngày"),
                new KeyValuePair<double, string>(10, "10 ngày"),
                 new KeyValuePair<double, string>(10.5, "10.5 ngày"),
                new KeyValuePair<double, string>(11, "11 ngày"),
                 new KeyValuePair<double, string>(11.5, "11.5 ngày"),
                new KeyValuePair<double, string>(12, "12 ngày")
            };
                ViewBag.TotalDate = new SelectList(TotalDate, "Key", "Value");
                return View(model);

            }
            catch
            {
                return View();
            }
        }

        // GET: DateOffHistory/Delete/5
        public ActionResult Delete(int id)
        {
            Session["yearofdayId"] = id;
            var model = service.GetInfo(id);
            return View(model);
        }

        // POST: DateOffHistory/Delete/5
        [HttpPost]
        public ActionResult Delete(DateOffHistoryViewModel model)
        {
           
                var DateOffHistorys = service.GetInfo((int)Session["yearofdayId"]);
                var Employees = employee.GetInfo(DateOffHistorys.EmployeeId);

                if (DateOffHistorys.HasPermission == "1")
                {
                    double test1 = Employees.YearDayOff + DateOffHistorys.TotalDate;
                    Employees.YearDayOff = test1;
                    employee.Update(Employees);
                    service.Delete(model);
                    service.Save();
                    return RedirectToAction("Index");
                }
                else
                {
                    service.Delete(model);
                    service.Save();
                    return RedirectToAction("Index");
                }                                    
           
        }

        public ActionResult Export(long? employeeId)
        {           
            //{
            Excel.Application application = new Excel.Application();
            Excel.Workbook workbook = application.Workbooks.Add(System.Reflection.Missing.Value);
            Excel.Worksheet worksheet = workbook.ActiveSheet;

            DateOffHistoryViewModel pm = new DateOffHistoryViewModel();
            var model = service.GetDateOffHistorys();
         
            if (Session["ngaynghinv"].ToString() =="1")
            {
                employeeId = (long)Session["mangaynghinv"];
                model = service.GetDateOffHistoryByEmployee(employeeId.Value);
            }            
            else
            {
                model = service.GetDateOffHistorys();
            }


            worksheet.Cells[1, 1] = "Mã NV";
            worksheet.Cells[1, 2] = "Tên NV";
            worksheet.Cells[1, 3] = "Nghỉ từ";
            worksheet.Cells[1, 4] = "Từ ngày";
            worksheet.Cells[1, 5] = "Đến lúc";
            worksheet.Cells[1, 6] = "Đến ngày";
            worksheet.Cells[1, 7] = "Xác nhận nghỉ";
            worksheet.Cells[1, 8] = "Số ngày nghỉ";
            worksheet.Cells[1, 9] = "Lý do nghỉ";           

            int row = 2;
            foreach (DateOffHistoryViewModel p in model)
            {
                worksheet.Cells[row, 1] = p.Employee.LastName;
                worksheet.Cells[row, 2] = p.Employee.FirstName;
                worksheet.Cells[row, 3] = p.FromSession;
                worksheet.Cells[row, 4] = p.From;
                worksheet.Cells[row, 5] = p.ToSession;
                worksheet.Cells[row, 6] = p.To;
                worksheet.Cells[row, 7] = p.HasPermission;
                worksheet.Cells[row, 8] = p.TotalDate;
                worksheet.Cells[row, 9] = p.Reason;                
                row++;
            }

            workbook.SaveAs("d:\\lichsuphep.xls");
            workbook.Close();
            Marshal.ReleaseComObject(workbook);
            application.Quit();
            Marshal.FinalReleaseComObject(application);
            ViewBag.Result = "Done";
            return View("Export");

        }
    }
}
