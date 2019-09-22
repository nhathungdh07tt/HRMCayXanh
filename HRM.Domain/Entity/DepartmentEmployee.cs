using HRM.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Domain.Entity
{
    public class DepartmentEmployee : Entity<long>
    {
        public DepartmentEmployee()
        {
            
        }

        [ForeignKey(nameof(Department))]
        public long DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        [ForeignKey(nameof(Employee))]
        public long EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
