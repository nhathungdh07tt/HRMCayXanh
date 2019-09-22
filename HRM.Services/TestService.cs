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
using HRM.ViewModels.Test;

namespace HRM.Services
{
    public interface ITestService : IService<Test>
    {
        List<TestViewModel> GetTests();
        
    }

    public class TestService : BaseService<Test>, ITestService
    {
        #region Properties

        private readonly IContext _context;
        private readonly ITestRepository _repository;

        #endregion Properties

        #region Constructors

        public TestService(IContext context, ITestRepository repository)
            : base(context, repository)
        {
            _repository = repository;
            _context = context;
        }

        public List<TestViewModel> GetTests()
        {
            return _repository.GetAll().ProjectTo<TestViewModel>().ToList();
            
        }

        #endregion Constructors


    }
}
