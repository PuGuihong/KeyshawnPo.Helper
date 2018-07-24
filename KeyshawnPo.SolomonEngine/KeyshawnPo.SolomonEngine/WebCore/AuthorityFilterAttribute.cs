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
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            //校验类型  0：权限校验  1：单点登录校验
            int type = 0;
            if (AuthorizeCore(filterContext, ref type) == false)
            {
                if (type == 0)
                {
                    if (AccountInfo.UserInfo == null)
                    {
                        //跳转到登录页面
                        filterContext.Result = new RedirectToRouteResult("Default", new RouteValueDictionary(new { Controller = "Login", Action = "TimeOut" }));
                    }
                    else
                    {
                        filterContext.Result = new ContentResult { Content = @"抱歉,你不具有当前操作的权限！" };
                    }
                }
                else
                {
                    //跳转到登录页面
                    filterContext.Result = new RedirectToRouteResult("Default", new RouteValueDictionary(new { Controller = "Login", Action = "SingleLogin" }));
                }
            }
        }
        #endregion

        #region 权限校验
        /// <summary>
        /// //权限判断业务逻辑
        /// </summary>
        /// <param name="filterContext"></param>
        /// <param name="isViewPage">是否是页面</param>
        /// <returns></returns>
        protected virtual bool AuthorizeCore(ActionExecutingContext filterContext, ref int type)
        {
            if (filterContext.HttpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            //验证当前Action是否是匿名访问Action
            if (CheckAnonymous(filterContext)) return true;

            //验证当前Action是否是登录就可以访问的Action
            if (CheckLoginAllowView(filterContext, ref type)) return true;

            //验证用户是否具有页面功能权限
            if (CheckFunctionAuthority(filterContext, ref type)) return true;

            return false;
        }
        #endregion

        #region 匿名访问
        /// <summary>
        /// [Anonymous标记]验证是否匿名访问
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        private bool CheckAnonymous(ActionExecutingContext filterContext)
        {
            return string.IsNullOrEmpty(moduleID);
        }
        #endregion

        #region 登录权限校验
        /// <summary>
        /// [LoginAllowView标记]验证是否登录就可以访问(如果已经登陆,那么不对于标识了LoginAllowView的方法就不需要验证了)
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        private bool CheckLoginAllowView(ActionExecutingContext filterContext, ref int type)
        {
            if (funcType == FunctionType.None)
            {
                //此页面需要登录校验，判断用户是否登录
                if (AccountInfo.UserInfo == null)
                {
                    return false;
                }
                if (!IsSSO(filterContext))
                {
                    type = 1;
                    return false;
                }
                return true;
            }
            return false;

        }
        #endregion

        #region 功能权限校验
        /// <summary>
        /// 功能权限校验
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        private bool CheckFunctionAuthority(ActionExecutingContext filterContext, ref int type)
        {
            bool flag = false;
            if (!string.IsNullOrEmpty(moduleID) && funcType != FunctionType.None)
            {
                flag = false;
                //此页面需要功能权限校验
                if (AccountInfo.UserInfo != null)
                {
                    //单点登录校验
                    if (IsSSO(filterContext))
                    {
                        if (!moduleID.Contains(","))
                        {
                            //用户具有当前权限，获取用户为admin，获取角色0均具有此权限
                            if (AccountInfo.UserInfo.Permissions.ContainsKey(moduleID) && AccountInfo.UserInfo.Permissions[moduleID].Contains(((int)funcType).ToString()) || AccountInfo.UserInfo.User.LOGINNAME == "admin")
                            {
                                flag = true;
                            }
                        }
                        else
                        {
                            foreach (var item in moduleID.Split(','))
                            {
                                //用户具有当前权限，获取用户为admin，获取角色0均具有此权限
                                if (!string.IsNullOrEmpty(item) && AccountInfo.UserInfo.Permissions.ContainsKey(item) && AccountInfo.UserInfo.Permissions[item].Contains(((int)funcType).ToString()) || AccountInfo.UserInfo.User.LOGINNAME == "admin")
                                {
                                    flag = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        type = 1;
                    }
                }
            }
            return flag;
        }
        #endregion
    }
}