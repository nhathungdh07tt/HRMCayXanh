using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.ViewModels.Employee
{
    public class EmployeeSkillViewModel
    {
        public long Id { get; set; }
        public SkillViewModel Skill { get; set; }
        public EmployeeViewModel Employee { get; set; }

    }
}
