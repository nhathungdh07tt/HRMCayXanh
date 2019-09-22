using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using HRM.Domain.Entity;
using HRM.Repository;
using HRM.Repository.Repositories;
using HRM.Services.Base;
using HRM.Services.Base.Interfaces;
using HRM.ViewModels.Company;
using HRM.ViewModels.System;

namespace HRM.Services
{
    public interface ICompanyService : IService<Company>
    {
        CompanyViewModel GetCompanyInfo();
        //CompanyViewModel Update(CompanyViewModel model);
        void Update(CompanyViewModel model);
        CompanyViewModel GetInfo(long id);

    }

    public class CompanyService : BaseService<Company>, ICompanyService
    {
        #region Properties

        private readonly IContext _context;
        private readonly ICompanyRepository _repository;
        private readonly ICompanyContactRepository _companyContactRepository;
        private readonly ICompanyChangeHistoryRepository _companyChangeHistoryRepository;

        #endregion Properties

        #region Constructors

        public CompanyService(
            IContext context, 
            ICompanyRepository repository,
            ICompanyContactRepository companyContactRepository,
            ICompanyChangeHistoryRepository companyChangeHistoryRepository
            ): base(context, repository)
        {
            _repository = repository;
            this._companyContactRepository = companyContactRepository;
            this._companyChangeHistoryRepository = companyChangeHistoryRepository;
            _context = context;
        }

        public CompanyViewModel GetCompanyInfo()
        {
            var company = _repository
                .GetAll(x => x.Contacts, x => x.ChangeHistories)
                .FirstOrDefault();
            
            if (company != null)
                return Mapper.Map<Company, CompanyViewModel>(company);
            
            return null;
        }

        //public CompanyViewModel Update(CompanyViewModel model)
        //{

        //    // Add Change Tracking
        //    var oldCompanyInfo = _repository.FindById(model.Id);
        //    var companyChangeHistory = Mapper.Map<Company, CompanyChangeHistory>(oldCompanyInfo);
        //    _companyChangeHistoryRepository.Add(companyChangeHistory);

        //    var company = Mapper.Map<CompanyViewModel, Company>(model);
        //    company.ChangedCount = oldCompanyInfo.ChangedCount + 1;

        //    // Update Company Infor
        //    var result = _repository.Update(company);

        //    this.Save();

        //    return Mapper.Map<Company, CompanyViewModel>(result);
        //}


        public void Update(CompanyViewModel model)
        {
            var company = AutoMapper.Mapper.Map<CompanyViewModel, Company>(model);
            _repository.Update(company);
        }

        public CompanyViewModel GetInfo(long id)
        {
            var company = _repository.FindById(id);
            if (company != null)
                return AutoMapper.Mapper.Map<Company, CompanyViewModel>(company);

            return new CompanyViewModel();
        }
        #endregion Constructors


    }
}
