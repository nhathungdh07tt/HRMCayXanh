using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain.Entity;
using HRM.ViewModels.Work;

namespace HRM.ViewModels.Salary
{
    public class SalaryLevelViewModel
    {
        public SalaryLevelViewModel()
        {
           
        }

        public long Id { get; set; }
        public long WorkTitleId { get; set; }
        public string Code { get; set; }
        public string Node { get; set; }
        public double PayRate { get; set; }
       
        public decimal MonthlySalary { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public virtual WorkTitleViewModel WorkTitle { get; set; }

    }
}
