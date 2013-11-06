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
    public class EQControlLoopController : Controller
    {
        private BrewDBContext db = new BrewDBContext();

        //
        // GET: /EQControlLoop/

        public ActionResult Index()
        {
            var eqcontrolloops = db.EQControlLoops.Include(e => e.Plant).Include(e => e.EQType);
            return View(eqcontrolloops.ToList());
        }

        //
        // GET: /EQControlLoop/Details/5

        public ActionResult Details(int id = 0)
        {
            EQControlLoop eqcontrolloop = db.EQControlLoops.Find(id);
            if (eqcontrolloop == null)
            {
                return HttpNotFound();
            }
            return View(eqcontrolloop);
        }

        //
        // GET: /EQControlLoop/Create

        public ActionResult Create()
        {
            ViewBag.PlantID = new SelectList(db.Plants, "PlantID", "PlantName");
            ViewBag.EQTypeID = new SelectList(db.EQTypes, "EQTypeID", "TypeDescription");
            return View();
        }

        //
        // POST: /EQControlLoop/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EQControlLoop eqcontrolloop)
        {
            if (ModelState.IsValid)
            {
                db.EQControlLoops.Add(eqcontrolloop);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PlantID = new SelectList(db.Plants, "PlantID", "PlantName", eqcontrolloop.PlantID);
            ViewBag.EQTypeID = new SelectList(db.EQTypes, "EQTypeID", "TypeDescription", eqcontrolloop.EQTypeID);
            return View(eqcontrolloop);
        }

        //
        // GET: /EQControlLoop/Edit/5

        public ActionResult Edit(int id = 0)
        {
            EQControlLoop eqcontrolloop = db.EQControlLoops.Find(id);
            if (eqcontrolloop == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlantID = new SelectList(db.Plants, "PlantID", "PlantName", eqcontrolloop.PlantID);
            ViewBag.EQTypeID = new SelectList(db.EQTypes, "EQTypeID", "TypeDescription", eqcontrolloop.EQTypeID);
            return View(eqcontrolloop);
        }

        //
        // POST: /EQControlLoop/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EQControlLoop eqcontrolloop)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eqcontrolloop).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PlantID = new SelectList(db.Plants, "PlantID", "PlantName", eqcontrolloop.PlantID);
            ViewBag.EQTypeID = new SelectList(db.EQTypes, "EQTypeID", "TypeDescription", eqcontrolloop.EQTypeID);
            return View(eqcontrolloop);
        }

        //
        // GET: /EQControlLoop/Delete/5

        public ActionResult Delete(int id = 0)
        {
            EQControlLoop eqcontrolloop = db.EQControlLoops.Find(id);
            if (eqcontrolloop == null)
            {
                return HttpNotFound();
            }
            return View(eqcontrolloop);
        }

        //
        // POST: /EQControlLoop/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EQControlLoop eqcontrolloop = db.EQControlLoops.Find(id);
            db.EQControlLoops.Remove(eqcontrolloop);
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