using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using HRM.Domain.Entity;
using HRM.ViewModels.Department;
using HRM.ViewModels.Employee;
using HRM.ViewModels.Work;

namespace HRM.ViewModels.Salary
{
    public class SalaryHistoryViewModel
    {
        public SalaryHistoryViewModel()
        {
           
        }

        public long Id { get; set; }       
        public decimal MonthlyBonus { get; set; }
        public string Document { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Name { get; set; }
        public long EmployeeId { get; set; }
        public virtual EmployeeViewModel Employee { get; set; }
        public long WorkTitleDetailId { get; set; }
        public virtual WorkTitleDetailViewModel WorkTitleDetail { get; set; }
        public long SalaryLevelId { get; set; }
        public virtual SalaryLevelViewModel SalaryLevel { get; set; }
        public long DepartmentId { get; set; }
        public virtual DepartmentViewModel Department { get; set; }

        public long CurrentContractTypeId { get; set; }
        public virtual ContractType CurrentContractType { get; set; }

        public HttpPostedFileBase DocumentFile { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }



    }
}
