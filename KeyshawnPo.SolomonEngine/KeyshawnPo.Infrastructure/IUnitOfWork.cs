using KeyshawnPo.Infrastructure.Domain.Base;
using KeyshawnPo.Infrastructure.RepositoryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyshawnPo.Infrastructure
{
    public interface IUnitOfWork
    {
        void RegisterAdded(EntityBase entity
            , IUnitOfWorkRepository repository);

        void RegisterChanged(EntityBase entity
            , IUnitOfWorkRepository repository);

        void RegisterRemoved(EntityBase entity
            , IUnitOfWorkRepository repository);

        void Commit();
    }
}
