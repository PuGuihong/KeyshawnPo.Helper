
using Dao.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KeyshawnPo.Demo.HtmlFactoryWeb.Controllers
{
    public class ParamController : Controller
    {
        // GET: Param
        public ActionResult Index()
        {
            Class1 _c = new Class1();
            _c.Save();

            Entities _db = new Entities();
            List<Tmplete> _data = _db.Tmplete.ToList();

            return View(_data);
        }

        // GET: Param/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Param/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Param/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                Tmplete _Tmplete = new Tmplete();
                _Tmplete.ID = Guid.NewGuid();
                _Tmplete.ParamKey = collection.GetValue("ParamKey").AttemptedValue.ToString();
                _Tmplete.ParamValue = collection.GetValue("ParamValue").AttemptedValue.ToString();
                _Tmplete.ParamRemarks = collection.GetValue("ParamRemarks").AttemptedValue.ToString();
                _Tmplete.ParamVersion = 1;

                Entities _db = new Entities();
                _db.Tmplete.Add(_Tmplete);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Param/Edit/5
        public ActionResult Edit(string id)
        {
            Tmplete _Tmplete = new Tmplete();
            _Tmplete.ID = new Guid(id);

            Entities _db = new Entities();
            Tmplete _editEntity = _db.Tmplete.Find(_Tmplete.ID);
            return View(_editEntity);
        }

        // POST: Param/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            try
            {
                Entities _db = new Entities();
                Tmplete _Tmplete = _db.Tmplete.Find(new Guid(id));
                _Tmplete.ParamKey = collection.GetValue("ParamKey").AttemptedValue.ToString();
                _Tmplete.ParamValue = collection.GetValue("ParamValue").AttemptedValue.ToString();
                _Tmplete.ParamRemarks = collection.GetValue("ParamRemarks").AttemptedValue.ToString();
                _Tmplete.ParamVersion = 1;


                _db.SaveChanges();

                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Param/Delete/5
        public ActionResult Delete(string id)
        {
            return View();
        }

        // POST: Param/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                Tmplete _Tmplete = new Tmplete();
                _Tmplete.ID = new Guid(id);

                Entities _db = new Entities();
                Tmplete _deleEntity = _db.Tmplete.Find(_Tmplete.ID);
                _db.Tmplete.Remove(_deleEntity);
                _db.SaveChanges();

                // TODO: Add delete logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
