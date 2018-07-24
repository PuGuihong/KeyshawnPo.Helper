using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KeyshawnPo.Service
{
    public class BaseService<I>
    {
        #region 字段
        /// <summary>
        /// 定义数据程序集名称
        /// </summary>
        private static readonly string AssemblyPath = "Repository";
        #endregion

        /// <summary>
        /// 获取数据访问集
        /// </summary>
        protected internal I Repository
        {
            get
            {
                Assembly assembly = Assembly.Load(AssemblyPath);
                return DataAccess<I>.Create(assembly);
            }
        }
    }
}
