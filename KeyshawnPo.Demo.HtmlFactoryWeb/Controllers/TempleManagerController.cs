using Dao.EntityFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KeyshawnPo.Demo.HtmlFactoryWeb.Controllers
{
    public class TempleManagerController : Controller
    {
        // GET: TempleManager
        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public string Index(FormCollection collection)
        {
            //获取所有可以进行操作的元素
            Entities _Entities = new Entities();
            List<Tmplete> _lstTmplete = _Entities.Tmplete.ToList();

            //Tree _Tree = new Tree();
            //foreach (var item in _lstTmplete)
            //{
            //    _Tree.id = item.ID;
            //    _Tree.text = item.ParamKey;
            //    var m = (from u in _Entities.Tmplete where u.ParentID == item.ID select u);
            //    if (0 < m.Count())
            //    {

            //    }
            //}

            DatagridData<Tmplete> _DatagridDataResult = new DatagridData<Tmplete>();
            _DatagridDataResult.total = _lstTmplete.Count();
            _DatagridDataResult.rows = _lstTmplete;

            return JsonConvert.SerializeObject(_DatagridDataResult);
        }


        public struct DatagridData<T>
        {
            public int total { get; set; }
            public List<T> rows { get; set; }
        }


        public struct TreeGrid
        {
            public string idField;
            public string treeField;
            public List<column> columns;
        }

        public struct column
        {
            public string title;
            public string field;
            public int width;
            public string align;
        }

        public struct Tree
        {
            public Guid id { get; set; }
            public string text { get; set; }
            public string iconCls { get; set; }
            public string state { get; set; }

            public TreeChildAttributes attributes { get; set; }

            public List<Tree> children { get; set; }
        }

        public struct TreeChildAttributes
        {
            public string url { get; set; }
        }
    }
}