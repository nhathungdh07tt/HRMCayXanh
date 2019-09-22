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
    public interface ICompanyContactRepository : IRepository<CompanyContact>
    {
    }

    public class CompanyContactRepository : BaseRepository<CompanyContact>, ICompanyContactRepository
    {
        public CompanyContactRepository(IContext dbFactory)
            : base(dbFactory)
        {
        }
    }
}
