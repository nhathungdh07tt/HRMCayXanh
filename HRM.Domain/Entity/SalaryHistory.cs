using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain.Base;

namespace HRM.Domain.Entity
{
    public class SalaryHistory : AuditableEntity<long>
    {
        public SalaryHistory()
        {
            
        }

        [ForeignKey(nameof(Employee))]
        public long EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }


        [ForeignKey(nameof(WorkTitleDetail))]
        public long WorkTitleDetailId { get; set; }
        public virtual WorkTitleDetail WorkTitleDetail { get; set; }

        [ForeignKey(nameof(SalaryLevel))]
        public long SalaryLevelId { get; set; }
        public virtual SalaryLevel SalaryLevel {get; set; }

        [ForeignKey(nameof(Department))]
        public long DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        public DateTime? From { get; set; }
        public DateTime? To { get; set; }


        [ForeignKey(nameof(CurrentContractType))]
        public long CurrentContractTypeId { get; set; }
        public virtual ContractType CurrentContractType { get; set; }

        public decimal MonthlyBonus { get; set; }
        public string Document { get; set; }      
    }
}
