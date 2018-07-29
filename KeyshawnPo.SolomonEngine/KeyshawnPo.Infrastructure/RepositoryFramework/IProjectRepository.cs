using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyshawnPo.Infrastructure.RepositoryFramework
{
    public interface IProjectRepository : IRepository<Project>
    {
        IList<Project> FindBy(object sector, object segment, bool completed);
    }
    {
    }
}
