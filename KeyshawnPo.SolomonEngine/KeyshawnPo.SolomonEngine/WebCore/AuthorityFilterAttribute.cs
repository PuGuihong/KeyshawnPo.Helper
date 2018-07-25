using KeyshawnPo.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace KeyshawnPo.SolomonEngine
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class AuthorityFilterAttribute : ActionFilterAttribute
    {

        #region 属性
        /// <summary>
        /// 页面标识
        /// </summary>
        private string moduleID
        {
            get;
            set;
        }

        /// <summary>
        /// 功能权限标识
        /// </summary>
        private FunctionType funcType
        {
            get;
            set;
        }
        #endregion

        #region 构造
        /// <summary>
        /// 构造函数（用于功能权限校验）
        /// </summary>
        /// <param name="pageID">页面标识</param>
        /// <param name="funcID">功能类型</param>
        /// <param name="authType">权限类型</param>
        public AuthorityFilterAttribute(string moduleID, FunctionType funcType)
        {
            this.moduleID = moduleID;
            this.funcType = funcType;
        }

        /// <summary>
        /// 构造函数(用于登录权限校验)
        /// </summary>
        /// <param name="pageID"></param>
        public AuthorityFilterAttribute(string moduleID)
            : this(moduleID, FunctionType.None)
        {

        }

        /// <summary>
        /// 构造函数（用于匿名权限校验）
        /// </summary>
        public AuthorityFilterAttribute()
            : this("")
        {

        }
        #endregion

        #region 方法
        /// <summary>
        /// 权限拦截
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }
        #endregion
    }
}