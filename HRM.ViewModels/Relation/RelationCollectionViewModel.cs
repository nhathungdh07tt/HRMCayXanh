using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.ViewModels.Employee;

namespace HRM.ViewModels.Relation
{
    public class RelationCollectionViewModel
    {
        public RelationCollectionViewModel()
        {
            Employees = new List<EmployeeViewModel>();
            Relations = new List<RelationViewModel>();
        }
        public List<EmployeeViewModel> Employees { get; set; }
        public List<RelationViewModel> Relations { get; set; }
    }
}
