using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using HRM.Services;
using HRM.ViewModels.Authenticate;
using HRM.ViewModels.Employee;
using Microsoft.Owin.Security;

namespace HRM.WebSite.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAuthenticateService _authenticateService;
        private readonly IEmployeeService _employeeService;
        private readonly ISalaryHistoryService _salaryHistoryService;

        public AccountController(IAuthenticateService authenticateService, IEmployeeService employeeService, ISalaryHistoryService salaryHistoryService)
        {
            this._authenticateService = authenticateService;
            this._employeeService = employeeService;
            this._salaryHistoryService = salaryHistoryService;
        }


        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(string ReturnUrl = "/")
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            //Session["username"] = model.Username;
            if (!ModelState.IsValid)
                return RedirectToAction("Login", "Account");

            var user = _authenticateService.Login(model);

            if (user == null)
                return RedirectToAction("Login", "Account");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
            };

            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var identity = new ClaimsIdentity(claims, "Cookies");

            _authenticateManager.SignIn(identity);

         
            //EmployeeViewModel query = new EmployeeViewModel();

            var query = _employeeService.GetAll();
            Session["count"] = query.Count();
            var query1 = query.Where(x => x.DateOfBirth.Value.Month == DateTime.Now.Month).ToList();
            Session["sinhnhat"] = query1.Count();
            query = query.Where(x => x.LastName == model.Username).ToList();
            Session["username"] = query[0].FirstName;
            Session["hinhanh"] = query[0].Avatar;
            Session["id"] = query[0].Id;
            long k = (long)Session["id"];
            var laymaphongban = _salaryHistoryService.GetSalaryHistorys1().Where(x => x.EmployeeId == k).ToList();
            Session["maphongban"] = laymaphongban[0].DepartmentId;

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            if (_authenticateManager.User.Identity.IsAuthenticated)
            {
                _authenticateManager.SignOut();
            }

            return RedirectToAction("Login", "Account");
        }

        private IAuthenticationManager _authenticateManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
    }
}