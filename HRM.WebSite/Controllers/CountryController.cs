using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using HRM.Common.Enum;
using HRM.Domain.Entity;
using HRM.Services;
using HRM.ViewModels.Employee;
using HRM.WebSite.Attributes;
using Excel = Microsoft.Office.Interop.Excel;

namespace HRM.WebSite.Controllers
{
    public class CountryController : Controller
    {
        private readonly ICountryService service;

        public CountryController(ICountryService service)
        {
            this.service = service;
        }

        [AuthorizeUser( new[] { UserRoles.Administrator})]
        public ActionResult Index()
        {
            var list = service.GetCountries();
            return View(list);
        }

        // GET: Country/Details/5
        [AuthorizeUser(new[] { UserRoles.Administrator , UserRoles.Employee})]
        public ActionResult Details(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // GET: Country/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Country/Create
        [HttpPost]
        public ActionResult Create(CountryViewModel model)
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

        // GET: Country/Edit/5
        public ActionResult Edit(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // POST: Country/Edit/5
        [HttpPost]
        public ActionResult Edit(CountryViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
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

        // GET: Country/Delete/5
        public ActionResult Delete(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // POST: Country/Delete/5
        [HttpPost]
        public ActionResult Delete(CountryViewModel model)
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

        public ActionResult Import(HttpPostedFileBase excelfile)
        {
            if (excelfile == null || excelfile.ContentLength == 0)
            {
                ViewBag.Error = "Vui lòng chọn tệp tin";
                return View("Index");
            }
            else
            {
                if (excelfile.FileName.EndsWith("xls") || excelfile.FileName.EndsWith("xlsx"))
                {
                    string pic = System.IO.Path.GetFileName(excelfile.FileName);//System.IO.Path.GetFileName(f.FileName);
                    var avatarpath = "/Uploads/Lichsuluong";
                    string path = System.IO.Path.Combine(Server.MapPath(avatarpath), pic);
                    excelfile.SaveAs(path);

                    Excel.Application application = new Excel.Application();
                    Excel.Workbook workbook = application.Workbooks.Open(path);
                    Excel.Worksheet worksheet = workbook.ActiveSheet;
                    Excel.Range range = worksheet.UsedRange;

                    List<Country> listProducts = new List<Country>();
                    for (int row = 2; row <= range.Rows.Count; row++)
                    {
                        Country p = new Country();
                        //p.Id = ((Excel.Range)range.Cells[row, 1]).Text;
                        p.Code = ((Excel.Range)range.Cells[row, 2]).Text;
                        p.Name = ((Excel.Range)range.Cells[row, 3]).Text;
                        p.ShortName = ((Excel.Range)range.Cells[row, 4]).Text;
                        //p.Name = decimal.Parse(((Excel.Range)range.Cells[row, 3]).Text);
                        //p.ShortName = int.Parse(((Excel.Range)range.Cells[row, 4]).Text);
                        listProducts.Add(p);
                        //    service.Insert(p);
                    }
                    ViewBag.ListProducts = listProducts;
                    service.Save();
                    return View("Success");
                }
                else
                {
                    ViewBag.Error = "File bạn chọn không phải là Excel";
                    return View();
                }

            }





        }

        public ActionResult Export()
        {

            try
            {
                Excel.Application application = new Excel.Application();
                Excel.Workbook workbook = application.Workbooks.Add(System.Reflection.Missing.Value);
                Excel.Worksheet worksheet = workbook.ActiveSheet;

                CountryViewModel pm = new CountryViewModel();
                var list = service.GetCountries();

                worksheet.Cells[1, 1] = "Id";
                worksheet.Cells[1, 2] = "Code";
                worksheet.Cells[1, 3] = "Name";
                worksheet.Cells[1, 4] = "ShortName";
                int row = 1;
                foreach (CountryViewModel p in list)
                {
                    worksheet.Cells[row, 1] = p.Id;
                    worksheet.Cells[row, 2] = p.Code;
                    worksheet.Cells[row, 3] = p.Name;
                    worksheet.Cells[row, 4] = p.ShortName;
                    row++;
                }

                workbook.SaveAs("d:\\myexcel.xls");
                workbook.Close();
                Marshal.ReleaseComObject(workbook);
                application.Quit();
                Marshal.FinalReleaseComObject(application);
                ViewBag.Result = "Done";
                return View("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Result = ex.Message;
            }
            return View("Index");

        }

    }
}
