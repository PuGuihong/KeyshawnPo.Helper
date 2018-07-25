using KeyshawnPo.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KeyshawnPo.Service
{
    public class BaseService<T, PK> : IBaseService<T, PK> where T : class
    {
        public IRepository<T, PK> CurrentRepository { get; set; }

        public virtual T Get(PK id)
        {
            if (id == null)
            {
                return null;
            }
            return this.CurrentRepository.Get(id);
        }

        public virtual T Load(PK id)
        {
            if (id == null)
            {
                return null;
            }
            return this.CurrentRepository.Load(id);
        }

        public virtual object Save(T entity)
        {
            if (entity == null)
            {
                return null;
            }
            return this.CurrentRepository.Save(entity);
        }

        public virtual void Update(T entity)
        {
            if (entity == null)
            {
                return;
            }

            this.CurrentRepository.Update(entity);
        }

        public virtual void Delete(PK id)
        {
            if (id == null)
            {
                return;
            }

            this.CurrentRepository.Delete(id);
        }

        public virtual IList<T> LoadAll<T>()
        {
            string _tbName = typeof(T).Name;
            return this.CurrentRepository.LoadAll<T>().ToList();
        }

        public virtual IList<T> LoadAllWithPage(out long count, int pageIndex, int pageSize)
        {
            return this.CurrentRepository.LoadAllWithPage(out count, pageIndex, pageSize).ToList();
        }

        public virtual void Delete(IList<PK> idList)
        {
            if (idList == null || idList.Count == 0)
            {
                return;
            }

            this.CurrentRepository.Delete(idList);
        }

        public virtual void SaveOrUpdate(T entity)
        {
            if (entity == null)
            {
                return;
            }

            this.CurrentRepository.SaveOrUpdate(entity);
        }

        public void Delete(T entity)
        {
            this.CurrentRepository.Delete(entity);
        }
    }
}

