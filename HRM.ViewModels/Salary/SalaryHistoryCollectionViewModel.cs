using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.ViewModels.Department;
using HRM.ViewModels.Employee;
using HRM.ViewModels.Work;

namespace HRM.ViewModels.Salary
{
    public class SalaryHistoryCollectionViewModel
    {
        public SalaryHistoryCollectionViewModel()
        {
            Employees = new List<EmployeeViewModel>();
            SalaryHistorys = new List<SalaryHistoryViewModel>();
            Departments =new List<DepartmentViewModel>();
            WorkTitleDetails = new List<WorkTitleDetailViewModel>();
        }
        public List<EmployeeViewModel> Employees { get; set; }
        public List<SalaryHistoryViewModel> SalaryHistorys { get; set; }
        public List<DepartmentViewModel> Departments { get; set; }
        public List<WorkTitleDetailViewModel> WorkTitleDetails { get; set; }

        
    }
}
