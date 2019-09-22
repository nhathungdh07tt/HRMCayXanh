using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.Services;
using HRM.ViewModels.Company;

namespace HRM.WebSite.Controllers
{
    public class CompanyChangeHistoryController : Controller
    {
        private readonly ICompanyChangeHistoryService service;
        private readonly ICompanyService _companyService;

        public CompanyChangeHistoryController(ICompanyChangeHistoryService service, ICompanyService companyService)
        {
            this.service = service;
            this._companyService = companyService;
        }

        // GET: CompanyChangeHistory
        public ActionResult Index()
        {
            var list = service.GetCompanyChangeHistorys();
            return View(list);
        }

        // GET: CompanyChangeHistory/Details/5
        public ActionResult Details(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // GET: CompanyChangeHistory/Create
        public ActionResult Create()
        {
            var model = new CompanyChangeHistoryViewModel();
            model.ChangedDate = DateTime.Now.ToLocalTime();
            return View(model);
        }

        // POST: CompanyChangeHistory/Create
        [HttpPost]
        public ActionResult Create(CompanyChangeHistoryViewModel model)
        {
            model.CompanyId = 1;
            try
            {
                if (ModelState.IsValid)
                {
                    var f = Request.Files["SignTitle"];
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
                        model.SignTitle = string.Join("/", avatarpath, pic);
                    }



                    var modelcompany = _companyService.GetInfo(model.CompanyId);
                    modelcompany.VietNamName = model.VietNamName;
                    modelcompany.EnglishName = model.EnglishName;
                    modelcompany.ShortName = model.ShortName;
                    modelcompany.TaxCode = model.TaxCode;
                    modelcompany.Address = model.Address;
                    modelcompany.CharterCapital = model.CharterCapital;
                    modelcompany.ChangedCount = model.ChangedCount;
                    modelcompany.ChangedDate = model.ChangedDate;
                    modelcompany.ChangedContent = model.ChangedContent;
                    modelcompany.Website = model.Website;
                    modelcompany.Mobile = model.Mobile;
                    modelcompany.Fax = model.Fax;
                    modelcompany.Phone = model.Phone;
                    modelcompany.Email = model.Email;
                    modelcompany.BankAccountNumber = model.BankAccountNumber;
                    modelcompany.BankAccountIssuePlace = model.BankAccountIssuePlace;
                    modelcompany.SignName = model.SignName;
                    modelcompany.SignTitle = model.SignTitle;
                    modelcompany.Id = 1;
                    _companyService.Update(modelcompany);

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

        // GET: CompanyChangeHistory/Edit/5
        public ActionResult Edit(int id)
        {
           var model = service.GetInfo(id);
            return View(model);
        }

        // POST: CompanyChangeHistory/Edit/5
        [HttpPost]
        public ActionResult Edit(CompanyChangeHistoryViewModel model)
        {
            model.CompanyId = 1;
            try
            {
                if (ModelState.IsValid)
                {

                    var f = Request.Files["SignTitle"];
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
                        model.SignTitle = string.Join("/", avatarpath, pic);
                    }

                    var modelcompany = _companyService.GetInfo(model.CompanyId);
                    modelcompany.VietNamName = model.VietNamName;
                    modelcompany.EnglishName = model.EnglishName;
                    modelcompany.ShortName = model.ShortName;
                    modelcompany.TaxCode = model.TaxCode;
                    modelcompany.Address = model.Address;
                    modelcompany.CharterCapital = model.CharterCapital;
                  
                    modelcompany.ChangedDate = model.ChangedDate;
                    modelcompany.ChangedContent = model.ChangedContent;
                    modelcompany.Website = model.Website;
                    modelcompany.Mobile = model.Mobile;
                    modelcompany.Fax = model.Fax;
                    modelcompany.Phone = model.Phone;
                    modelcompany.Email = model.Email;
                    modelcompany.BankAccountNumber = model.BankAccountNumber;
                    modelcompany.BankAccountIssuePlace = model.BankAccountIssuePlace;
                    modelcompany.SignName = model.SignName;
                    modelcompany.SignTitle = model.SignTitle;
                    modelcompany.Id = 1;
                    if(modelcompany.ChangedCount==model.ChangedCount)
                    {
                        modelcompany.ChangedCount = model.ChangedCount;
                        _companyService.Update(modelcompany);
                    }
                                      

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

        // GET: CompanyChangeHistory/Delete/5
        public ActionResult Delete(int id)
        {
            var model = service.GetInfo(id);
            return View(model);
        }

        // POST: CompanyChangeHistory/Delete/5
        [HttpPost]
        public ActionResult Delete(CompanyChangeHistoryViewModel model)
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
