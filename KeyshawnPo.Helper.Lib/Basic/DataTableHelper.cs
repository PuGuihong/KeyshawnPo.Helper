using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace KeyshawnPo.Helper.Lib
{
    public class DataTableHelper<T> where T : new()
    {
        /// <summary>
        /// datatable转list
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> DataTable2ModelList(DataTable dt)
        {

            List<T> ts = new List<T>();// 定义集合
            Type type = typeof(T); // 获得此模型的类型
            string tempName = "";
            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                PropertyInfo[] propertys = t.GetType().GetProperties();// 获得此模型的公共属性
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;
                    if (dt.Columns.Contains(tempName))
                    {
                        if (!pi.CanWrite) continue;
                        object value = dr[tempName];
                        if (value != DBNull.Value)
                            pi.SetValue(t, value, null);
                    }
                }
                ts.Add(t);
            }
            return ts;
        }

        /// <summary>
        /// ModelList转DataTable
        /// </summary>
        /// <param name="list">实体集合</param>
        /// <returns>DataTable</returns>
        public DataTable ModelList2DataTable(IList list)
        {
            if (list == null || list.Count == 0)
            {
                return null;
            }
            DataTable _dt = new DataTable();
            try
            {
                //获取并指定列名
                PropertyInfo[] _propertys = list[0].GetType().GetProperties();
                foreach (PropertyInfo _pInfo in _propertys)
                {
                    _dt.Columns.Add(_pInfo.Name, _pInfo.PropertyType.GetGenericArguments().Length > 0 ? _pInfo.PropertyType.GetGenericArguments()[0] : _pInfo.PropertyType);
                }

                //逐行赋值
                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in _propertys)
                    {
                        object obj = pi.GetValue(list[i], null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    _dt.LoadDataRow(array, true);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("实体类转DataTable失败，" + ex.Message);
            }
            return _dt;
        }
    }
}
