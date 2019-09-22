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
    public interface ICountryService : IService<Country>
    {
        List<CountryViewModel> GetCountries();
        CountryViewModel GetInfo(long id);
        void Insert(CountryViewModel model);
        void Update(CountryViewModel model);
        void Delete(CountryViewModel model);
    }

    public class CountryService : BaseService<Country>, ICountryService
    {
        #region Properties

        private readonly IContext _context;
        private readonly ICountryRepository _repository;

        #endregion Properties

        #region Constructors

        public CountryService(IContext context, ICountryRepository repository)
            : base(context, repository)
        {
            _repository = repository;
            _context = context;
        }

        public void Delete(CountryViewModel model)
        {
            var item = _repository.FindById(model.Id);
            if (item != null)
                _repository.Delete(item);
        }

        public List<CountryViewModel> GetCountries()
        {
            return _repository.GetAll().ProjectTo<CountryViewModel>().ToList();
        }

        public CountryViewModel GetInfo(long id)
        {
            var contractType = _repository.FindById(id);
            if (contractType != null)
                return AutoMapper.Mapper.Map<Country, CountryViewModel>(contractType);

            return new CountryViewModel();
        }

        public void Insert(CountryViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<CountryViewModel, Country>(model);
            _repository.Add(contractType);
        }

        public void Update(CountryViewModel model)
        {
            var contractType = AutoMapper.Mapper.Map<CountryViewModel, Country>(model);
            _repository.Update(contractType);
        }

        #endregion Constructors


    }
}