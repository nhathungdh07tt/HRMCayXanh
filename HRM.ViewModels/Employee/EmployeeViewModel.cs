using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.ViewModels.Authenticate;

namespace HRM.ViewModels.Employee
{
    public class EmployeeViewModel
    {
        public long Id { get; set; }
        public virtual RoleViewModel Role { get; set; }
        public string FirstName { get; set; }
        public string Roles { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{this.FirstName} {this.LastName}";

        public CountryViewModel Nationality { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public int Gender { get; set; }

        public virtual EthnicGroupViewModel EthnicGroup { get; set; }
        public virtual ReligionViewModel Religion { get; set; }

        public string Address { get; set; }
        public string Phone { get; set; }
        public string IdentityNo { get; set; }
        public DateTime? DateIssueIdentity { get; set; }
        public string PlaceIssueIdentity { get; set; }
        public virtual EducationViewModel Education { get; set; }
        public string DetailEducation { get; set; }
        public long[] SelectedSkills { get; set; }
        public virtual ICollection<EmployeeSkillViewModel> Skills { get; set; }
        public string DetailSkill { get; set; }
        public int CommunistYouthUnion { get; set; }
        public string Email { get; set; }
        public string SocialInsuranceNo { get; set; }
        public DateTime? DateIssueSocialInsurance { get; set; }
        public double YearDayOff { get; set; }
        public DateTime? DateSignContract { get; set; }
        public DateTime? DateOffContract { get; set; }
        public string Certificate { get; set; }
        public string BankAccount { get; set; }
        public string Bank { get; set; }
        /// <summary>
        /// Working State
        /// </summary>
        public bool Working { get; set; }
        public string Note { get; set; }
        public string Avatar { get; set; }
        public string Notification { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsDeactivated { get; set; }
        public int EmployeeRelationType { get; set; }

    }
}
