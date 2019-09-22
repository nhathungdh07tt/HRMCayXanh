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
    public interface ICompanyChangeHistoryRepository : IRepository<CompanyChangeHistory>
    {
    }

    public class CompanyChangeHistoryRepository : BaseRepository<CompanyChangeHistory>, ICompanyChangeHistoryRepository
    {
        public CompanyChangeHistoryRepository(IContext dbFactory)
            : base(dbFactory)
        {
        }
    }
}
