using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain.Base;

namespace HRM.Domain.Entity
{
    public class EthnicGroup : Entity<long>
    {
        public string Code { get;set; }
        public string Name { get; set; }
    }
}
