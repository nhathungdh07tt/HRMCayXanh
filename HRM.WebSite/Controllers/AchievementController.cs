using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using HRM.Services;
using HRM.ViewModels.Achievement;
using HRM.ViewModels.Salary;
using HRM.WebSite.Helpers;

namespace HRM.WebSite.Controllers
{
    public class AchievementController : Controller
    {
        private readonly IAchievementService achievementService;
        private readonly IEmployeeService employeeService;

        public AchievementController(IAchievementService achievementService, IEmployeeService employeeService)
        {
            this.achievementService = achievementService;
            this.employeeService = employeeService;
        }

        // GET: Achievement
        public ActionResult Index(long? employeeId)
        {

            AchievementCollectionViewModel model = new AchievementCollectionViewModel {
                Employees = employeeService.GetAll(),
                Archievements = achievementService.GetAchievements()
            };

            if (employeeId.HasValue)
                model.Archievements = achievementService.GetAchievementByEmployee(employeeId.Value);           

            return View(model);
        }

        // GET: Achievement/Details/5
        public ActionResult Details(int id)
        {
            var model = achievementService.GetInfo(id);
            return View(model);
        }

        // GET: Achievement/Create
        public ActionResult Create()
        {            
            var employees = employeeService.GetEmployeeSelectListItems().Select(x => new {
                Id = x.Id,
                Name = x.LastName + " - " + x.FirstName
            }).ToList();
            ViewBag.Employees = new SelectList(employees, "Id", "Name");
            
            return View();
        }

        // POST: Achievement/Create
        [HttpPost]
        public ActionResult Create(AchievementViewModel model)
        {
         try
            { 
                if (ModelState.IsValid)
                {                              
                        var f = Request.Files["Document"];
                        if (f != null && f.ContentLength > 0)
                        {
                            string pic = System.IO.Path.GetFileName(f.FileName);//System.IO.Path.GetFileName(f.FileName);
                            var avatarpath = "/Uploads/Thanhtichs";
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
                        //model.Document = document;
               
                    achievementService.Insert(model);
                    achievementService.Save();
                    return RedirectToAction("Index", "Achievement", new { employeeId = model.EmployeeId });
                }

                var employees = employeeService.GetEmployeeSelectListItems().Select(x => new {
                    Id = x.Id,
                    Name = x.LastName + " - " + x.FirstName
                }).ToList();
                ViewBag.Employees = new SelectList(employees, "Id", "Name");

                return View(model);
        }
            catch (Exception e)
            {
                return View(model);
    }


}

        // GET: Achievement/Edit/5
        public ActionResult Edit(int id)
        {
            var employees = employeeService.GetEmployeeSelectListItems().Select(x => new {
                Id = x.Id,
                Name = x.LastName + " - " + x.FirstName
            }).ToList();

            ViewBag.Employees = new SelectList(employees, "Id", "Name");

            var model = achievementService.GetInfo(id);
            return View(model);
        }

        // POST: Achievement/Edit/5
        [HttpPost]
        public ActionResult Edit(AchievementViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var f = Request.Files["Document"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string pic = System.IO.Path.GetFileName(f.FileName);//System.IO.Path.GetFileName(f.FileName);
                        var avatarpath = "/Uploads/Thanhtichs";
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

                    achievementService.Update(model);
                    achievementService.Save();
                    return RedirectToAction("Index", "Achievement", new { employeeId = model.EmployeeId });
                }

                var employees = employeeService.GetEmployeeSelectListItems().Select(x => new {
                    Id = x.Id,
                    Name = x.LastName + " - " + x.FirstName
                }).ToList();

                ViewBag.Employees = new SelectList(employees, "Id", "Name");

                return View(model);
               
            }
            catch
            {
                return View();
            }
        }

        // GET: Achievement/Delete/5
        public ActionResult Delete(int id)
        {
            var model = achievementService.GetInfo(id);
            return View(model);
        }

        // POST: Achievement/Delete/5
        [HttpPost]
        public ActionResult Delete(AchievementViewModel model)
        {
            try
            {
                achievementService.Delete(model);
                achievementService.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
