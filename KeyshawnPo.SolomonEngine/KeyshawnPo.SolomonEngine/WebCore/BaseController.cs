using KeyshawnPo.ViewModel;
using MysqlConnectionString;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KeyshawnPo.SolomonEngine.WebCore
{
    public class BaseController : Controller
    {
        private AccountInfoModel accountInfo;
        public AccountInfoModel AccountInfo
        {
            get
            {
                if (null != accountInfo)
                {
                    return accountInfo;
                }
                else
                {
                    return accountInfo;
                }
            }
        }
    }
}