using KeyshawnPo.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KeyshawnPo.SolomonEngine
{
    /// <summary>
    /// 异常信息处理类
    /// </summary>
    public class CustomExceptionAttribute : FilterAttribute, IExceptionFilter
    {

        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled == true)
            {
                HttpException httpExec = filterContext.Exception as HttpException;
                if (httpExec != null && httpExec.GetHttpCode() != 500)
                    return;
            }
            Exception exception = filterContext.Exception as Exception;
            if (exception != null)
            {
                LogHelper.Error("系统错误", exception);
                filterContext.Controller.ViewBag.UrlRefer = filterContext.HttpContext.Request.UrlReferrer;
                if (exception is HttpException)
                {
                    HttpException httpException = exception as HttpException;
                    //404错误
                    if (httpException.GetHttpCode() == 404)
                    {
                        filterContext.HttpContext.Response.Redirect("~/Error/NotFound");
                    }
                    //500错误
                    else if (httpException.GetHttpCode() == 500)
                    {
                        filterContext.HttpContext.Response.Redirect("~/Error/InternalError");
                    }
                    else
                    {
                        filterContext.HttpContext.Response.Redirect("~/Error/Index");
                    }
                }
                else
                {
                    filterContext.HttpContext.Response.Redirect("~/Error/Index");
                }
            }
            //记录日志，设置异常信息已处理
            filterContext.ExceptionHandled = true;
        }
    }
}