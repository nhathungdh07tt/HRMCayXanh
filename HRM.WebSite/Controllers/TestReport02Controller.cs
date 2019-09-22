using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.Services;
using System.Runtime.InteropServices;
using HRM.ViewModels.Employee;
using Excel = Microsoft.Office.Interop.Excel;
using HRM.ViewModels.Report;

namespace HRM.WebSite.Controllers
{
    public class TestReport02Controller : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IReport02Service _service;
        private readonly ISalaryHistoryService _salaryHistoryservice;
        private readonly ISalaryLevelService _salaryLevelService;


        public TestReport02Controller(IEmployeeService employeeService, IReport02Service service, ISalaryHistoryService salaryHistoryservice, ISalaryLevelService salaryLevelService)
        {
            this._employeeService = employeeService;
            this._service = service;
            this._salaryHistoryservice = salaryHistoryservice;
            this._salaryLevelService = salaryLevelService;
        }

        public ActionResult Index()
        {
            var report = _service.GetReport();

            return View(report);
        }

       
        public ActionResult dotuoi(int? department,string salarylever,int? worktitle, int? from, int? to, int? education, int? phongban, DateTime? fromdate, DateTime? todate, int? gender, int? skill, string sugget = "")
        {
                       

            int _depart = department.HasValue ? department.Value : -1;
            int _from = from.HasValue ? from.Value : -1;
            int _to = to.HasValue ? to.Value : -1;


            if (fromdate.HasValue)
            {
                var model = _employeeService.SearchByDateOfBirth(_depart, fromdate, todate, sugget);
                return View(model);
            }
            else if (gender.HasValue)
            {
                var model = _employeeService.SearchByGender(_depart, gender, sugget);
                return View(model);
            }
            else if (education.HasValue)
            {
                var model = _employeeService.SearchByEducation(_depart, education, sugget);
                return View(model);
            }
            else if (skill.HasValue)
            {
                var model = _employeeService.SearchBySkill(_depart, skill, sugget);
                return View(model);
            }
            else if (worktitle.HasValue)
            {
                var query = _salaryHistoryservice.GetSalaryHistorys1();
                var query1 = _salaryLevelService.GetSalaryLevels();
                var query2 = _employeeService.GetAll();
                var model = (from a in query1
                             join b in query
                             on a.Id equals b.SalaryLevelId
                             join p in query2
                             on b.EmployeeId equals p.Id
                             where a.Node == salarylever && a.WorkTitleId == worktitle
                             select new EmployeeViewModel{
                                 Id = p.Id,
                                 //Note = a.Node,
                                 LastName = p.LastName,
                                 FirstName = p.FirstName,
                                 Nationality = p.Nationality,
                                 Gender = p.Gender,
                                 IdentityNo = p.IdentityNo,
                                 DateOfBirth = p.DateOfBirth,
                                 PlaceOfBirth = p.PlaceOfBirth,
                                 DateIssueIdentity = p.DateIssueIdentity,
                                 PlaceIssueIdentity = p.PlaceIssueIdentity,
                                 EthnicGroup = p.EthnicGroup,
                                 Religion = p.Religion,
                                 Address = p.Address,
                                 Phone = p.Phone,
                                 Email = p.Email,
                                 YearDayOff = p.YearDayOff,
                                 Education = p.Education,
                                 DetailEducation = p.DetailEducation,
                                 Certificate = p.Certificate,
                                 CommunistYouthUnion = p.CommunistYouthUnion,
                                 SocialInsuranceNo = p.SocialInsuranceNo,
                                 DateIssueSocialInsurance = p.DateIssueSocialInsurance,
                                 BankAccount = p.BankAccount,
                                 Bank = p.Bank,
                                 DateSignContract = p.DateSignContract,
                                 DateOffContract = p.DateOffContract

                             }).OrderBy(x => x.Id).ToList();                
                return View(model);
            }
            else
            {
                var model = _employeeService.SearchByOld(_depart, _from, _to, sugget);
                return View(model);
            }
        }

        public ActionResult Export()
        {

            Excel.Application application = new Excel.Application();
            Excel.Workbook workbook = application.Workbooks.Add(System.Reflection.Missing.Value);
            Excel.Worksheet worksheet = workbook.ActiveSheet;

            Report02ViewModel pm = new Report02ViewModel();
            var model = _service.GetReport();


            worksheet.Cells[1, 1] = "Chức danh";
            worksheet.Cells[1, 2] = "Tổng";
            worksheet.Cells[1, 3] = "Bậc 1";
            worksheet.Cells[1, 4] = "Bậc 2";
            worksheet.Cells[1, 5] = "Bậc 3";
            worksheet.Cells[1, 6] = "Bậc 4";
            worksheet.Cells[1, 7] = "Bậc 5";
            worksheet.Cells[1, 8] = "Bậc 6";
            worksheet.Cells[1, 9] = "Bậc 7";
            worksheet.Cells[1, 10] = "Bậc 8";            
            int row = 2;
            foreach (Report02ViewModel p in model)
            {
                worksheet.Cells[row, 1] = p.Name;
                worksheet.Cells[row, 2] = p.Tong;
                worksheet.Cells[row, 3] = p.Bac1;
                worksheet.Cells[row, 4] = p.Bac2;
                worksheet.Cells[row, 5] = p.Bac2;
                worksheet.Cells[row, 6] = p.Bac2;
                worksheet.Cells[row, 7] = p.Bac2;
                worksheet.Cells[row, 8] = p.Bac2;
                worksheet.Cells[row, 9] = p.Bac2;
                worksheet.Cells[row, 10] = p.Bac2;               
                row++;
            }

            workbook.SaveAs("d:\\baocao02.xls");
            workbook.Close();
            Marshal.ReleaseComObject(workbook);
            application.Quit();
            Marshal.FinalReleaseComObject(application);
            ViewBag.Result = "Done";
            return View("Export");

        }

    }
}