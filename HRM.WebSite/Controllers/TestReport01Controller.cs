using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using HRM.Services;
using HRM.ViewModels.Employee;
using HRM.ViewModels.Report;
using Excel = Microsoft.Office.Interop.Excel;

namespace HRM.WebSite.Controllers
{
    public class TestReport01Controller : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IReport01Service _service;
        private readonly ISalaryHistoryService _salaryHistoryservice;

        public TestReport01Controller(IEmployeeService employeeService, IReport01Service service, ISalaryHistoryService salaryHistoryservice)
        {
            this._employeeService = employeeService;
            this._service = service;
            this._salaryHistoryservice = salaryHistoryservice;
        }

        public ActionResult Index()
        {
            var report = _service.GetReport();

            return View(report);
        }

       
        public ActionResult dotuoi(int? department, long? contracttype, int? from, int? to, int? education, int? phongban, DateTime? fromdate, DateTime? todate, int? gender, int? skill, string sugget = "")
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
            else if (contracttype.HasValue)
            {
                var tablenv = _employeeService.GetAll();
                var tablesalaryhistory = _salaryHistoryservice.GetSalaryHistorys1().Where(x=>x.DepartmentId == _depart && x.CurrentContractTypeId == contracttype).ToList();

                var model = (from a in tablesalaryhistory
                             join p in tablenv
                             on a.EmployeeId equals p.Id
                             select new EmployeeViewModel {
                                 Id = p.Id,
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
                             }).OrderBy(x => x.Id);

                //var model = _employeeService.SearchByContractTypes(_depart, contracttype);
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

            Report01ViewModel pm = new Report01ViewModel();
            var model = _service.GetReport();
           

            worksheet.Cells[1, 1] = "Phòng ban";
            worksheet.Cells[1, 2] = "Tổng";
            worksheet.Cells[1, 3] = "Nam";
            worksheet.Cells[1, 4] = "Nữ";
            worksheet.Cells[1, 5] = "Thử việc";
            worksheet.Cells[1, 6] = "6 tháng";
            worksheet.Cells[1, 7] = "12 tháng";
            worksheet.Cells[1, 8] = "24 tháng";
            worksheet.Cells[1, 9] = "36 tháng";
            worksheet.Cells[1, 10] = "Không xác định";
            worksheet.Cells[1, 11] = "Đại học";
            worksheet.Cells[1, 12] = "cao đẳng";
            worksheet.Cells[1, 13] = "Trung cấp";
            worksheet.Cells[1, 14] = "Sơ cấp nghề";
            worksheet.Cells[1, 15] = "Đào tạo tại chỗ";
            //worksheet.Cells[1, 4] = "Trình độ";
            int row = 2;
            foreach (Report01ViewModel p in model)
            {
                worksheet.Cells[row, 1] = p.Name;
                worksheet.Cells[row, 2] = p.tong;
                worksheet.Cells[row, 3] = p.nam;
                worksheet.Cells[row, 4] = p.nu;
                worksheet.Cells[row, 5] = p.thuviec;
                worksheet.Cells[row, 6] = p.thang6;
                worksheet.Cells[row, 7] = p.thang12;
                worksheet.Cells[row, 8] = p.thang24;
                worksheet.Cells[row, 9] = p.thang36;
                worksheet.Cells[row, 10] = p.kxd;
                worksheet.Cells[row, 11] = p.daihoc;
                worksheet.Cells[row, 12] = p.caodang;
                worksheet.Cells[row, 13] = p.trungcap;
                worksheet.Cells[row, 14] = p.socapnghe;
                worksheet.Cells[row, 15] = p.dttc;
                row++;
            }

            workbook.SaveAs("d:\\baocao01.xls");
            workbook.Close();
            Marshal.ReleaseComObject(workbook);
            application.Quit();
            Marshal.FinalReleaseComObject(application);
            ViewBag.Result = "Done";
            return View("Export");

        }



    }
}