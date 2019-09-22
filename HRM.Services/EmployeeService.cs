using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper.QueryableExtensions;
using HRM.Domain.Entity;
using HRM.Repository;
using HRM.Repository.Repositories;
using HRM.Services.Base;
using HRM.Services.Base.Interfaces;
using HRM.ViewModels.Employee;
using HRM.ViewModels.Salary;
using LinqKit;

namespace HRM.Services
{
    public interface IEmployeeService : IService<Employee>
    {
        List<EmployeeViewModel> GetAll();
        List<EmployeeViewModel> GetAll1();
        List<EmployeeViewModel> SearchByOld(int department, int? from, int? to, string sugget = "");
        //List<EmployeeViewModel> SearchByContractTypes(int department, long? contracttype);
        List<EmployeeViewModel> SearchByDayOfBirth(int? month1);
        List<EmployeeViewModel> SearchByUserName(string username);
        List<EmployeeViewModel> SearchByNguoiThan(int? nguoithan);
        List<EmployeeViewModel> SearchBySkill(int department, int? skill, string sugget = "");
        List<EmployeeViewModel> SearchByEducation(int department, int? education, string sugget = "");
        List<EmployeeViewModel> SearchByGender(int department, int? gender, string sugget = "");
        List<EmployeeViewModel> SearchByDateOfBirth(int department, DateTime? fromdate, DateTime? todate, string sugget = "");
        EmployeeViewModel GetInfo(long? id);
        Employee ConvertToModel(EmployeeViewModel employeeViewModel);
        EmployeeViewModel ConvertToData(Employee model);
        void Insert(EmployeeViewModel employeeViewModel);
        EmployeeViewModel Update(EmployeeViewModel employeeViewModel);
        new bool Delete(Employee employee);
        List<EmployeeSelectItemViewModel> GetEmployeeSelectListItems();
        List<EmployeeSelectItemViewModel> GetEmployeeSelectListItems1();

    }

    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        #region Properties

        private readonly IContext _context;
        private readonly IEmployeeRepository _repository;
        private readonly ICountryRepository _countryRepository;
        private readonly IEthnicGroupRepository _ethnicGroupRepository;
        private readonly IReligionRepository _religionRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly ISalaryHistoryRepository _salaryHistoryrepository;
        private readonly IEmployeeSkillRepository _employeeSkillRepository;

        #endregion Properties

        #region Constructors

        public EmployeeService(IContext context,
            IEmployeeRepository repository,
            ICountryRepository countryRepository,
            IEthnicGroupRepository ethnicGroupRepository,
            IReligionRepository religionRepository,
            IEducationRepository educationRepository,
            ISkillRepository skillRepository,
            ISalaryHistoryRepository salaryHistoryrepository,
            IEmployeeSkillRepository employeeSkillRepository
            )
            : base(context, repository)
        {
            _repository = repository;
            this._countryRepository = countryRepository;
            this._ethnicGroupRepository = ethnicGroupRepository;
            this._religionRepository = religionRepository;
            this._educationRepository = educationRepository;
            this._skillRepository = skillRepository;
            this._employeeSkillRepository = employeeSkillRepository;
            _context = context;
        }

        public List<EmployeeViewModel> GetAll()
        {
            return _repository.GetAllByCondition(x => !x.IsDeleted && x.EmployeeRelationType == 0).ProjectTo<EmployeeViewModel>()
                .ToList()
                .Select(x =>
                {
                    x.SelectedSkills = x.Skills.Select(y => y.Skill.Id).ToArray();
                    return x;
                }).ToList();
        }

        public List<EmployeeViewModel> GetAll1()
        {
            return _repository.GetAllByCondition(x => x.EmployeeRelationType == 1).ProjectTo<EmployeeViewModel>()
                .ToList()
                .Select(x =>
                {
                    x.SelectedSkills = x.Skills.Select(y => y.Skill.Id).ToArray();
                    return x;
                }).ToList();
        }

