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
using HRM.ViewModels.Document;
using HRM.ViewModels.Relation;

namespace HRM.Services
{
    public interface IRelationService : IService<Relation>
    {
        List<RelationViewModel> GetRelations();
        List<RelationViewModel> GetRelationsNCT();
        List<RelationViewModel> GetRelationsByEmployee(long employeeId);
        List<RelationViewModel> GetRelationsByEmployeeNCT(long employeeId);
        RelationViewModel GetInfo(long id);
        void Insert(RelationViewModel model);
        void Update(RelationViewModel model);
        void Delete(RelationViewModel model);
    }

    public class RelationService : BaseService<Relation>, IRelationService
    {
        #region Properties

        private readonly IContext _context;
        private readonly IRelationRepository _repository;

        #endregion Properties

        #region Constructors

        public RelationService(IContext context, IRelationRepository repository)
            : base(context, repository)
        {
            _repository = repository;
            _context = context;
        }

        public void Delete(RelationViewModel model)
        {
            var item = _repository.FindById(model.Id);
            if (item != null)
                _repository.Delete(item);
        }

        public List<RelationViewModel> GetRelations()
        {
            return _repository.GetAllByCondition(x => x.RelationType == 0).ProjectTo<RelationViewModel>().ToList();
        }

        public List<RelationViewModel> GetRelationsNCT()
        {
            return _repository.GetAllByCondition(x => x.RelationType == 1).ProjectTo<RelationViewModel>().ToList();
        }


        public List<RelationViewModel> GetRelationsByEmployee(long employeeId)
        {

            return _repository.GetAllByCondition(x => x.Employee.Id == employeeId && x.RelationType == 0, x => x.Employee,x=>x.RelationWithEmployee)
                .ProjectTo<RelationViewModel>().ToList();
        }

        public List<RelationViewModel> GetRelationsByEmployeeNCT(long employeeId)
        {

            return _repository.GetAllByCondition(x => x.Employee.Id == employeeId && x.RelationType == 1, x => x.Employee, x => x.RelationWithEmployee)
                .ProjectTo<RelationViewModel>().ToList();
        }

        public RelationViewModel GetInfo(long id)
        {
            var contractType = _repository.FindById(id);
            if (contractType != null)
                return AutoMapper.Mapper.Map<Relation, RelationViewModel>(contractType);

            return new RelationViewModel();
        }

        public void Insert(RelationViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<RelationViewModel, Relation>(model);
            _repository.Add(contractType);
        }

        public void Update(RelationViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<RelationViewModel, Relation>(model);
            _repository.Update(contractType);
        }

        #endregion Constructors


    }
}