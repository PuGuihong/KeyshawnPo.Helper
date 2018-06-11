using Dao.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KeyshawnPo.Demo.HtmlFactoryWeb.Controllers
{
    public class TmplCreateController : Controller
    {
        // GET: TmplCreate
        public ActionResult Index()
        {
            //获取所有可以进行操作的元素
            Entities _Entities = new Entities();
            List<Tmplete> _lstTmplete = _Entities.Tmplete.ToList();

            return View(_lstTmplete);
        }
    }
}