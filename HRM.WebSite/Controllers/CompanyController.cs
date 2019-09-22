using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.Services;
using HRM.Services.AutoMapperService;
using HRM.ViewModels.Company;

namespace HRM.WebSite.Controllers
{
    
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            this._companyService = companyService;
        }

        [HttpGet]
        public ActionResult Show()
        {
            var company = _companyService.GetCompanyInfo();
            if (company == null) 
                company = new CompanyViewModel();
            return View(company);
        }

        [HttpGet]
        public ActionResult Edit()
        {
            var company = _companyService.GetCompanyInfo();
            if (company == null)
                company = new CompanyViewModel();

            return View(company);
        }

        [HttpPost]
        public ActionResult Edit(CompanyViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Edit", model);

            if (_companyService.Exists(model.Id))
                _companyService.Update(model);

            return RedirectToAction("Show", "Company");
        }
      
    }
}