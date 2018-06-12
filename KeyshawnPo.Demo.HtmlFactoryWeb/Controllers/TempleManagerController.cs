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
            //获取所有可以进行操作的元素
            Entities _Entities = new Entities();
            List<Tmplete> _lstTmplete = _Entities.Tmplete.ToList();

            TreeGrid _TreeGrid = new TreeGrid();
            _TreeGrid.idField = "ID";
            _TreeGrid.treeField = "ParamValue";
            _TreeGrid.columns = new List<column>();

            column _column = new column();
            foreach (var item in _lstTmplete)
            {
                _column.title = item.ParamKey;
                _column.field = item.ParamValue;
                _column.width = 10;
                _column.align = "center";
                _TreeGrid.columns.Add(_column);
            }
            ViewBag.TreeGridData = MvcHtmlString.Create(JsonConvert.SerializeObject(_TreeGrid.columns));

            return View();
        }
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
}