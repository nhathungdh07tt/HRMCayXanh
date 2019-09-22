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
    public interface IEthnicGroupService : IService<EthnicGroup>
    {
        List<EthnicGroupViewModel> GetEthnicGroups();
        EthnicGroupViewModel GetInfo(long id);
        void Insert(EthnicGroupViewModel model);
        void Update(EthnicGroupViewModel model);
        void Delete(EthnicGroupViewModel model);
    }

    public class EthnicGroupService : BaseService<EthnicGroup>, IEthnicGroupService
    {
        #region Properties

        private readonly IContext _context;
        private readonly IEthnicGroupRepository _repository;

        #endregion Properties

        #region Constructors

        public EthnicGroupService(IContext context, IEthnicGroupRepository repository)
            : base(context, repository)
        {
            _repository = repository;
            _context = context;
        }

        public void Delete(EthnicGroupViewModel model)
        {
            var item = _repository.FindById(model.Id);
            if (item != null)
                _repository.Delete(item);
        }

        public List<EthnicGroupViewModel> GetEthnicGroups()
        {
            return _repository.GetAll().ProjectTo<EthnicGroupViewModel>().ToList();
        }

        public EthnicGroupViewModel GetInfo(long id)
        {
            var contractType = _repository.FindById(id);
            if (contractType != null)
                return AutoMapper.Mapper.Map<EthnicGroup, EthnicGroupViewModel>(contractType);

            return new EthnicGroupViewModel();
        }

        public void Insert(EthnicGroupViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<EthnicGroupViewModel, EthnicGroup>(model);
            _repository.Add(contractType);
        }

        public void Update(EthnicGroupViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<EthnicGroupViewModel, EthnicGroup>(model);
            _repository.Update(contractType);
        }

        #endregion Constructors


    }
}