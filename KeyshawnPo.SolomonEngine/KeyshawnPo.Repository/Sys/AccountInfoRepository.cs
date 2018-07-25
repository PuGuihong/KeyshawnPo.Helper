﻿using KeyshawnPo.DAO;
using KeyshawnPo.IRepository;
using MysqlConnectionString;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyshawnPo.Repository
{
    public class AccountInfoRepository : RepositoryBase<sys_account, string>, IAccountInfoRepository
    {
        #region Constructor
        /// <summary>
        /// 私有构造函数，禁止外界访问
        /// </summary>
        private AccountInfoRepository()
        { }
        #endregion

        #region Field
        /// <summary>
        /// 静态变量，用于保存类的实例
        /// </summary>
        private static AccountInfoRepository _instance;

        /// <summary>
        /// 线程标志，确保线程同步
        /// </summary>
        private static object _locker = new object();

        #endregion

        #region Property
        /// <summary>
        /// 定义公共属性，使全局均可访问
        /// </summary>
        public static AccountInfoRepository Instance
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
                            _instance = new AccountInfoRepository();
                        }
                    }
                }
                return _instance;
            }
        }

        #endregion
    }
}
