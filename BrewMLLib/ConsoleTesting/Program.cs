using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using BrewMLLib;
using BrewMLLib.DAL;
using BrewMLLib.Models;

namespace ConsoleTesting
{
    class Program
    {
        static void Main(string[] args)
        {

            BrewML b = new BrewML();

            BrewDBContext db = new BrewDBContext();

            Plant p = new Plant();
            //p.PlantName = "jakes";

            //db.EQAddPlant(p);

            //db.Plants.Add(p);

            //db.SaveChanges();
            EQType t = new EQType();
            t.TypeDescription = "valve";
            db.EQTypes.Add(t);
            db.SaveChanges();
            PlantActions pd = new PlantActions();
            
            //pd.InPlant("jake").AddLoop(new ControlLoopActions().Create(pd).WithName("testloo").Final());

            pd.InPlant("jakes").AddLoop().WithName("testlooddd33p").AsType("valve").Final();


        }
    }
}
