using KeyshawnPo.Infrastructure.DomainBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyshawnPo.Infrastructure.RepositoryFramework
{
    public interface IUnitOfWorkRepository
    {
        void PeristNewItem(EntityBase item);

        void PersistUpdatedItem(EntityBase item);

        void PersistDeletedItem(EntityBase item);
    }
}
