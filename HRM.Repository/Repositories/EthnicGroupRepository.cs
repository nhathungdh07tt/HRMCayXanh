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
    public interface IEthnicGroupRepository : IRepository<EthnicGroup>
    {
    }

    public class EthnicGroupRepository : BaseRepository<EthnicGroup>, IEthnicGroupRepository
    {
        public EthnicGroupRepository(IContext dbFactory)
            : base(dbFactory)
        {
        }
    }
}
