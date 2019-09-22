using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.ViewModels.Employee;

namespace HRM.ViewModels.Relation
{
    public class RelationViewModel
    {
        public long Id { get; set; }
        public long EmployeeId { get; set; }
        public long RelationWithEmployeeId { get; set; }
        public EmployeeViewModel Employee { get; set; }
        public EmployeeViewModel RelationWithEmployee { get; set; }
        public int RelationType { get; set; }
        public string Xungbanthan { get; set; }
        public string Xungnguoidoidien { get; set; }

    }
}
