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
    public interface IDateOffHistoryRepository : IRepository<DateOffHistory>
    {
    }

    public class DateOffHistoryRepository : BaseRepository<DateOffHistory>, IDateOffHistoryRepository
    {
        public DateOffHistoryRepository(IContext dbFactory)
            : base(dbFactory)
        {

        }
    }
}
