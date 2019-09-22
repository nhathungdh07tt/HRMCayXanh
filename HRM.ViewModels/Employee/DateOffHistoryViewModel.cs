using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain.Entity;

namespace HRM.ViewModels.Employee
{
    public class DateOffHistoryViewModel
    {
        public DateOffHistoryViewModel()
        {
           
        }

        public long Id { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string FromSession { get; set; }
        public string ToSession { get; set; }
        public string HasPermission { get; set; }
        public double TotalDate { get; set; }
      
        public string Reason { get; set; }
        public long EmployeeId { get; set; }
        public virtual EmployeeViewModel Employee { get; set; }

    }
}