        public EmployeeViewModel GetInfo(long? id)
        {
            var model = _repository.FindById(id: id.Value);
            var employee = AutoMapper.Mapper.Map<Employee, EmployeeViewModel>(model);
            employee.SelectedSkills = employee.Skills.Select(y => y.Skill.Id).ToArray();
            return employee;
        }

        public Employee ConvertToModel(EmployeeViewModel employeeViewModel)
        {
            return AutoMapper.Mapper.Map<EmployeeViewModel, Employee>(employeeViewModel);
        }

        public EmployeeViewModel ConvertToData(Employee model)
        {
            return AutoMapper.Mapper.Map<Employee, EmployeeViewModel>(model);
        }

        public void Insert(EmployeeViewModel employeeViewModel)
        {
            var model = AutoMapper.Mapper.Map<EmployeeViewModel, Employee>(employeeViewModel);
            if (model.Nationality != null)
                model.Nationality = _countryRepository.FindById(model.Nationality.Id);
            if (model.EthnicGroup != null)
                model.EthnicGroup = _ethnicGroupRepository.FindById(model.EthnicGroup.Id);
            if (model.Religion != null)
                model.Religion = _religionRepository.FindById(model.Religion.Id);
            if (model.Education != null)
                model.Education = _educationRepository.FindById(model.Education.Id);

            model.IsDeleted = false;
            model.IsDeactivated = false;
            //model.EmployeeRelationType = 1;
            // Temporal
            model.UserId = 1;

            var skillIds = employeeViewModel.SelectedSkills != null ?
                            employeeViewModel.SelectedSkills.ToList() :
                            new List<long>();

            var skills = _skillRepository.GetAllByCondition(x => skillIds.Contains(x.Id));

            using (var scope = new TransactionScope())
            {
                try
                {
                    var insertItem = _repository.Add(model);

                    foreach (var skill in skills)
                    {
                        _employeeSkillRepository.Add(new EmployeeSkill() {
                            Skill = skill,
                            Employee = insertItem
                        });
                    }

                    Save();

                    scope.Complete();
                }
                catch (TransactionException ex)
                {
                    scope.Dispose();
                }
            }
        }

        public EmployeeViewModel Update(EmployeeViewModel employeeViewModel)
        {
            var model = AutoMapper.Mapper.Map<EmployeeViewModel, Employee>(employeeViewModel);

            var employee = _repository
                .FindById(model.Id);

            if (employee == null) return null;


            if (model.Nationality != null)
                model.Nationality = _countryRepository.FindById(model.Nationality.Id);
            if (model.EthnicGroup != null)
                model.EthnicGroup = _ethnicGroupRepository.FindById(model.EthnicGroup.Id);
            if (model.Religion != null)
                model.Religion = _religionRepository.FindById(model.Religion.Id);
            if (model.Education != null)
                model.Education = _educationRepository.FindById(model.Education.Id);

            // Temporal
            model.UserId = 1;

            var skills = new List<Skill>();

            if (employeeViewModel.SelectedSkills != null && employeeViewModel.SelectedSkills.Any())
            {
                var skillIds = employeeViewModel.SelectedSkills != null ?
                            employeeViewModel.SelectedSkills.ToList() :
                            new List<long>();
                // new skills
                skills =
                    _skillRepository
                        .GetAllByCondition(x => skillIds.Contains(x.Id)).ToList();

                // delete old skills
                var oldSkills = _employeeSkillRepository.GetAllByCondition(x => x.Employee.Id == model.Id).ToList();
                foreach (var oldSkill in oldSkills)
                {
                    _employeeSkillRepository.Delete(oldSkill);
                }

            }

            using (var scope = new TransactionScope())
            {
                try
                {
                    _repository.Update(model);
                    Save();
                    foreach (var skill in skills)
                    {
                        _employeeSkillRepository.Add(new EmployeeSkill() {
                            Skill = skill,
                            Employee = employee
                        });
                    }

                    Save();

                    scope.Complete();
                }
                catch (TransactionException ex)
                {
                    scope.Dispose();
                }
            }

            return employeeViewModel;

        }

