using KeyshawnPo.IRepository;
using KeyshawnPo.Repository;
using KeyshawnPo.Service;
using MysqlConnectionString;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KeyshawnPo.SolomonEngine.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            var dt = DbProviderFactories.GetFactoryClasses();
            AccountInfoService.Instance.CurrentRepository = AccountInfoRepository.Instance;
            var _lstRlt = AccountInfoService.Instance.CurrentRepository.LoadAll<sys_account>();
            var _jsonRlt = JsonConvert.SerializeObject(_lstRlt);
            //return View(_jsonRlt);
            ViewBag.Data = _jsonRlt;
            return View();
        }
    }
}