using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain.Base;

namespace HRM.Domain.Entity
{
    public class TimeAttendanceType : AuditableEntity<long>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal MonthlyBonus { get; set; }
        public string Content1 { get; set; }
        public string Note { get; set; }
        public string Document { get; set; }

        public virtual ICollection<Timekeeping> Timekeepings { get; set; }

    }
}
