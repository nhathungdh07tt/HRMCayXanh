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
using HRM.ViewModels.Department;
using HRM.ViewModels.Salary;
using HRM.ViewModels.Work;

namespace HRM.Services
{
    public interface ISalaryHistoryService : IService<SalaryHistory>
    {
        List<SalaryHistoryViewModel> GetSalaryHistorys();
        List<SalaryHistoryViewModel> GetSalaryHistorys1();
        SalaryHistoryViewModel GetInfo(long id);
        void Insert(SalaryHistoryViewModel model);
        void Update(SalaryHistoryViewModel model);
        void Delete(SalaryHistoryViewModel model);
        List<SalaryHistoryViewModel> GetSalaryHistoryByEmployee(long employeeId);
        List<DepartmentViewModel> GetSalaryHistoryByDepartment(long departmentId);
        List<WorkTitleDetailViewModel> GetSalaryHistoryByWorkTitleDetail(long WorkTitleDetailId);
    }

    public class SalaryHistoryService : BaseService<SalaryHistory>, ISalaryHistoryService
    {
        #region Properties

        private readonly IContext _context;
        private readonly ISalaryHistoryRepository _repository;

        #endregion Properties

        #region Constructors

        public SalaryHistoryService(IContext context, ISalaryHistoryRepository repository)
            : base(context, repository)
        {
            _repository = repository;
            _context = context;
        }

        public void Delete(SalaryHistoryViewModel model)
        {
            var item = _repository.FindById(model.Id);
            if (item != null)
                _repository.Delete(item);
        }

        public List<SalaryHistoryViewModel> GetSalaryHistoryByEmployee(long employeeId)
        {
            return _repository.GetAllByCondition(x => x.Employee.Id == employeeId, x => x.Employee, x => x.SalaryLevel)
                .ProjectTo<SalaryHistoryViewModel>().ToList();
        }

        public List<DepartmentViewModel> GetSalaryHistoryByDepartment(long departmentId)
        {
            return _repository.GetAllByCondition(x => x.Department.Id == departmentId, x => x.Department, x => x.Department)
                .ProjectTo<DepartmentViewModel>().ToList();
        }

        public List<WorkTitleDetailViewModel> GetSalaryHistoryByWorkTitleDetail(long WorkTitleDetailId)
        {
            return _repository.GetAllByCondition(x => x.WorkTitleDetail.Id == WorkTitleDetailId, x => x.WorkTitleDetail, x => x.WorkTitleDetail)
                .ProjectTo<WorkTitleDetailViewModel>().ToList();
        }


        public List<SalaryHistoryViewModel> GetSalaryHistorys()
        {
            return _repository.GetAll().ProjectTo<SalaryHistoryViewModel>().ToList();
        }

        public List<SalaryHistoryViewModel> GetSalaryHistorys1()
        {
            return _repository.GetAll().GroupBy(x => new { x.EmployeeId}).Select(g => g.OrderByDescending(r => r.From).FirstOrDefault()).Select(i => new SalaryHistoryViewModel { SalaryLevelId=i.SalaryLevelId,Id=i.Id,DepartmentId=i.DepartmentId,CurrentContractTypeId=i.CurrentContractTypeId ,EmployeeId=i.EmployeeId, FirstName = i.Employee.FirstName, LastName = i.Employee.LastName, Name=i.Department.Name, From=i.From,To=i.To}).ToList();
            //var model = _repository.GetAll().GroupBy(x => new { x.EmployeeId, x.From }).Select(g => g.OrderByDescending(r => r.From).First()).Select(i => new {i.Id, i.EmployeeId,i.DepartmentId,i.CurrentContractTypeId,i.CreatedBy,i.CreatedDate,i.SalaryLevelId,i.WorkTitleDetailId,i.UpdatedBy,i.UpdatedDate,i.To,i.From }).ToList();
            //return _repository.GetAll().GroupBy(x => new { x.EmployeeId,x.From }).Where(x=>x.Max(From)).ProjectTo<SalaryHistoryViewModel>().ToList();
            //return model;
        }

        public SalaryHistoryViewModel GetInfo(long id)
        {
            var contractType = _repository.FindById(id);
            if (contractType != null)
                return AutoMapper.Mapper.Map<SalaryHistory, SalaryHistoryViewModel>(contractType);

            return new SalaryHistoryViewModel();
        }

        public void Insert(SalaryHistoryViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<SalaryHistoryViewModel, SalaryHistory>(model);
            _repository.Add(contractType);
        }

        public void Update(SalaryHistoryViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<SalaryHistoryViewModel, SalaryHistory>(model);

            if (string.IsNullOrEmpty(contractType.Document))
            {
                var old = _repository.FindById(model.Id);
                if (old != null)
                    contractType.Document = old.Document;
            }

            _repository.Update(contractType);
        }

        #endregion Constructors


    }
}