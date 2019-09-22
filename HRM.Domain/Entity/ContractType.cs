﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain.Base;

namespace HRM.Domain.Entity
{
    public class ContractType : Entity<long>
    {
        public ContractType()
        {
                
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual  ICollection<Contract> Contracts { get; set; }
    }
}
