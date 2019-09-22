using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain.Base;

namespace HRM.Domain.Entity
{
    public class EmployeeSkill : Entity<long>
    {
        [ForeignKey(nameof(Skill))]
        public long SkillId { get; set; }
        public virtual Skill Skill { get; set; }

        [ForeignKey(nameof(Employee))]
        public long EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
