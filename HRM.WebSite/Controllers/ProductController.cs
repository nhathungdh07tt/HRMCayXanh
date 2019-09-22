using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.Domain.Entity;
using HRM.Services;
using HRM.ViewModels.Employee;
using Excel = Microsoft.Office.Interop.Excel;

namespace HRM.WebSite.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICountryService service;

        public ProductController (ICountryService service)
        {
            this.service = service;
        }

        // GET: Product
        public ActionResult Index()
        {
            return View();
        }


        //[HttpPost]
        public ActionResult Import(HttpPostedFileBase excelfile)
        {
            if(excelfile == null ||excelfile.ContentLength ==0)
            {
                ViewBag.Error = "Vui lòng chọn tệp tin";
                return View("Index");
            }
            else
            {
                if(excelfile.FileName.EndsWith("xls") || excelfile.FileName.EndsWith("xlsx"))
                {                    
                        string pic = System.IO.Path.GetFileName(excelfile.FileName);//System.IO.Path.GetFileName(f.FileName);
                        var avatarpath = "/Uploads/Lichsuluong";
                        string path = System.IO.Path.Combine(Server.MapPath(avatarpath), pic);                   
                        excelfile.SaveAs(path);
                        
                    Excel.Application application = new Excel.Application();
                    Excel.Workbook workbook = application.Workbooks.Open(path);
                    Excel.Worksheet worksheet = workbook.ActiveSheet;
                    Excel.Range range = worksheet.UsedRange;

                    List<CountryViewModel> listProducts = new List<CountryViewModel>();
                    for(int row = 2; row <= range.Rows.Count; row++)
                    {
                        CountryViewModel p = new CountryViewModel();
                        //p.Id = ((Excel.Range)range.Cells[row, 1]).Text;
                        p.Code = ((Excel.Range)range.Cells[row, 2]).Text;
                        p.Name = ((Excel.Range)range.Cells[row, 3]).Text;
                        p.ShortName = ((Excel.Range)range.Cells[row, 4]).Text;
                        //p.Name = decimal.Parse(((Excel.Range)range.Cells[row, 3]).Text);
                        //p.ShortName = int.Parse(((Excel.Range)range.Cells[row, 4]).Text);
                        listProducts.Add(p);                      
                        service.Insert(p);
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
    }
}