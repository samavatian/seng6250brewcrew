using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication2.Models;
using MvcApplication2.DAL;

using MvcApplication2.ViewModel;

namespace MvcApplication2.Controllers
{
    public class PlantController : Controller
    {
        private BrewDBContext db = new BrewDBContext();

        //
        // GET: /Plant/

        public ActionResult Index()
        {
            //return View(db.Plants.ToList());
            ViewModel.ViewModelPlants plants = new ViewModelPlants();
            plants.Plants = db.GetPlants();
            return View(plants);

//            return View(db.GetPlants());

        }


        public ActionResult IndexOfEQControlLoops(Plant plant)
        {
            ViewModel.ViewModelEQControlLoops loops = new ViewModelEQControlLoops();
            loops.Loops = db.GetEQControlLoopsByPlant(plant);

            return View(loops);

        }



        //
        // GET: /Plant/Details/5

        //public ActionResult Details(int id = 0)
        //{
        //    Plant plant = db.Plants.Find(id);
        //    if (plant == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(plant);
        //}


        public ActionResult Details(int id = 0)
        {
            ViewModel.ViewPlantDetails details = new ViewPlantDetails();

            details.Plant = db.GetPlantByPlantID(id);
            details.EQControlLoops = db.GetEQControlLoopsByPlantID(id);

            return View(details);
        }


        //
        // GET: /Plant/Create

        public ActionResult Create()
        {

            return View();
        }

        //
        // POST: /Plant/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Plant plant)
        {
            if (ModelState.IsValid)
            {
//jac                db.Plants.Add(plant);
//jac                db.SaveChanges();
                plant = db.EQAddPlant(plant);
                return RedirectToAction("Index");
            }

            return View(plant);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEQControlLoopToPlant(int plantID, EQControlLoop loop)
        {

            if (ModelState.IsValid)
            {
                db.AddEQControlLoopToPlant(plantID, loop);

                return RedirectToAction("Index");
            }
            return View(loop);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEQControlLoopToPlant(int plantID)
        {

           
            return View();
        }


        //
        // GET: /Plant/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Plant plant = db.Plants.Find(id);
            if (plant == null)
            {
                return HttpNotFound();
            }
            return View(plant);
        }

        //
        // POST: /Plant/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Plant plant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(plant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(plant);
        }

        //
        // GET: /Plant/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Plant plant = db.Plants.Find(id);
            if (plant == null)
            {
                return HttpNotFound();
            }
            return View(plant);
        }

        //
        // POST: /Plant/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Plant plant = db.Plants.Find(id);
            db.Plants.Remove(plant);
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