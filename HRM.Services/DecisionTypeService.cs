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
using HRM.ViewModels.Decision;

namespace HRM.Services
{
    public interface IDecisionTypeService : IService<DecisionType>
    {
        List<DecisionTypeViewModel> GetDecisionTypes();
        DecisionTypeViewModel GetInfo(long id);
        void Insert(DecisionTypeViewModel model);
        void Update(DecisionTypeViewModel model);
        void Delete(DecisionTypeViewModel model);
    }

    public class DecisionTypeService : BaseService<DecisionType>, IDecisionTypeService
    {
        #region Properties

        private readonly IContext _context;
        private readonly IDecisionTypeRepository _repository;

        #endregion Properties

        #region Constructors

        public DecisionTypeService(IContext context, IDecisionTypeRepository repository)
            : base(context, repository)
        {
            _repository = repository;
            _context = context;
        }

        public void Delete(DecisionTypeViewModel model)
        {
            var item = _repository.FindById(model.Id);
            if (item != null)
                _repository.Delete(item);
        }

        public List<DecisionTypeViewModel> GetDecisionTypes()
        {
            return _repository.GetAll().ProjectTo<DecisionTypeViewModel>().ToList();
        }

        public DecisionTypeViewModel GetInfo(long id)
        {
            var contractType = _repository.FindById(id);
            if (contractType != null)
                return AutoMapper.Mapper.Map<DecisionType, DecisionTypeViewModel>(contractType);

            return new DecisionTypeViewModel();
        }

        public void Insert(DecisionTypeViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<DecisionTypeViewModel, DecisionType>(model);
            _repository.Add(contractType);
        }

        public void Update(DecisionTypeViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<DecisionTypeViewModel, DecisionType>(model);
            _repository.Update(contractType);
        }

        #endregion Constructors


    }
}