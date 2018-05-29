using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyshawnPo.Helper.Lib
{
    public class ModelAttributeHelper
    {
        #region 构造
        /// <summary>
        /// 私有构造函数，禁止外界访问
        /// </summary>
        private ModelAttributeHelper()
        { }
        #endregion

        #region 变量
        /// <summary>
        /// 静态变量，用于保存类的实例
        /// </summary>
        private static ModelAttributeHelper instance;
        /// <summary>
        /// 线程标志，确保线程同步
        /// </summary>
        private static object locker = new object();

        #endregion

        #region 属性
        /// <summary>
        /// 定义公共属性，使全局均可访问
        /// </summary>
        public static ModelAttributeHelper Instance
        {
            get
            {
                //实例不存在则创建
                if (instance == null)
                {
                    //当线程来的时候进程先挂起
                    lock (locker)
                    {
                        if (instance == null)
                        {
                            instance = new ModelAttributeHelper();
                        }
                    }
                }
                return instance;
            }
        }

        #endregion

        /// <summary>
        /// 根据属性名称获取属性显示名称
        /// </summary>
        /// <param name="modelType"></param>
        /// <param name="propertyDisplayName"></param>
        /// <returns></returns>
        public string GetDisplayName(Type modelType, string propertyDisplayName)
        {
            return (System.ComponentModel.TypeDescriptor.GetProperties(modelType)[propertyDisplayName].Attributes[typeof(System.ComponentModel.DisplayNameAttribute)] as System.ComponentModel.DisplayNameAttribute).DisplayName;
        }
    }
}
