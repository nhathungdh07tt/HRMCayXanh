using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using HRM.Domain.Entity;
using HRM.Repository;
using HRM.Repository.Repositories;
using HRM.Services.Base;
using HRM.Services.Base.Interfaces;
using HRM.ViewModels.Employee;

namespace HRM.Services
{
    public interface IEmployeeSearchService : IService<Employee>
    {
        List<EmployeeViewModel> SearchByAge(int from, int to);
        List<EmployeeViewModel> SearchByBirthDay(DateTime from, DateTime to);
        List<EmployeeViewModel> SearchByGender(int gender);
        List<EmployeeViewModel> SearchByEducation(long educationId);
        List<EmployeeViewModel> SearchByContractType(long typeId);
        List<EmployeeViewModel> SearchBySkill(List<long> skills);

    }

    public class EmployeeSearchService : BaseService<Employee>, IEmployeeSearchService
    {
        #region Properties

        private readonly IContext _context;
        private readonly IEmployeeRepository _repository;
        private readonly ISkillRepository _skillRepository;
        private readonly IEmployeeSkillRepository _employeeSkillRepository;
        #endregion Properties

        #region Constructors

        public EmployeeSearchService(IContext context, 
            IEmployeeRepository repository, 
            ISkillRepository skillRepository,
            IEmployeeSkillRepository employeeSkillRepository
            )
            : base(context, repository)
        {
            _repository = repository;
            _skillRepository = skillRepository;
            _employeeSkillRepository = employeeSkillRepository;
            _context = context;
        }

        public List<EmployeeViewModel> SearchByAge(int from, int to)
        {
            var currentYear = DateTime.Now.Year;

            return _repository.GetAllByCondition(x =>
                    (currentYear - x.DateOfBirth.Value.Year >= from) && (currentYear - x.DateOfBirth.Value.Year <= to))
                .ProjectTo<EmployeeViewModel>().ToList();
        }

        public List<EmployeeViewModel> SearchByBirthDay(DateTime from, DateTime to)
        {
            return _repository.GetAllByCondition(x => x.DateOfBirth.Value >= from && x.DateOfBirth.Value <= to)
                .ProjectTo<EmployeeViewModel>().ToList();
        }

        public List<EmployeeViewModel> SearchBySkill(List<long> skills)
        {
            var skillList = _skillRepository.GetAllByCondition(x => skills.Contains(x.Id)).ToList();

            return _employeeSkillRepository.GetAllByCondition(x => skillList.Contains(x.Skill))
                .Select(x => x.Employee).ProjectTo<EmployeeViewModel>().ToList();
            //return _repository.GetAllByCondition(x => x.Skills.)
        }

        public List<EmployeeViewModel> SearchByContractType(long typeId)
        {
            return _repository.GetAllByCondition(x => x.Contracts.Any(y => y.CurrentContractType.Id == typeId))
                .ProjectTo<EmployeeViewModel>().ToList();
        }

        public List<EmployeeViewModel> SearchByEducation(long educationId)
        {
            return _repository.GetAllByCondition(x => x.Education.Id == educationId)
                .ProjectTo<EmployeeViewModel>().ToList();

        }

        public List<EmployeeViewModel> SearchByGender(int gender)
        {
            return _repository.GetAllByCondition(x => x.Gender == gender)
                .ProjectTo<EmployeeViewModel>().ToList();
        }

        #endregion Constructors


    }
}