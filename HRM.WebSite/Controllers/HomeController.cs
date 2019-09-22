using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using HRM.Domain.Entity;
using HRM.Services;
using HRM.ViewModels.System;
using Microsoft.AspNet.Identity;

namespace HRM.WebSite.Controllers
{
   
    public class HomeController : Controller
    {
    
        private readonly  IUserService _userService;
        private readonly IRoleService _roleService;
        public HomeController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }
        
        // GET: Home
        [Authorize]
        public ActionResult Index()
        {
            if (User != null)
            {
                var user = new UserViewModel()
                {
                    Id = Convert.ToInt64(User.Identity.GetUserId()),
                    UserName = User.Identity.GetUserName()
                };

                return View(user);
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
    }
}