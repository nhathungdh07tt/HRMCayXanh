using HRM.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Domain.Entity
{
    public class Department : Entity<long>
    {
        public Department()
        {
            AssignEmployees = new HashSet<DepartmentEmployee>();
        }

        public string Name { get; set;  }
        public int Code { get; set; }

        public virtual ICollection<DepartmentEmployee> AssignEmployees { get; set; }
    }
}
