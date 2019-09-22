using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain.Base;

namespace HRM.Domain.Entity
{
    public class WorkContract : AuditableEntity<long>
    {
        public WorkContract()
        {
            
        }

        [ForeignKey(nameof(Type))]
        public long TypeId { get; set; }
        public virtual ContractType Type { get; set; }

    }
}
