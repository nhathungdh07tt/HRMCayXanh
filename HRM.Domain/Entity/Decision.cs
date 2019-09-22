using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain.Base;

namespace HRM.Domain.Entity
{
    public class Decision : AuditableEntity<long>
    {
        public Decision()
        {
            
        }

        [ForeignKey(nameof(Type))]
        public long TypeId { get; set; }
        public virtual DecisionType Type { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }

        [ForeignKey(nameof(Employee))]
        public long EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        public DateTime? Date { get; set; }
        public string Content { get; set; }
        public string Note { get; set; }
        public string Document { get; set; }
    }
}
