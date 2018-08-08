using KeyshawnPo.Infrastructure.DomainBase;
using KeyshawnPo.Infrastructure.EntityFactoryFramework;
using KeyshawnPo.Infrastructure.RepositoryFramework;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace KeyshawnPo.Infrastructure.Repositories
{
    public abstract class SqlCeRepositoryBase<T> :
        RepositoryBase<T> where T : EntityBase
    {

        #region AppednChildData　Delegage

        public delegate void
            AppendChildData(T entityAggregate,
            object childEntityKeyValue);
        #endregion

        #region Private Members
        private Database database;
        private IEntityFactory<T> entityFactory;
        private Dictionary<string, AppendChildData> childCallbacks;

        #endregion

        #region Constructors
        protected SqlCeRepositoryBase() : this(null)
        {

        }

        protected SqlCeRepositoryBase(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            this.database = DatabaseFactory.CreateDatabase();
            this.entityFactory = EntityFactoryBuilder.BuildFactory<T>();
            this.childCallbacks = new Dictionary<string, SqlCeRepositoryBase<T>.AppendChildData>();
            this.BuildChildCallbacks();
        }
        #endregion

        #region Abstract Methods
        protected abstract void
            BuildChildCallbacks();
        public abstract override T
            FindBy(object key);
        protected abstract override void
            PersistNewItem(T item);
        protected abstract override void
            PersistUpdatedItem(T item);
        protected abstract override void
             PersistDeletedItem(T item);
        #endregion

        #region Properties
        protected Database Database
        {
            get
            {
                return this.database;
            }
        }

        protected Dictionary<string, AppendChildData> ChildCallbacks
        {
            get { return this.childCallbacks; }
        }
        #endregion

        #region Protectd Methods
        protected IDataReader ExecuteReader(string sql)
        {
            DbCommand command = this.database.GetSqlStringCommand(sql);
            return this.database.ExecuteReader(command);
        }

        protected virtual T BuildEntityFromSql(string sql)
        {
            T entity = default(T);
            using (IDataReader reader =
                this.ExecuteReader(sql))
            {
                if (reader.Read())
                {
                    entity = this.BuildEntityFromReader(reader);
                }
            }
            return entity;
        }

        protected virtual T BuildEntityFromReader(IDataReader reader)
        {
            T entity = this.entityFactory.BuildEntity(reader);
            if (this.childCallbacks != null && this.childCallbacks.Count > 0)
            {
                object childKeyValue = null;
                DataTable columnData = reader.GetSchemaTable();
                foreach (string childKeyName in this.childCallbacks.Keys)
                {
                    //if (DataHelper.ReaderContainsColumnName(columnData
                    //    , childKeyName))
                    //{
                    //    childKeyValue = reader[childKeyName];
                    //}
                    //else
                    //{
                    //    childKeyValue = null;
                    //}
                }
            }
            return entity;
        }

        protected virtual List<T> BuildEntitiesFromSql(string sql)
        {
            List<T> entities = new List<T>();
            using (IDataReader reader = this.ExecuteReader(sql))
            {
                while (reader.Read())
                {
                    entities.Add(this.BuildEntityFromReader(reader));
                }
            }
            return entities;
        }
        #endregion
    }
}
