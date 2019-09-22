using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.ViewModels.Employee;

namespace HRM.ViewModels.Decision
{
    public class DecisionCollectionViewModel
    {
        public DecisionCollectionViewModel()
        {
            Employees = new List<EmployeeViewModel>();
            Decisions = new List<DecisionViewModel>();
        }
        public List<EmployeeViewModel> Employees { get; set; }
        public List<DecisionViewModel> Decisions { get; set; }
    }
}
