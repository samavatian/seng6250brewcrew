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

            types.AddEQType("Pump").Final();
            types.AddEQType("Valve").Final();
            types.AddEQType("TempController").Final();

            Report.reportit();

            FluentPlant pd = new FluentPlant();

            pd.AddPlant("Little Jakes").Final();
            
            pd.ForPlant("Little Jakes")
                .HasLoops()
                .AddControlLoop("WaterPump","Pump")
                .SetSetPoint(215)
                .Final();

            //Console.WriteLine(pd.GetMessage());
            //Console.WriteLine(pd.ForPlant("Little Jakes").HasLoops("WaterInfeed").GetSetPoint());

            pd.ForPlant("Little Jakes")
                .HasLoops()
                .AddControlLoop("TempControl1", "TempController")
                .SetSetPoint(222)
                .Final();

            Report.reportit();

            //Console.WriteLine(pd.ForPlant("Little Jakes").HasLoops("TempControl1").GetSetPoint());


            pd.ForPlant("Little Jakes")
                .HasLoops("TempControl1")
                .SetSetPoint(678)
                .HasType("Valve")
                .Final();

            pd.ForPlant("Little Jakes").HasLoops().ForControlLoop("TempControl1").SetSetPoint(33).Final();

            //Console.WriteLine(pd.ForPlant("Little Jakes").HasLoops("TempControl1").GetSetPoint());

            pd.AddPlant("Big Jakes").Final();

            pd.ForPlant("Big Jakes")
                .HasLoops()
                .AddControlLoop("ValveXV1321", "Valve")
                .SetSetPoint(33)
                .Final()
                .HasLoops()
                .AddControlLoop("DumpValve12", "Valve")
                .SetSetPoint(123)
                .Final()
                .Final();


            //Console.WriteLine("-------------------");


            pd.ForPlant("Big Jakes")
                .HasLoops("ValveXV1321")
                .SetSetPoint(678)
                .HasType("Valve")
                .SetSetPoint(99)
                .Final();

            //Console.WriteLine(pd.ForPlant("Big Jakes").HasLoops("ValveXV1321").GetSetPoint());

            //Console.WriteLine(pd.ForPlant("Big Jakes").HasLoops("ValveXV1321").GetSetPoint());

            Console.WriteLine("-------------------");
            Console.WriteLine("Recipe Test ");


            FluentRecipe rec = new FluentRecipe();

            rec.AddRecipe("Big Eddy")
                .SetBrandDescription("hey this is big stuff")
                .SetQualityTarget("always high quality")
                .Final();

            rec.ForRecipe("Big Eddy")
                .HasRecOperations()
                .AddOperation("Heat 12")
                .SetSetPoint(33)
                .Final()
                .Final();


            rec.AddRecipe("So Smooth")
                .SetQualityTarget("top notch")
                .HasRecOperations()
                .AddOperation("Heat Phase 1")
                .SetSetPoint(170)
                .Final()
                .Final();

            rec.ForRecipe("So Smooth")
                .HasRecOperations()
                .AddOperation("heat phase 2")
                .SetSetPoint(33)
                .Final()
                .HasRecOperations()
                .AddOperation("Heat Phase 3")
                .SetSetPoint(34)
                .Final()
                .Final();
            
            rec.ForRecipe("So Smooth")
                .HasRecOperations()
                .AddOperation("cool phase 3")
                .SetSetPoint(123)
                .Final()
                .HasRecOperations()
                .AddOperation("cool phase 4")
                .SetSetPoint(155).Final().Final();



            Console.WriteLine("-------------------");

            Console.WriteLine("-------------------");

            Console.WriteLine("-------------------");


            Report.reportit();

            Console.WriteLine("hit key");
            var name = Console.ReadLine();

        }



    }

    public class WriteDb
    {

        BrewDBContext contx = new BrewDBContext();
        string line = "";

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
                Console.WriteLine(line);
                Console.WriteLine(line);
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

    
