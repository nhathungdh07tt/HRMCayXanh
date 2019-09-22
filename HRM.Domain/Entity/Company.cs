using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain.Base;

namespace HRM.Domain.Entity
{
    public class Company : Entity<long>
    {
        public Company()
        {
            Contacts = new HashSet<CompanyContact>();
            ChangeHistories = new HashSet<CompanyChangeHistory>();
            ChangedCount = 0;
            CharterCapital = 0;
        }

        public string VietNamName { get; set; }
        public string EnglishName { get; set; }
        public string ShortName { get; set; }
        public string TaxCode { get; set; }
        public string Address { get; set; }
        public DateTime? FirstRegistrationDate { get; set; }
        public decimal CharterCapital { get; set; }
        public int ChangedCount { get; set; }
        public DateTime? ChangedDate { get; set; }
        public string ChangedContent { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
        public string Website { get;set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankAccountIssuePlace { get; set; }
        public string SignName { get; set; }
        public string SignTitle { get; set; }

        public virtual ICollection<CompanyContact> Contacts { get; set; }
        public virtual ICollection<CompanyChangeHistory> ChangeHistories { get; set; }

    }
}
