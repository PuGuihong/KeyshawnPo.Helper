using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyshawnPo.Service
{
    /// <summary>
    /// 日志帮助类
    /// </summary>
    public static class LogHelper
    {
        #region Error
        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="message">描述做什么事情</param>
        /// <param name="exception">捕获到的异常信息</param>
        public static void Error(string message, Exception exception)
        {
            Hashtable records = new Hashtable();
            records["Message"] = message;
            records["ErrorMsg"] = exception.Message;
            records["StackTrace"] = exception.StackTrace;
            records["LevelNum"] = 1;
        }
        #endregion

        #region Info
        /// <summary>
        /// 记录一般日志
        /// </summary>
        /// <param name="message">描述做什么事情</param>
        public static void Info(string message)
        {
            Hashtable records = new Hashtable();
            records["Message"] = message;

        }
        #endregion

        #region Fatal
        /// <summary>
        /// 严重错误
        /// </summary>
        /// <param name="message">描述做什么事情</param>
        /// <param name="exception">捕获到的异常信息</param>
        public static void Fatal(string message, Exception exception)
        {
            Hashtable records = new Hashtable();
            records["Message"] = message;
            records["rrorMsg"] = exception.Message;
            records["StackTrace"] = exception.StackTrace;
            records["LevelNum"] = 2;

        }
        #endregion
    }
}

