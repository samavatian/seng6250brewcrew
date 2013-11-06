using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication2.Models;
using MvcApplication2.DAL;

namespace MvcApplication2.Controllers
{
    public class EQTypeController : Controller
    {
        private BrewDBContext db = new BrewDBContext();

        //
        // GET: /EQType/

        public ActionResult Index()
        {
            return View(db.EQTypes.ToList());
        }

        //
        // GET: /EQType/Details/5

        public ActionResult Details(int id = 0)
        {
            EQType eqtype = db.EQTypes.Find(id);
            if (eqtype == null)
            {
                return HttpNotFound();
            }
            return View(eqtype);
        }

        //
        // GET: /EQType/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /EQType/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EQType eqtype)
        {
            if (ModelState.IsValid)
            {
                db.EQTypes.Add(eqtype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(eqtype);
        }

        //
        // GET: /EQType/Edit/5

        public ActionResult Edit(int id = 0)
        {
            EQType eqtype = db.EQTypes.Find(id);
            if (eqtype == null)
            {
                return HttpNotFound();
            }
            return View(eqtype);
        }

        //
        // POST: /EQType/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EQType eqtype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eqtype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eqtype);
        }

        //
        // GET: /EQType/Delete/5

        public ActionResult Delete(int id = 0)
        {
            EQType eqtype = db.EQTypes.Find(id);
            if (eqtype == null)
            {
                return HttpNotFound();
            }
            return View(eqtype);
        }

        //
        // POST: /EQType/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EQType eqtype = db.EQTypes.Find(id);
            db.EQTypes.Remove(eqtype);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}