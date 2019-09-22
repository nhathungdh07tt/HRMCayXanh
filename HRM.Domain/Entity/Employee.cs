using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain.Base;

namespace HRM.Domain.Entity
{
    public class Employee : Entity<long> {
        public Employee()
        {
            AssignToDepartments = new HashSet<DepartmentEmployee>();
            Contracts = new HashSet<Contract>();
            Relations = new HashSet<Relation>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [NotMapped]
        public string FullName => $"{this.FirstName} {this.LastName}";

        [ForeignKey(nameof(Nationality))]
        public long NationalityId { get; set; }
        public virtual Country Nationality { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public int Gender { get; set; }

        [ForeignKey(nameof(EthnicGroup))]
        public long EthnicGroupId { get;set; }
        public virtual EthnicGroup EthnicGroup { get; set; }

        [ForeignKey(nameof(Religion))]
        public long ReligionId { get; set; }
        public virtual Religion Religion { get; set; }

        public string Address { get; set; }
        public string Phone { get; set; }
        public string IdentityNo { get; set; }
        public DateTime? DateIssueIdentity { get; set; }
        public string PlaceIssueIdentity { get; set; }

        [ForeignKey(nameof(Education))]
        public long EducationId { get; set; }
        public virtual Education Education { get; set; }
        public string DetailEducation { get; set; }
        public virtual ICollection<EmployeeSkill> Skills { get;set; }
        public string DetailSkill { get; set; }
        public int CommunistYouthUnion { get; set; }
        public string Email { get; set; }
        public string SocialInsuranceNo { get; set; }
        public DateTime? DateIssueSocialInsurance { get; set; }
        public double YearDayOff { get; set; }
        public DateTime? DateSignContract { get;set; }
        public DateTime? DateOffContract { get; set; }
        public string Certificate { get; set; }
        public string BankAccount { get; set; }
        public string Bank { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsDeactivated { get; set; }
        /// <summary>
        /// Working State
        /// </summary>
        public bool Working { get; set; }
        public string Note { get; set; }
        public string Avatar { get; set; }
        public string Notification { get; set; }

        [ForeignKey(nameof(User))]
        public long UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Contract> Contracts { get; set; }
        public virtual ICollection<DepartmentEmployee> AssignToDepartments { get; set; }
        public virtual ICollection<Relation> Relations {get; set; }
        public virtual ICollection<SalaryHistory> SalaryHistories { get; set; }
        // RelationType [Internal, External]
        public int EmployeeRelationType { get; set; }

        
    }

}
