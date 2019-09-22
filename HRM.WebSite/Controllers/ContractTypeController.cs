using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.Services;
using HRM.ViewModels.Contract;

namespace HRM.WebSite.Controllers
{
    public class ContractTypeController : Controller
    {
        private IContractTypeService _contractTypeService;

        public ContractTypeController(IContractTypeService contractTypeService)
        {
            _contractTypeService = contractTypeService;
        }

        // GET: ContractType
        public ActionResult Index()
        {
            var contractTypes = _contractTypeService.GetContractTypes();
            return View(contractTypes);
        }

        // GET: ContractType/Details/5
        public ActionResult Details(int id)
        {
            var contractType = _contractTypeService.GetInfo(id);
            return View(contractType);
        }

        // GET: ContractType/Create
        [HttpGet]
        public ActionResult Create()
        {
            var contractType = _contractTypeService.GetInfo(0);          
            return View(contractType);
        }

        // POST: ContractType/Create
        [HttpPost]
        public ActionResult Create(ContractTypeViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _contractTypeService.Insert(model);
                    _contractTypeService.Save();
                    return RedirectToAction("Index");
                }

                return View(model);
            }
            catch
            {
                return View();
            }
        }

        // GET: ContractType/Edit/5
        public ActionResult Edit(int id)
        {
            var contractType = _contractTypeService.GetInfo(id);
            if (contractType == null || contractType.Id <= 0)
                return HttpNotFound();

            return View(contractType);
        }

        // POST: ContractType/Edit/5
        [HttpPost]
        public ActionResult Edit(ContractTypeViewModel model)
        {
            try
            {
                var contractType = _contractTypeService.GetInfo(model.Id);
                if (contractType == null || contractType.Id <= 0)
                    return HttpNotFound();

                _contractTypeService.Update(model);
                _contractTypeService.Save();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ContractType/Delete/5
        public ActionResult Delete(int id)
        {
            var contractType = _contractTypeService.GetInfo(id);
            if (contractType == null || contractType.Id <= 0)
                return HttpNotFound();

            return View(contractType);
        }

        // POST: ContractType/Delete/5
        [HttpPost]
        public ActionResult Delete(ContractTypeViewModel model)
        {
            try
            {
                _contractTypeService.Delete(model);
                _contractTypeService.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
