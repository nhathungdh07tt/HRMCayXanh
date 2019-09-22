using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain.Base;

namespace HRM.Domain.Entity
{
    public class Skill : Entity<long>
    {
        public Skill()
        {
            Employees = new List<EmployeeSkill>();
        }

        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual ICollection<EmployeeSkill> Employees { get; set; }
    }
}
