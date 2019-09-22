using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain.Entity;

namespace HRM.ViewModels.Work
{
    public class WorkContractViewModel
    {
        public WorkContractViewModel()
        {
           
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

    }
}
