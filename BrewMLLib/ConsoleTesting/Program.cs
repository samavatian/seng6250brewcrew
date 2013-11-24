using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using BrewMLLib;
//using BrewMLLib.DAL;
//using BrewMLLib.Models;

namespace ConsoleTesting
{
    class Program
    {

        /// <summary>
        /// Test
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            BrewML b = new BrewML();

            BrewDBContext db = new BrewDBContext();

            WriteDb Report = new WriteDb();

            Plant p = new Plant();


            FluentEQType types = new FluentEQType();

            //types.AddEQType("Pump").Final();
            //types.AddEQType("Valve").Final();
            //types.AddEQType("TempController").Final();
            Console.WriteLine("---------------------------------------------");


            types.AddEQType("Pump");
            types.AddEQType("Valve");
            types.AddEQType("TempController");

            Report.reportit();
            Console.WriteLine("---------------------------------------------");

            FluentPlant pd = new FluentPlant();

//            pd.AddPlant("Little Jakes").Final();
            pd.AddPlant("Little Jakes");
 

            pd.ForPlant("Little Jakes")
                .HasLoops()
                .AddControlLoop("WaterPump","Pump")
                .SetSetPoint(215);
//                .Final();

            //Console.WriteLine(pd.GetMessage());
            //Console.WriteLine(pd.ForPlant("Little Jakes").HasLoops("WaterInfeed").GetSetPoint());

            pd.ForPlant("Little Jakes")
                .HasLoops()
                .AddControlLoop("TempControl1", "TempController")
                .SetSetPoint(222);
//                .Final();

            Report.reportit();

            //Console.WriteLine(pd.ForPlant("Little Jakes").HasLoops("TempControl1").GetSetPoint());


            pd.ForPlant("Little Jakes")
                .HasLoops("TempControl1")
                .SetSetPoint(678)
                .HasType("Valve");
//                .Final();

            pd.ForPlant("Little Jakes")
               .HasLoops()
               .AddControlLoop("TempControl122", "TempController")
               .SetSetPoint(678)
               .HasType("Valve"); //// WHAT IS THIS?
               //.Final();

            pd.ForPlant("Little Jakes").HasLoops().ForControlLoop("TempControl1").SetSetPoint(353);

            //Console.WriteLine(pd.ForPlant("Little Jakes").HasLoops("TempControl1").GetSetPoint());

//            pd.AddPlant("Big Jakes").Final();
            pd.AddPlant("Big Jakes");

            pd.ForPlant("Big Jakes")
                .HasLoops()
                .AddControlLoop("ValveXV1321", "Valve")
                .SetSetPoint(33);
//                .Final();

                pd.ForPlant("Big Jakes")
                .HasLoops()
                .AddControlLoop("DumpValve12", "Valve")
                .SetSetPoint(123);
//                .Final();
                //.Final();


            //Console.WriteLine("-------------------");


            pd.ForPlant("Big Jakes")
                .HasLoops("ValveXV1321")
                .SetSetPoint(678)
                .HasType("Valve")
                .SetSetPoint(99);
//                .Final();


            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("-------adding units--------------------------------------");


            FluentUnit fu = new FluentUnit();


            Report.reportUnits();

            //fu.AddUnit("test unit 1","Big Jakes");

            pd.ForPlant("Big Jakes").HasUnits().AddUnit("test unit 1");

            Report.reportUnits();
            pd.ForPlant("Little Jakes").HasUnits().AddUnit("test unit 1");
            Report.reportUnits(); 
            pd.ForPlant("Big Jakes").HasUnits().AddUnit("test unit 1");
            Report.reportUnits();
            //pd.ForPlant("Little Jakes").HasLoopsInUnit("test unit 1").AddControlLoop("ValveXV1321");

            //fu.ForUnit("test unit 1").HasLoops().AddControlLoop("ValveXV1321");

            pd.ForPlant("Little Jakes").HasUnits().AddUnit("asdfasdf").HasLoops().AddControlLoop("ValveXV1321");

            pd.ForPlant("Big Jakes").HasUnits().AddUnit("asdfaddsdf").HasLoops().AddControlLoop("ValveXV1321x","Valve");

            pd.ForPlant("Big Jakes").HasUnits().AddUnit("again").HasLoops().AddControlLoop("ValveXV1321xxx", "Valve");
            //Console.WriteLine(pd.ForPlant("Big Jakes").HasLoops("ValveXV1321").GetSetPoint());

            //Console.WriteLine(pd.ForPlant("Big Jakes").HasLoops("ValveXV1321").GetSetPoint());
            Report.reportUnits();
            Console.WriteLine("---------------------------------------------");
            
