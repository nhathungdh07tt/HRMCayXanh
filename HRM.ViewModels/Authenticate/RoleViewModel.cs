using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using HRM.Domain.Entity;
using HRM.ViewModels.Employee;

namespace HRM.ViewModels.Authenticate
{
    public class RoleViewModel
    {
        public RoleViewModel()
        {
          
        }

        public long Id { get; set; }
        public string Name { get; set; }      
       

    }
}
