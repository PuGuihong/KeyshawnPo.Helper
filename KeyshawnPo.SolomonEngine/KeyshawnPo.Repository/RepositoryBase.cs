using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyshawnPo.DAO
{
    public class RepositoryBase<T, PK> : IRepository<T, PK> where T : class
    {

        PetaPoco.Database _db = new PetaPoco.Database("MysqlConnectionString");

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void CloseSession()
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(PK id)
        {
            throw new NotImplementedException();
        }

        public void Delete(IList<PK> idList)
        {
            throw new NotImplementedException();
        }

        public int DeleteHQL(string queryString)
        {
            throw new NotImplementedException();
        }

        public List<T> Find(string queryString)
        {
            throw new NotImplementedException();
        }

        public List<T> Find(string queryString, object[] values)
        {
            throw new NotImplementedException();
        }

        public List<T> FindByNamedParam(string queryString, string[] paramNames, object[] values)
        {
            throw new NotImplementedException();
        }

        public List<T> FindByNamedQuery(string queryName)
        {
            throw new NotImplementedException();
        }

        public List<T> FindByNamedQuery(string queryName, object[] values)
        {
            throw new NotImplementedException();
        }

        public List<T> FindListByHql(string hql, object[] values, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public void Flush()
        {
            throw new NotImplementedException();
        }

        public T Get(PK id)
        {
            T _objRlt = _db.Single<T>(id);
            return _objRlt;
        }

        public T Load(PK id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> LoadAll<T>()
        {
            string _tbName = typeof(T).Name;
            IEnumerable<T> _lstObj = _db.Query<T>("select * from " + _tbName);
            return _lstObj;
        }

        public IQueryable<T> LoadAllWithPage(out long count, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public int PerformNativeSQL(string queryString, object[] values)
        {
            throw new NotImplementedException();
        }

        public List<T> PerformNativeSQLSelect(string SQLStr)
        {
            throw new NotImplementedException();
        }

        public List<T> PerformNativeSQLSelect(string SQLStr, object[] values)
        {
            throw new NotImplementedException();
        }

        public List<T> PerformNativeSQLSelect(string SQLStr, object[] values, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public object Save(T entity)
        {
            var _objRlt = entity;
            _db.Save(entity);
            return _objRlt;
        }

        public void SaveOrUpdate(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
