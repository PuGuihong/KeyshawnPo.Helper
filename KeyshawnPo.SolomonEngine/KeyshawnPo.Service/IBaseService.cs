using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KeyshawnPo.Service
{
    public interface IBaseService<T, PK> where T : class
    {
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>实体</returns>
        T Get(PK id);

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>实体</returns>
        T Load(PK id);

        /// <summary>
        /// 插入实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>ID</returns>
        object Save(T entity);

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="entity">实体</param>
        void Update(T entity);

        /// <summary>
        /// 修改或保存实体
        /// </summary>
        /// <param name="entity">实体</param>
        void SaveOrUpdate(T entity);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id">ID</param>
        void Delete(PK id);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="idList">ID集合</param>
        void Delete(IList<PK> idList);

        /// <summary>
        /// 获取全部集合
        /// </summary>
        /// <returns>集合</returns>
        IList<T> LoadAll<T>();

        /// <summary>
        /// 分页获取全部集合
        /// </summary>
        /// <param name="count">记录总数</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns>集合</returns>
        IList<T> LoadAllWithPage(out long count, int pageIndex, int pageSize);
    }
}
