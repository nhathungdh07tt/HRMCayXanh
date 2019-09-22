using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HRM.Domain.Entity;
using HRM.Services;
using HRM.WebSite.Models;

namespace HRM.WebSite.Api
{
    public class UserController : ApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok();
        }
    }
}
