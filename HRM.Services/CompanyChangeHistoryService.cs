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
using HRM.ViewModels.Company;

namespace HRM.Services
{
    public interface ICompanyChangeHistoryService : IService<CompanyChangeHistory>
    {
        List<CompanyChangeHistoryViewModel> GetCompanyChangeHistorys();
        CompanyChangeHistoryViewModel GetInfo(long id);
        void Insert(CompanyChangeHistoryViewModel model);
        void Update(CompanyChangeHistoryViewModel model);
        void Delete(CompanyChangeHistoryViewModel model);
    }

    public class CompanyChangeHistoryService : BaseService<CompanyChangeHistory>, ICompanyChangeHistoryService
    {
        #region Properties

        private readonly IContext _context;
        private readonly ICompanyChangeHistoryRepository _repository;

        #endregion Properties

        #region Constructors

        public CompanyChangeHistoryService(IContext context, ICompanyChangeHistoryRepository repository)
            : base(context, repository)
        {
            _repository = repository;
            _context = context;
        }

        public void Delete(CompanyChangeHistoryViewModel model)
        {
            var item = _repository.FindById(model.Id);
            if (item != null)
                _repository.Delete(item);
        }

        public List<CompanyChangeHistoryViewModel> GetCompanyChangeHistorys()
        {
            return _repository.GetAll().ProjectTo<CompanyChangeHistoryViewModel>().ToList();
        }

        public CompanyChangeHistoryViewModel GetInfo(long id)
        {
            var contractType = _repository.FindById(id);
            if (contractType != null)
                return AutoMapper.Mapper.Map<CompanyChangeHistory, CompanyChangeHistoryViewModel>(contractType);

            return new CompanyChangeHistoryViewModel();
        }

        public void Insert(CompanyChangeHistoryViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<CompanyChangeHistoryViewModel, CompanyChangeHistory>(model);
            _repository.Add(contractType);
        }

        public void Update(CompanyChangeHistoryViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<CompanyChangeHistoryViewModel, CompanyChangeHistory>(model);
            _repository.Update(contractType);
        }

        #endregion Constructors


    }
}