using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain.Base;

namespace HRM.Domain.Entity
{
    public class Relation : Entity<long> {
        public Relation()
        {

        }

        public long EmployeeId { get; set; }
        public long RelationWithEmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
        [ForeignKey("RelationWithEmployeeId")]
        public Employee RelationWithEmployee { get; set; }
        public int? RelationType { get; set; }

        public string Xungbanthan { get; set; }
        public string Xungnguoidoidien { get; set; }




    }




}