            Console.WriteLine("Recipe Test ");


            FluentRecipe rec = new FluentRecipe();

            rec.AddRecipe("Big Eddy")
                .SetBrandDescription("hey this is big stuff")
                .SetQualityTarget("always high quality");
//                .Final();

            rec.ForRecipe("Big Eddy")
                .HasRecOperations()
                .AddOperation("Heat 12")
                .SetSetPoint(33);
//                .Final()
 //               .Final();


            rec.AddRecipe("So Smooth")
                .SetQualityTarget("top notch")
                .HasRecOperations()
                .AddOperation("Heat Phase 1")
                .SetSetPoint(170);
                //.Final()
                //.Final();

            rec.ForRecipe("So Smooth")
                .HasRecOperations()
                .AddOperation("heat phase 2")
                .SetSetPoint(33)
                //.HasRecOperations()
                .AddOperation("Heat Phase 3")
                .SetSetPoint(34)
                .AddOperation("test chain 3")
                .SetSetPoint(55);
                
                //.Final()
                //.Final();
            
            rec.ForRecipe("So Smooth")
                .HasRecOperations()
                .AddOperation("cool phase 3")
                .SetSetPoint(123)
                //.HasRecOperations()
                .AddOperation("cool phase 4")
                .SetSetPoint(155);
            //.Final().Final();
            Console.WriteLine("-------------------");
            Report.reportMasterRecipes();
            rec.ForRecipe("So Smooth").HasRecOperations().AddAllowedUnits("again");

            Console.WriteLine("-------------------"); 
            Report.reportMasterRecipes();
            rec.ForRecipe("So Smooth").HasRecOperations("cool phase 3").AddAllowedUnits("asdfasdf").AddAllowedUnits("asdfaddsdf");

            pd.ForPlant("Big Jakes").HasUnits().AddUnit("test2ss3Unit");
            rec.ForRecipe("So Smooth").HasRecOperations("cool phase 3").AddAllowedUnits("test2ss3Unit");

            rec.ForRecipe("So Smooth").HasRecOperations("cool phase 3").AddAllowedUnits("test unit 1").AddAllowedUnits("asdfaddsdf");
            Console.WriteLine("-------------------");
            Report.reportMasterRecipes();
            rec.ForRecipe("So Smooth").HasRecOperations().AddAllowedUnits("test unit 1");

            
            rec.ForRecipe("Big Eddy")
               .HasRecOperations()
               .AddOperation("Heat 33")
               .SetSetPoint(33);
            Console.WriteLine("-------------------");
            Report.reportMasterRecipes();
            Console.WriteLine("-------------------");

            Console.WriteLine("-------------------");

            Console.WriteLine("-------------------");


            Report.reportit();


            Report.reportPlants();
            Console.WriteLine("-------------------");

            Console.WriteLine("-------------------");

