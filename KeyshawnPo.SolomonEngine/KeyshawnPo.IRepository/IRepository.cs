using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyshawnPo.DAO
{
    public interface IRepository<T, PK> where T : class
    {
        #region 添加
        /// <summary>
        /// 插入实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>ID</returns>
        object Save(T entity);
        #endregion

        #region 修改
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
        #endregion

        #region 删除
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
        /// HQL删除
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        int DeleteHQL(string queryString);

        /// <summary>
        /// 带参数的删除语句，可实现批量删除
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="values"></param>
        /// <param name="types"></param>
        //int Delete(string queryString, object[] values, NHibernate.Type.IType[] types);

        /// <summary>
        /// 加锁并删除指定的实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="locked"></param>
        //void DeleteWithLock(T entity, Autofac.Module locked);

        /// <summary>
        /// 根据主键加锁并删除指定的实体
        /// </summary>
        /// <param name="id"></param>
        /// <param name="locked"></param>
        //void DeleteByKeyWithLock(PK id, Autofac.Module locked);
        #endregion

        #region 查询

        /// <summary>
        /// 执行原生SQL
        /// </summary>
        /// <param name="SQLStr"></param>
        /// <returns></returns>
        List<T> PerformNativeSQLSelect(string SQLStr);

        /// <summary>
        /// 执行原生SQL语句-不分页-带参数
        /// </summary>
        /// <param name="SQLStr"></param>
        /// <returns></returns>
        List<T> PerformNativeSQLSelect(string SQLStr, object[] values);

        /// <summary>
        /// 执行原生SQL语句-分页-带参数
        /// </summary>
        /// <param name="SQLStr"></param>
        /// <returns></returns>
        List<T> PerformNativeSQLSelect(string SQLStr, object[] values, int pageIndex, int pageSize);

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
        /// 获取全部集合
        /// </summary>
        /// <returns>集合</returns>
        IEnumerable<T> LoadAll<T>(string tbName);

        /// <summary>
        /// 分页获取全部集合
        /// </summary>
        /// <param name="count">记录总数</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns>集合</returns>
        IQueryable<T> LoadAllWithPage(out long count, int pageIndex, int pageSize);

        /// <summary>
        /// 使用hql获取分页,实体列表
        /// </summary>
        /// <param name="hql"></param>
        /// <param name="values"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        List<T> FindListByHql(string hql, object[] values, int pageIndex, int pageSize);

        /// <summary>
        /// 使用HSQL语句查询数据
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        List<T> Find(String queryString);

        /// <summary>
        /// 使用带参数的HSQL语句查询指定数据
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        List<T> Find(String queryString, Object[] values);

        /// <summary>
        /// 使用带命名的参数的HSQL语句查询数据
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="paramNames"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        List<T> FindByNamedParam(string queryString, string[] paramNames,
                                     Object[] values);

        /// <summary>
        /// 使用命名的HSQL语句查询数据
        /// </summary>
        /// <param name="queryName"></param>
        /// <returns></returns>
        List<T> FindByNamedQuery(String queryName);

        /// <summary>
        /// 使用带参数的命名HSQL语句查询数据
        /// </summary>
        /// <param name="queryName"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        List<T> FindByNamedQuery(String queryName, Object[] values);
        #endregion

        #region 其他
        //ISession GetSession();

        void CloseSession();

        void Clear();

        void Flush();

        /// <summary>
        /// 执行原生SQL
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        int PerformNativeSQL(string queryString, object[] values);
        #endregion
    }
}
