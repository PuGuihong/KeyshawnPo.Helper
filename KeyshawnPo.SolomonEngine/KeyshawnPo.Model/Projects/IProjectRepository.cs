using KeyshawnPo.Infrastructure.RepositoryFramework;
using KeyshawnPo.Model.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyshawnPo.Infrastructure.Projects
{
    public interface IProjectRepository : IRepository<Project>
    {
        IList<Project> FindBy(object sector, object segment, bool completed);
    }
}