            Console.WriteLine("-------------------");
            Report.reportMasterRecipes();
            Console.WriteLine("hit key");
            var name = Console.ReadLine();

        }



    }

    public class WriteDb
    {

        BrewDBContext contx = new BrewDBContext();
        string line = "";

        
        
        public void reportPlants()
        {
                    
            line = " ";

            Console.WriteLine(line);
            foreach (Plant p in contx.Plants.ToList())
            {
                line = "plant-----" + p.PlantName + "------------------------";
                Console.WriteLine(line);
                reportUnits(p);
                
            }

        }


        public void reportUnits(Plant p)
        {

            foreach (Unit u in contx.Plants.SelectMany(g => g.Units).Where(g => g.PlantID == p.PlantID))
            {
                line = "unit--------" + u.UnitName + "   ";
                Console.WriteLine(line);
                reportControlLoops(u);
            }


        }

        public void reportControlLoops(Unit u)
        {
            foreach (EQControlLoop l in contx.Units.SelectMany(g => g.UnitLoops).Where(g => g.UnitID == u.UnitID))
            {
                line = "loop-----------" +l.EquipName + "   SP:" + l.SetPoint.ToString();
                Console.WriteLine(line);
            }

        }

        public void reportMasterRecipes()
        {


//            List<MasterRecipe> rr = new List<MasterRecipe>();

            //rr = contx.MasterRecipes.ToList();


            foreach (MasterRecipe r in contx.MasterRecipes.ToList())
            {

                line = "Brand-----------" + r.BrandName + "   ";
                Console.WriteLine(line);
                reportUnitOperation(r);
               
            }



        }

        public void reportMasterRecipe(Plant p)
        {
            List<MasterRecipe> rr = new List<MasterRecipe>();

            rr = contx.Plants.SelectMany(g => g.ThisPlantsBrands).ToList();

            foreach (MasterRecipe r in rr)
            {
                line = "Brand-----------" + r.BrandName + "   ";
                Console.WriteLine(line);
                reportUnitOperation(r);

            }

        }
        public void reportUnitOperation(MasterRecipe r)
        {
            //            foreach (RecUnitOperation ru in contx.MasterRecipes.SelectMany(g => g.RecOperations).ToList())
//            foreach (RecUnitOperation ru in r.RecOperations.ToList())

            List<RecUnitOperation> list = new List<RecUnitOperation>();
            List<Unit> lunit = new List<Unit>();
            
            //list = contx.MasterRecipes.FirstOrDefault(s => s.MasterRecipeID == r.MasterRecipeID).RecOperations.ToList();

            foreach (RecUnitOperation ru in contx.MasterRecipes.FirstOrDefault(s => s.MasterRecipeID == r.MasterRecipeID).RecOperations.ToList()) 
            {
                
                line = "Operation-----------" + ru.OperationName + "   ";
                Console.WriteLine(line);
                lunit = contx.RecUnitOperations.FirstOrDefault(g => g.RecUnitOperationID == ru.RecUnitOperationID).AllowedUnits.ToList();
                
                reportUnits(lunit);
            }

        }

        public void reportUnits(List<Unit> units)
        {

            foreach (Unit ru in units)
            {   
                line = "aUnits--------------------" + ru.UnitName + "   ";
                Console.WriteLine(line);


            }
        }

        public void reportUnits()
        {
            line = "Report Units In Plants";
            Console.WriteLine(line);
            foreach (Plant p in contx.Plants.ToList())
            {

                line = " ";
                Console.WriteLine(line);
                line = "---" + p.PlantName + "------------------------";
                Console.WriteLine(line);
                foreach (Unit u in contx.Plants.SelectMany(g=>g.Units).Where(g=>g.PlantID == p.PlantID))
                {
                    line = "  " + u.UnitName + "   ";
                    Console.WriteLine(line);
                }

            }
        }

        public void reportit()
        {


            line = "--------------------Equipment Types------------------------";
            Console.WriteLine(line);

            foreach (EQType type in contx.EQTypes)
            {
                line = type.TypeDescription;
                Console.WriteLine(line);
            }

            line = " ";
            Console.WriteLine(line);
            Console.WriteLine(line);
            Console.WriteLine(line);

            line = "--------------------Plants------------------------";
            Console.WriteLine(line);

            foreach (Plant p in contx.Plants.ToList<Plant>())
            {

                line = " ";
                //Console.WriteLine(line);
                //Console.WriteLine(line);
                Console.WriteLine(line);
                line = "--------------------" + p.PlantName + "------------------------";
                Console.WriteLine(line);

                line = "Name:::"+p.PlantName + " :: " +"Menu::"+ p.BrewPubMenu + " :: " + "Address:" + p.PlantAddress + " :: " +"Location::" + p.PlantLocation + " :: ";
                Console.WriteLine(line);



                line = "--------------------Control Loops------------------------";
                Console.WriteLine(line);
                foreach (EQControlLoop l in p.PlantLoops)
                {
                    line = "Name::" +  l.EquipName + " :: " +"EQType" + l.EQType.TypeDescription + " :: " + " Set Point::"  + l.SetPoint.ToString();
                    Console.WriteLine(line);
                }


            }


            Console.WriteLine(line);
            Console.WriteLine(line);
            Console.WriteLine(line);

            line = "--------------------Recipes------------------------";
            Console.WriteLine(line);
            line = " ";
            Console.WriteLine(line);

            foreach (MasterRecipe rec in contx.MasterRecipes.ToList<MasterRecipe>())
            {

                line = " ";
                Console.WriteLine(line);
                line = "--------------------" + rec.BrandName + "------------------------";
                Console.WriteLine(line);
               

                line = "Name:::" + rec.BrandName + " :: " + "Desc::" + rec.BrandDescription + " :: " + "Quality:" + rec.QaulityTargets + " :: ";
                Console.WriteLine(line);


                line = "--------------------Operations------------------------";
                Console.WriteLine(line);
                foreach (RecUnitOperation op in rec.RecOperations)
                {

                    line = "Name:::" + op.OperationName + " :: " + "SP::" + op.SetPoint.ToString() + " :: ";
                    Console.WriteLine(line);


                }



            }



        }


    }

}

    
