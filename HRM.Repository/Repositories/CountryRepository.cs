using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain.Entity;
using HRM.Repository.Base;
using HRM.Repository.Base.Interfaces;

namespace HRM.Repository.Repositories
{
    public interface ICountryRepository : IRepository<Country>
    {
    }

    public class CountryRepository : BaseRepository<Country>, ICountryRepository
    {
        public CountryRepository(IContext dbFactory)
            : base(dbFactory)
        {
        }
    }
}