        public new bool Delete(Employee employee)
        {
            employee.IsDeleted = true;
            try
            {
                _repository.Update(employee);
                Save();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<EmployeeSelectItemViewModel> GetEmployeeSelectListItems()
        {
            return _repository.GetAllByCondition(x => !x.IsDeleted && x.EmployeeRelationType == 0).ProjectTo<EmployeeSelectItemViewModel>().ToList();
        }

        public List<EmployeeSelectItemViewModel> GetEmployeeSelectListItems1()
        {
            return _repository.GetAllByCondition(x => x.EmployeeRelationType == 1).ProjectTo<EmployeeSelectItemViewModel>().ToList();
        }

        public List<EmployeeViewModel> SearchByOld(int department, int? from, int? to, string sugget = "")
        {
            var query = _repository.GetAll();

            var currentYear = DateTime.Now.Year;

            if (from > 0)
                query = query.Where(x => currentYear - x.DateOfBirth.Value.Year >= from);

            if (to > 0)
                query = query.Where(x => (currentYear - x.DateOfBirth.Value.Year) <= to);

            if (!string.IsNullOrEmpty(sugget))
            {
                query = query.Where(x => x.LastName.StartsWith(sugget));
            }

            if (department > 0)
            {
                query = query.Where(x => x.SalaryHistories.Any(c => c.DepartmentId == department));
            }

            return query.ProjectTo<EmployeeViewModel>()
                 .ToList()
                 .Select(x =>
                 {
                     x.SelectedSkills = x.Skills.Select(y => y.Skill.Id).ToArray();
                     return x;
                 }).ToList();
        }


        public List<EmployeeViewModel> SearchByDayOfBirth(int? month1)
        {
            var query = _repository.GetAll();

            var currentYear = DateTime.Now.Month;

            if (month1 > 0)
                query = query.Where(x => x.DateOfBirth.Value.Month == month1);

            return query.ProjectTo<EmployeeViewModel>()
                 .ToList();

        }


        public List<EmployeeViewModel> SearchByDateOfBirth(int department, DateTime? fromdate, DateTime? todate, string sugget = "")
        {
            var query = _repository.GetAll();

            var currentYear = DateTime.Now.Year;

            if (fromdate.HasValue)
                query = query.Where(x => x.DateOfBirth >= fromdate);

            if (todate.HasValue)
                query = query.Where(x => x.DateOfBirth <= todate);

            if (!string.IsNullOrEmpty(sugget))
            {
                query = query.Where(x => x.LastName.StartsWith(sugget));
            }

            if (department > 0)
            {
                query = query.Where(x => x.SalaryHistories.Any(c => c.DepartmentId == department));
            }

            return query.ProjectTo<EmployeeViewModel>()
                 .ToList()
                 .Select(x =>
                 {
                     x.SelectedSkills = x.Skills.Select(y => y.Skill.Id).ToArray();
                     return x;
                 }).ToList();
        }

        public List<EmployeeViewModel> SearchByGender(int department, int? gender, string sugget = "")
        {
            var query = _repository.GetAll();

            var currentYear = DateTime.Now.Year;

            if (gender.HasValue)
                query = query.Where(x => x.Gender == gender);

            if (!string.IsNullOrEmpty(sugget))
            {
                query = query.Where(x => x.LastName.StartsWith(sugget));
            }

            if (department > 0)
            {
                query = query.Where(x => x.SalaryHistories.Any(c => c.DepartmentId == department));
            }

            return query.ProjectTo<EmployeeViewModel>()
                 .ToList()
                 .Select(x =>
                 {
                     x.SelectedSkills = x.Skills.Select(y => y.Skill.Id).ToArray();
                     return x;
                 }).ToList();
        }

        public List<EmployeeViewModel> SearchByEducation(int department, int? education, string sugget = "")
        {
            var query = _repository.GetAll();

            var currentYear = DateTime.Now.Year;

            if (education.HasValue)
                query = query.Where(x => x.EducationId == education);

            if (!string.IsNullOrEmpty(sugget))
            {
                query = query.Where(x => x.LastName.StartsWith(sugget));
            }

            if (department > 0)
            {
                query = query.Where(x => x.SalaryHistories.Any(c => c.DepartmentId == department));
            }

            return query.ProjectTo<EmployeeViewModel>()
                 .ToList()
                 .Select(x =>
                 {
                     x.SelectedSkills = x.Skills.Select(y => y.Skill.Id).ToArray();
                     return x;
                 }).ToList();
        }

        public List<EmployeeViewModel> SearchBySkill(int department, int? skill, string sugget = "")
        {
            int what = skill.HasValue ? skill.Value : -1;

            return _repository.GetAllByCondition(x => x.Skills.Any(y => y.Skill.Id == what))
                .ProjectTo<EmployeeViewModel>().ToList()
                .Select(x =>
                {
                    x.SelectedSkills = x.Skills.Select(y => y.Skill.Id).ToArray();
                    return x;
                }).ToList();


        }


        public List<EmployeeViewModel> SearchByNguoiThan(int? nguoithan)
        {
            var query = _repository.GetAll();

            var currentYear = DateTime.Now.Year;

            if (nguoithan.HasValue)
                query = query.Where(x => x.EmployeeRelationType == 1);

            return query.ProjectTo<EmployeeViewModel>()
                 .ToList()
                 .Select(x =>
                 {
                     x.SelectedSkills = x.Skills.Select(y => y.Skill.Id).ToArray();
                     return x;
                 }).ToList();
        }


        public List<EmployeeViewModel> SearchByUserName(string username)
        {
            var query = _repository.GetAll();


            query = query.Where(x => x.LastName == username);

            return query.ProjectTo<EmployeeViewModel>()
                 .ToList();
        }
        #endregion Constructors


        //public List<EmployeeViewModel> SearchByContractTypes(int department, long? contracttype)
        //{
        //    var query = _repository.GetAll();
        //    var query2 = _salaryHistoryrepository.
        //    var query1 = _salaryHistoryrepository.GetAll().GroupBy(x => new { x.EmployeeId }).Select(g => g.OrderByDescending(r => r.From).FirstOrDefault()).Select(i => new SalaryHistoryViewModel { Id = i.Id, DepartmentId = i.DepartmentId, EmployeeId = i.EmployeeId, From = i.From, To = i.To }).ToList();

        //    var model = (from a in query1
        //                 join p in query
        //                 on a.EmployeeId equals p.Id                        
        //                 select new EmployeeViewModel {
        //                     Id=p.Id,
        //                     LastName = p.LastName,
        //                     FirstName = p.FirstName,
        //                     //Nationality = p.Nationality.Name,
        //                     Gender = p.Gender,
        //                     IdentityNo = p.IdentityNo,
        //                     DateOfBirth = p.DateOfBirth,
        //                     PlaceOfBirth = p.PlaceOfBirth,
        //                     DateIssueIdentity = p.DateIssueIdentity,
        //                     PlaceIssueIdentity = p.PlaceIssueIdentity,
        //                     ////EthnicGroup = p.EthnicGroup.Name,
        //                     //Religion = p.Religion.Name,
        //                     Address = p.Address,
        //                     Phone = p.Phone,
        //                     Email = p.Email,
        //                     YearDayOff = p.YearDayOff,
        //                     //Education = p.Education.Title,
        //                     DetailEducation = p.DetailEducation,
        //                     Certificate = p.Certificate,
        //                     CommunistYouthUnion = p.CommunistYouthUnion,
        //                     SocialInsuranceNo = p.SocialInsuranceNo,
        //                     DateIssueSocialInsurance = p.DateIssueSocialInsurance,
        //                     BankAccount = p.BankAccount,
        //                     Bank = p.Bank,
        //                     DateSignContract = p.DateSignContract,
        //                     DateOffContract = p.DateOffContract
        //}).OrderBy(x=>x.Id);

        //    return query.ProjectTo<EmployeeViewModel>()
        //         .ToList()
        //         .Select(x =>
        //         {
        //             x.SelectedSkills = x.Skills.Select(y => y.Skill.Id).ToArray();
        //             return x;
        //         }).ToList();
        //}

    }
}