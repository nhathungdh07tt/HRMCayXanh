using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using HRM.Domain.Entity;
using HRM.ViewModels.Employee;

namespace HRM.ViewModels.Contract
{
    public class ContractViewModel
    {
        public ContractViewModel()
        {
          
        }

        public long Id { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public virtual ContractType CurrentContractType { get; set; }
        public long CurrentContractTypeId { get; set; }
        //public virtual ContractType DisplayContractType { get; set; }
        //public long DisplayContractTypeId { get; set; }
        public long EmployeeId { get; set; }
        public virtual EmployeeViewModel Employee { get; set; }

        public string ContractNo { get; set; }
        public string Document { get; set; }
        public HttpPostedFileBase DocumentFile { get; set; }

    }
}
