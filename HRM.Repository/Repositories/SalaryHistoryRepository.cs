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
    public interface ISalaryHistoryRepository : IRepository<SalaryHistory>
    {
    }

    public class SalaryHistoryRepository : BaseRepository<SalaryHistory>, ISalaryHistoryRepository
    {
        public SalaryHistoryRepository(IContext dbFactory)
            : base(dbFactory)
        {

        }
    }
}
