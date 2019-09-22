using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain.Base;

namespace HRM.Domain.Entity
{
    public class CompanyContact : Entity<long>
    {
        public CompanyContact()
        {
            
        }

        /// <summary>
        /// Name prefix ex (Mr, Mrs, Miss ...)
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// Contract Name ex (John Smith)
        /// </summary>
        public string ContactName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        /// <summary>
        /// Company Title ex (General, Manager ..)
        /// </summary>
        public string Title {get; set; }

        [ForeignKey(nameof(Company))]
        public long CompanyId { get; set; }

        public virtual Company Company { get; set; }
    }
}
