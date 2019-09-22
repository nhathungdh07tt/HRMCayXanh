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
    public interface ICompanyContactService : IService<CompanyContact>
    {
        List<CompanyContactViewModel> GetCompanyContacts();
        CompanyContactViewModel GetInfo(long id);
        void Insert(CompanyContactViewModel model);
        void Update(CompanyContactViewModel model);
        void Delete(CompanyContactViewModel model);
    }

    public class CompanyContactService : BaseService<CompanyContact>, ICompanyContactService
    {
        #region Properties

        private readonly IContext _context;
        private readonly ICompanyContactRepository _repository;

        #endregion Properties

        #region Constructors

        public CompanyContactService(IContext context, ICompanyContactRepository repository)
            : base(context, repository)
        {
            _repository = repository;
            _context = context;
        }

        public void Delete(CompanyContactViewModel model)
        {
            var item = _repository.FindById(model.Id);
            if (item != null)
                _repository.Delete(item);
        }

        public List<CompanyContactViewModel> GetCompanyContacts()
        {
            return _repository.GetAll().ProjectTo<CompanyContactViewModel>().ToList();
        }

        public CompanyContactViewModel GetInfo(long id)
        {
            var contractType = _repository.FindById(id);
            if (contractType != null)
                return AutoMapper.Mapper.Map<CompanyContact, CompanyContactViewModel>(contractType);

            return new CompanyContactViewModel();
        }

        public void Insert(CompanyContactViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<CompanyContactViewModel, CompanyContact>(model);
            _repository.Add(contractType);
        }

        public void Update(CompanyContactViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<CompanyContactViewModel, CompanyContact>(model);
            _repository.Update(contractType);
        }

        #endregion Constructors


    }
}