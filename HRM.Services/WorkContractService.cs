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
using HRM.ViewModels.Work;

namespace HRM.Services
{
    public interface IWorkContractService : IService<WorkContract>
    {
        List<WorkContractViewModel> GetWorkContracts();
        WorkContractViewModel GetInfo(long id);
        void Insert(WorkContractViewModel model);
        void Update(WorkContractViewModel model);
        void Delete(WorkContractViewModel model);
    }

    public class WorkContractService : BaseService<WorkContract>, IWorkContractService
    {
        #region Properties

        private readonly IContext _context;
        private readonly IWorkContractRepository _repository;

        #endregion Properties

        #region Constructors

        public WorkContractService(IContext context, IWorkContractRepository repository)
            : base(context, repository)
        {
            _repository = repository;
            _context = context;
        }

        public void Delete(WorkContractViewModel model)
        {
            var item = _repository.FindById(model.Id);
            if (item != null)
                _repository.Delete(item);
        }

        public List<WorkContractViewModel> GetWorkContracts()
        {
            return _repository.GetAll().ProjectTo<WorkContractViewModel>().ToList();
        }

        public WorkContractViewModel GetInfo(long id)
        {
            var contractType = _repository.FindById(id);
            if (contractType != null)
                return AutoMapper.Mapper.Map<WorkContract, WorkContractViewModel>(contractType);

            return new WorkContractViewModel();
        }

        public void Insert(WorkContractViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<WorkContractViewModel, WorkContract>(model);
            _repository.Add(contractType);
        }

        public void Update(WorkContractViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<WorkContractViewModel, WorkContract>(model);
            _repository.Update(contractType);
        }

        #endregion Constructors


    }
}