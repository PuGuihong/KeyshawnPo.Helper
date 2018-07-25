using KeyshawnPo.IRepository;
using KeyshawnPo.Repository;
using MysqlConnectionString;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace KeyshawnPo.Service
{
    public class AccountInfoService : BaseService<sys_account, string>
    {
        #region Constructor
        /// <summary>
        /// 私有构造函数，禁止外界访问
        /// </summary>
        private AccountInfoService()
        { }
        #endregion

        #region Field
        /// <summary>
        /// 静态变量，用于保存类的实例
        /// </summary>
        private static AccountInfoService _instance;

        /// <summary>
        /// 线程标志，确保线程同步
        /// </summary>
        private static object _locker = new object();

        #endregion

        #region Property
        /// <summary>
        /// 定义公共属性，使全局均可访问
        /// </summary>
        public static AccountInfoService Instance
        {
            get
            {
                //实例不存在则创建
                if (_instance == null)
                {
                    //当线程来的时候进程先挂起
                    lock (_locker)
                    {
                        if (_instance == null)
                        {
                            _instance = new AccountInfoService();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion
    }
}
