using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KeyshawnPo.Service
{
    public class DataAccess<T>
    {
        #region Field
        private static Hashtable objCache = new Hashtable(); //对象缓存 
        #endregion

        #region Method
        /// <summary>
        /// 创建实现此接口T的对象
        /// </summary>
        /// <param name="assembly">需要查找的程序集对象</param>
        /// <returns>实现此接口的对象</returns>
        public static T Create(Assembly assembly)
        {
            string cacheKey = typeof(T).FullName;
            if (!objCache.ContainsKey(cacheKey))
            {
                T obj = default(T);
                Type type = assembly.GetTypes().Where(t => t.GetInterface(cacheKey) != null).FirstOrDefault();
                if (type != null)
                {
                    obj = (T)Activator.CreateInstance(type);
                    objCache.Add(cacheKey, obj);
                }
                return obj;
            }
            return (T)objCache[cacheKey];
        }
        #endregion
    }
}
