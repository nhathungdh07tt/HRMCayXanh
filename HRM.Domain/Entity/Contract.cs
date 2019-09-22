using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain.Base;

namespace HRM.Domain.Entity
{
    public class Contract : AuditableEntity<long>
    {
        public string ContractNo { get; set; }

        [ForeignKey(nameof(Employee))]
        public long EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

        public DateTime? From { get; set; }
        public DateTime? To { get; set; }


        [ForeignKey(nameof(CurrentContractType))]
        public long CurrentContractTypeId { get; set; }
        public virtual ContractType CurrentContractType { get; set; }


        //[ForeignKey(nameof(DisplayContractType))]
        //public long DisplayContractTypeId { get; set; }
        //public virtual ContractType DisplayContractType { get; set; }
        
        public string Document { get; set; }
    }
}
