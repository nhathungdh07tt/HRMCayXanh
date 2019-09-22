using HRM.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Domain.Entity
{
    public  class User : AuditableEntity<long>
    {
        public User ()
	    {
         
	    }

        public string UserName { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }

    }
}
