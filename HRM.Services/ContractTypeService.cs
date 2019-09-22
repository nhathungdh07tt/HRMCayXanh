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
    public interface IContractTypeService : IService<ContractType>
    {
        List<ContractTypeViewModel> GetContractTypes();
        ContractTypeViewModel GetInfo(long id);
        void Insert(ContractTypeViewModel model);
        void Update(ContractTypeViewModel model);
        void Delete(ContractTypeViewModel model);
    }

    public class ContractTypeService : BaseService<ContractType>, IContractTypeService
    {
        #region Properties

        private readonly IContext _context;
        private readonly IContractTypeRepository _repository;

        #endregion Properties

        #region Constructors

        public ContractTypeService(IContext context, IContractTypeRepository repository)
            : base(context, repository)
        {
            _repository = repository;
            _context = context;
        }

        public void Delete(ContractTypeViewModel model)
        {
            var item = _repository.FindById(model.Id);
            if (item != null)
                _repository.Delete(item);
        }

        public List<ContractTypeViewModel> GetContractTypes()
        {
            return _repository.GetAll().ProjectTo<ContractTypeViewModel>().ToList();
        }

        public ContractTypeViewModel GetInfo(long id)
        {
            var contractType = _repository.FindById(id);
            if (contractType != null)
                return AutoMapper.Mapper.Map<ContractType, ContractTypeViewModel>(contractType);
            
            return new ContractTypeViewModel();
        }

        public void Insert(ContractTypeViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<ContractTypeViewModel, ContractType>(model);
            _repository.Add(contractType);
        }

        public void Update(ContractTypeViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<ContractTypeViewModel, ContractType>(model);
            _repository.Update(contractType);
        }

        #endregion Constructors


    }
}