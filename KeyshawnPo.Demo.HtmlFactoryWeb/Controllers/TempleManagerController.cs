using Dao.EntityFramework;
using KeyshawnPo.HtmlEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

            Datagrid<Tmplete> _DatagridDataResult = new Datagrid<Tmplete>();
            _DatagridDataResult.total = _lstTmplete.Count();
            _DatagridDataResult.rows = _lstTmplete;

            return JsonConvert.SerializeObject(_DatagridDataResult);
        }

        [HttpPost]
        public string Tree_1(FormCollection collection)
        {
            string _strRlt = string.Empty;

            //获取所有可以进行操作的元素
            Entities _Entities = new Entities();
            List<Tmplete> _lstTmplete = _Entities.Tmplete.ToList();

            List<Tree> _lstTree = new List<Tree>();

            Tree _tree = new Tree();
            _tree.id = new Guid("872D82ED-9F82-4A81-87F9-74F35A369ABC");
            _tree = FooTree(_tree, _lstTmplete);
            _lstTree.Add(_tree);
            string s = JsonConvert.SerializeObject(_lstTree);

            return s;
        }

        [HttpPost]
        public string ComboBox_1(FormCollection collection)
        {

            string _strRlt = string.Empty;

            //获取所有可以进行操作的元素
            Entities _Entities = new Entities();
            List<Tmplete> _lstTmplete = _Entities.Tmplete.ToList();
            List<Combobox> _lstCombobox = new List<Combobox>();
            foreach (var item in _lstTmplete)
            {
                Combobox _Combobox = new Combobox();
                _Combobox.id = item.ID.ToString();
                _Combobox.text = item.ParamKey;
                _Combobox.desc = item.ParamRemarks;
                _lstCombobox.Add(_Combobox);
            }

            _strRlt = JsonConvert.SerializeObject(_lstCombobox);

            return _strRlt;
        }




        #region 通用递归树

        //递归获取节点和子节点
        //public Tree FooTree(Tree treeNode, List<Tmplete> dataSource)
        //{
        //    Tree _tree = new Tree();
        //    //子节点的原始数据
        //    List<Tmplete> _dataSource = new List<Tmplete>();
        //    _dataSource = dataSource.FindAll(o => o.ParentID == treeNode.id);

        //    _tree.id = treeNode.id;
        //    _tree.text = treeNode.text;
        //    _tree.children = new List<Tree>();
        //    //判断是否还有子节点
        //    //还有子节点
        //    if (_dataSource.Count > 0)
        //    {
        //        foreach (var item in _dataSource)
        //        {
        //            Tree _child = new Tree();
        //            _child.id = item.ID;
        //            _child.text = item.ParamValue;
        //            _child = FooTree(_child, dataSource);
        //            _tree.children.Add(_child);
        //        }
        //    }
        //    return _tree;
        //}


        private object GetValue<T>(List<T> entity, string value, object[] index)
        {

            Type _type = entity.GetType();
            PropertyInfo _prop = _type.GetProperty(value);
            return _prop.GetValue(entity, index);
        }


        //递归获取节点和子节点
        public Tree FooTree<T>(Tree treeNode, List<T> dataSource, string parentIdName = "ParentID", string idName = "ID", string textName = "ParamValue")
        {
            Tree _tree = new Tree();
            //子节点的原始数据
            List<T> _dataSource = new List<T>();
            _dataSource = dataSource.Where(o => o.GetType().GetProperty(parentIdName).GetValue(o, null).ToString() == treeNode.id.ToString()).ToList();

            _tree.id = treeNode.id;
            _tree.text = treeNode.text;
            _tree.children = new List<Tree>();
            //判断是否还有子节点
            //还有子节点
            if (_dataSource.Count > 0)
            {
                foreach (var item in _dataSource)
                {
                    Tree _child = new Tree();
                    _child.id = item.GetType().GetProperty(idName).GetValue(item, null);
                    _child.text = item.GetType().GetProperty(textName).GetValue(item, null);
                    _child = FooTree(_child, dataSource, parentIdName, idName, textName);
                    _tree.children.Add(_child);
                }
            }
            return _tree;
        }



        //通用 ParentId,Id,children 用了反射效率不高

        public void LoopToAppendChildren<T>(List<T> all, T curItem, string parentIdName = "ParentId", string idName = "Id", string childrenName = "children")
        {
            var subItems = all.Where(ee => ee.GetType().GetProperty(parentIdName).GetValue(ee, null).ToString() == curItem.GetType().GetProperty(idName).GetValue(curItem, null).ToString()).ToList(); //新闻1

            curItem.GetType().GetField(childrenName).SetValue(curItem, subItems);
            foreach (var subItem in subItems)
            {
                LoopToAppendChildren(all, subItem);//新闻1.1
            }
        }

        #endregion

    }
}