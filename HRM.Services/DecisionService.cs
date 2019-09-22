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
    public interface IDecisionService : IService<Decision>
    {
        List<DecisionViewModel> GetDecisions();
        DecisionViewModel GetInfo(long id);
        void Insert(DecisionViewModel model);
        void Update(DecisionViewModel model);
        void Delete(DecisionViewModel model);
        List<DecisionViewModel> GetDecisionByEmployee(long employeeId);
    }

    public class DecisionService : BaseService<Decision>, IDecisionService
    {
        #region Properties

        private readonly IContext _context;
        private readonly IDecisionRepository _repository;

        #endregion Properties

        #region Constructors

        public DecisionService(IContext context, IDecisionRepository repository)
            : base(context, repository)
        {
            _repository = repository;
            _context = context;
        }

        public void Delete(DecisionViewModel model)
        {
            var item = _repository.FindById(model.Id);
            if (item != null)
                _repository.Delete(item);
        }
               
        public List<DecisionViewModel> GetDecisionByEmployee(long employeeId)
        {
            return _repository.GetAllByCondition(x => x.Employee.Id == employeeId, x => x.Employee, x => x.Type)
                .ProjectTo<DecisionViewModel>().ToList();
        }

        public List<DecisionViewModel> GetDecisions()
        {
            return _repository.GetAll().ProjectTo<DecisionViewModel>().ToList();
        }

        public DecisionViewModel GetInfo(long id)
        {
            var contractType = _repository.FindById(id);
            if (contractType != null)
                return AutoMapper.Mapper.Map<Decision, DecisionViewModel>(contractType);

            return new DecisionViewModel();
        }

        public void Insert(DecisionViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<DecisionViewModel, Decision>(model);
            _repository.Add(contractType);
        }

        public void Update(DecisionViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<DecisionViewModel, Decision>(model);
            
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