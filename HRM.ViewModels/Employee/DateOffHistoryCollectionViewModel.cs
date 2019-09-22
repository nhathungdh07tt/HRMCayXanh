using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.ViewModels.Employee;

namespace HRM.ViewModels.Employee
{
    public class DateOffHistoryCollectionViewModel
    {
        public DateOffHistoryCollectionViewModel()
        {
            Employees = new List<EmployeeViewModel>();
            DateOffHistorys = new List<DateOffHistoryViewModel>();
        }
        public List<EmployeeViewModel> Employees { get; set; }
        public List<DateOffHistoryViewModel> DateOffHistorys { get; set; }
    }
}
