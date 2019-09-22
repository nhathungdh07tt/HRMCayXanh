using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain.Base;

namespace HRM.Domain.Entity
{
    public class SalaryLevel : AuditableEntity<long>
    {
        public SalaryLevel()
        {
            
        }

        [ForeignKey(nameof(WorkTitle))]
        public long WorkTitleId { get; set; }
        public virtual WorkTitle WorkTitle { get; set; }
        public string Code { get; set; }
        public string Node { get; set; }
        public double PayRate { get; set; }
        public decimal MonthlySalary { get; set; }
        public DateTime? EffectiveDate { get; set; }

    }
}
