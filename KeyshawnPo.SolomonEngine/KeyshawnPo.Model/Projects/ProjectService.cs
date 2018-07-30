using KeyshawnPo.Infrastructure.Projects;
using KeyshawnPo.Infrastructure.RepositoryFramework;
using System.Collections.Generic;

namespace KeyshawnPo.Model.Projects
{
    public static class ProjectService
    {
        public static IList<Project> GetAllProjects()
        {
            IProjectRepository repository =
                RepositoryFactory.GetRepository<IProjectRepository, Project>();
            return repository.FindAll();
        }

    }
}
