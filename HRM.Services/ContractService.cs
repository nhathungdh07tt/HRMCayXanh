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
using HRM.ViewModels.Contract;

namespace HRM.Services
{
    public interface IContractService : IService<Contract>
    {
        List<ContractViewModel> GetContracts();
        ContractViewModel GetInfo(long id);
        void Insert(ContractViewModel model);
        void Update(ContractViewModel model);
        void Delete(ContractViewModel model);
        List<ContractViewModel> GetContractByEmployee(long employeeId);  
    }

    public class ContractService : BaseService<Contract>, IContractService
    {
        #region Properties

        private readonly IContext _context;
        private readonly IContractRepository _repository;

        #endregion Properties

        #region Constructors

        public ContractService(IContext context, IContractRepository repository)
            : base(context, repository)
        {
            _repository = repository;
            _context = context;
        }

        public void Delete(ContractViewModel model)
        {
            var item = _repository.FindById(model.Id);
            if (item != null)
                _repository.Delete(item);
        }

        public List<ContractViewModel> GetContractByEmployee(long employeeId)
        {
            return _repository.GetAllByCondition(x => x.Employee.Id == employeeId, x => x.Employee, x => x.CurrentContractType)
                .ProjectTo<ContractViewModel>().ToList();
        }
             

        public List<ContractViewModel> GetContracts()
        {
            return _repository.GetAll().ProjectTo<ContractViewModel>().ToList();
        }

        public ContractViewModel GetInfo(long id)
        {
            var contractType = _repository.FindById(id);
            if (contractType != null)
                return AutoMapper.Mapper.Map<Contract, ContractViewModel>(contractType);

            return new ContractViewModel();
        }

        public void Insert(ContractViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<ContractViewModel, Contract>(model);
            _repository.Add(contractType);
        }

        public void Update(ContractViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<ContractViewModel, Contract>(model);

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