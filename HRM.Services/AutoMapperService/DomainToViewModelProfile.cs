using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HRM.Domain.Entity;
using HRM.ViewModels.Company;
using HRM.ViewModels.Contract;
using HRM.ViewModels.System;
using HRM.ViewModels.Employee;
using HRM.ViewModels.Test;
using HRM.ViewModels.Document;
using HRM.ViewModels.Department;
using HRM.ViewModels.Achievement;
using HRM.ViewModels.Decision;
using HRM.ViewModels.Salary;
using HRM.ViewModels.Work;
using HRM.ViewModels.Relation;
using HRM.ViewModels.Report;
using HRM.Domain.ReportModel;
using HRM.ViewModels.Timekeeping;

namespace HRM.Services.AutoMapperService
{
    public class DomainToViewModelProfile : Profile
    {
        public DomainToViewModelProfile()
        {
            
            CreateMap<User, UserViewModel>();
            CreateMap<Role, RoleViewModel>()
                .ForMember(m => m.Name, opt => opt.MapFrom(f => f.Name));
            CreateMap<Company, CompanyViewModel>()
                .ReverseMap();
            CreateMap<CompanyContact, CompanyContactViewModel>().ReverseMap();
            CreateMap<CompanyChangeHistory, CompanyChangeHistoryViewModel>().ReverseMap();
            CreateMap<Company, CompanyChangeHistory>()
                .ForMember(x => x.ChangedCount, opt => opt.MapFrom(f => f.ChangedCount + 1))
                .ForMember(x => x.Company, opt => opt.MapFrom(f => f));
            CreateMap<Employee, EmployeeViewModel>()
                .ForMember(x => x.SelectedSkills, opt => opt.Ignore())
                .ForMember(x => x.FullName, opt => opt.Ignore())
                .ReverseMap(); // EmployeeViewModel -> Employee
                //.ForMember(x => x.Skills, opt => opt.Ignore());
                //.ForMember(x => x.Skills, opt => opt.ResolveUsing(f =>
                //{
                //   return f.SelectedSkills.Select(y => new EmployeeSkill {
                //       // TODO

                //    });
                //}));
            CreateMap<Country, CountryViewModel>().ReverseMap();
            CreateMap<EthnicGroup, EthnicGroupViewModel>().ReverseMap();
            CreateMap<Religion, ReligionViewModel>().ReverseMap();
            CreateMap<Education, EducationViewModel>().ReverseMap();
            CreateMap<Skill, SkillViewModel>().ReverseMap();
            CreateMap<Test, TestViewModel>().ReverseMap();
            CreateMap<ContractType, ContractTypeViewModel>().ReverseMap();
            CreateMap<Relation, RelationViewModel>().ReverseMap();
            CreateMap<DocumentType, DocumentTypeViewModel>().ReverseMap();
            CreateMap<Department, DepartmentViewModel >().ReverseMap();
            CreateMap<Achievement, AchievementViewModel>().ReverseMap();
            CreateMap<Contract, ContractViewModel>().ReverseMap();
            CreateMap<TimeAttendanceType, TimeAttendanceTypeViewModel>().ReverseMap();
            CreateMap<Timekeeping, TimekeepingViewModel>().ReverseMap();

            CreateMap<Decision, DecisionViewModel>().ReverseMap();
            CreateMap<DecisionType, DecisionTypeViewModel>().ReverseMap();
            CreateMap<DepartmentEmployee, DepartmentEmployeeViewModel>().ReverseMap();
            CreateMap<Document, DocumentViewModel>().ReverseMap();
            CreateMap<SalaryHistory, SalaryHistoryViewModel>().ReverseMap();
            CreateMap<SalaryLevel, SalaryLevelViewModel>().ReverseMap();
            CreateMap<WorkContract, WorkContractViewModel>().ReverseMap();
            CreateMap<WorkTitle, WorkTitleViewModel>().ReverseMap();
            CreateMap<WorkTitleDetail, WorkTitleDetailViewModel>().ReverseMap();
            CreateMap<DateOffHistory, DateOffHistoryViewModel>().ReverseMap();
            CreateMap<EmployeeSkill, EmployeeSkillViewModel>().ReverseMap();
            CreateMap<Employee, EmployeeSelectItemViewModel>().ForMember(x => x.FullName, opt => opt.Ignore()).ReverseMap()
                .ForMember(x => x.FullName, opt => opt.Ignore());
            CreateMap<Report01Model, Report01ViewModel>().ReverseMap();
        }
    }
}
