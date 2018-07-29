using KeyshawnPo.Infrastructure.Domain.Base;
using KeyshawnPo.Infrastructure.RepositoryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace KeyshawnPo.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {

        private Dictionary<EntityBase, IUnitOfWorkRepository>
            addedEntities;
        private Dictionary<EntityBase, IUnitOfWorkRepository>
            changeEntities;
        private Dictionary<EntityBase, IUnitOfWorkRepository>
            deleteedEntities;

        public UnitOfWork()
        {
            this.addedEntities =
                new Dictionary<EntityBase, IUnitOfWorkRepository>();
            this.changeEntities =
                new Dictionary<EntityBase, IUnitOfWorkRepository>();
            this.deleteedEntities =
                new Dictionary<EntityBase, IUnitOfWorkRepository>();
        }

        #region IUnitOfWork Members
        public void RegisterAdded(EntityBase entity,
            IUnitOfWorkRepository repository)
        {
            this.addedEntities.Add(entity, repository);
        }

        public void RegisterChanged(EntityBase entity,
            IUnitOfWorkRepository repository)
        {
            this.changeEntities.Add(entity, repository);
        }

        public void RegisterRemoved(EntityBase entity,
            IUnitOfWorkRepository repository)
        {
            this.deleteedEntities.Add(entity, repository);
        }

        public void Commit()
        {
            using (TransactionScope scope
                = new TransactionScope())
            {
                foreach (EntityBase entity
                    in this.deleteedEntities.Keys)
                {
                    this.deleteedEntities[entity].
                        PersistDeletedItem(entity);
                }

                foreach (EntityBase entity
                    in this.addedEntities.Keys)
                {
                    this.addedEntities[entity]
                        .PersistDeletedItem(entity);
                }

                foreach (EntityBase entity
                    in this.changeEntities.Keys)
                {
                    this.changeEntities[entity]
                        .PersistDeletedItem(entity);
                }

                scope.Complete();
            }
            this.deleteedEntities.Clear();
            this.addedEntities.Clear();
            this.changeEntities.Clear();
        }
        #endregion
    }
}
