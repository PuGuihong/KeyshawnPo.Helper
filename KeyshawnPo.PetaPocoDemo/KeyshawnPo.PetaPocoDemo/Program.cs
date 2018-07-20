using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyshawnPo.PetaPocoDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.ReadKey();
        }


        PetaPoco.Database db = new PetaPoco.Database("connectionString");

        /// <summary>
        /// 查询信息列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tbName"></param>
        /// <returns></returns>
        public IEnumerable<T> Search<T>(string tbName, string strWhere)
        {
            string _strSql = "select * from " + tbName;
            if (!string.IsNullOrEmpty(strWhere))
            {
                _strSql += "" + strWhere;
            }
            IEnumerable<T> _lstRlt = db.Query<T>(_strSql);
            return _lstRlt;
        }


        public int SearchScalar(string tbName, string strWhere)
        {
            int _intRlt = 0;
            string _strSql = "select count(*) from " + tbName;
            if (!string.IsNullOrEmpty(strWhere))
            {
                _strSql += "" + strWhere;
            }
            _intRlt = db.ExecuteScalar<int>(_strSql);
            return _intRlt;
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tbName"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public T GetModel<T>(string tbName, string strWhere)
        {
            T _tRlt;
            string _strSql = "select * from " + tbName;
            if (!string.IsNullOrEmpty(strWhere))
            {
                _strSql += "" + strWhere;
            }
            _tRlt = db.SingleOrDefault<T>(_strSql);

            return _tRlt;
        }

        /// <summary>
        /// 数据插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tbName"></param>
        /// <param name="primaryKey"></param>
        /// <param name="autoIncrement"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public int InsertModel<T>(string tbName, string primaryKey, bool autoIncrement, T t)
        {
            int _intRlt = 0;
            _intRlt = Convert.ToInt32(db.Insert(tbName, primaryKey, autoIncrement, t));
            return _intRlt;
        }

        /// <summary>
        ///  更新单条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tbName"></param>
        /// <param name="primaryKeyValue"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public int UpdateModel<T>(string tbName, string primaryKeyValue, T t)
        {
            int _intRlt = 0;
            _intRlt = db.Update(tbName, primaryKeyValue, t);
            return _intRlt;
        }
    }
}
