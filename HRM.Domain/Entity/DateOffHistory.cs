using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain.Base;

namespace HRM.Domain.Entity
{
    public class DateOffHistory : AuditableEntity<long>
    {
        [ForeignKey(nameof(Employee))]
        public long EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string FromSession { get; set; }
        public string ToSession { get; set; }
        public string HasPermission { get; set; }
        public double TotalDate { get; set; }       
        public string Reason { get; set; }


    }
}
