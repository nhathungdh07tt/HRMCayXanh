using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain.Base;

namespace HRM.Domain.Entity
{
    public class Timekeeping : AuditableEntity<long>
    {
        [ForeignKey(nameof(Employee))]
        public long EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }


        [ForeignKey(nameof(Department))]
        public long DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        public DateTime? Date { get; set; }

        public string Content { get; set; }
        public string Note { get; set; }

        public string Document { get; set; }

        [ForeignKey(nameof(TimeAttendanceType))]
        public long TimeAttendanceTypeId { get; set; }
        public virtual TimeAttendanceType TimeAttendanceType { get; set; }

    }
}
