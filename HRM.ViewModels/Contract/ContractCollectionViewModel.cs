using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.ViewModels.Employee;

namespace HRM.ViewModels.Contract
{
    public class ContractCollectionViewModel
    {
        public ContractCollectionViewModel()
        {
            Employees = new List<EmployeeViewModel>();
            Contracts = new List<ContractViewModel>();
        }
        public List<EmployeeViewModel> Employees { get; set; }
        public List<ContractViewModel> Contracts{ get; set; }
    }
}
