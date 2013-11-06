using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

///What I added
using System.Data.Entity;
using MvcApplication2.Models;

namespace MvcApplication2.DAL
{
    public class BrewDBContext : DbContext 
    {

        //public BrewDBContext() : base("DefaultConnection") { }
        public BrewDBContext()
            : base("DefaultConnection")
        {

            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<BrewDBContext>());

        }
        
        public DbSet<EQType> EQTypes { get; set; }
        public DbSet<EQControlLoop> EQControlLoops { get; set; }
        public DbSet<EQAuxilary> EQAuxilarys { get; set; }         
        public DbSet<EQVessel> EQVessels { get; set; }

        public DbSet<Unit> Units { get; set; }
        public DbSet<EQUnitOperation> EQUnitOperations { get; set; }

        public DbSet<Plant> Plants { get; set; }

        public DbSet<Batch> Batchs { get; set; }
        public DbSet<MasterRecipe> MasterRecipes { get; set; }

        public DbSet<RecOperation> RecOperations { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<IngredientType> IngredientTypes { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Unit>().h
            //modelBuilder.Entity<Unit>().HasMany(e => e.EQControlLoops).WithRequired(e => e.Unit);


            modelBuilder.Entity<Plant>().HasMany(e => e.PlantLoops).WithRequired(g => g.Plant);
                
            modelBuilder.Entity<Plant>().HasMany(e => e.PlantAux).WithRequired(e => e.Plant);

            modelBuilder.Entity<Plant>().HasMany(e => e.PlantVessels).WithRequired(e => e.Plant);





        }


        //----------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------
        //          Equipment
        //----------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------

        public Plant EQAddPlant(Plant newplant)
        {

            BrewDBContext cont = new BrewDBContext();

            Plant plant = new Plant();
            plant.PlantLoops = new List<EQControlLoop>();
            plant.PlantAux = new List<EQAuxilary>();
            plant.PlantVessels = new List<EQVessel>();
            plant.BrewPubMenu = newplant.BrewPubMenu;
            plant.PlantAddress = newplant.PlantAddress;
            plant.PlantLocation = newplant.PlantLocation;
            plant.PlantName = newplant.PlantName;
            plant.Units = new List<Unit>();

            cont.Plants.Add(plant);
            cont.SaveChanges();

            Plant plantreturn = cont.Plants.FirstOrDefault(e => e.PlantID == plant.PlantID);

            return plantreturn;

        }

        /// <summary>
        ///  eventually do this by person id or user account 
        /// </summary>
        /// <returns></returns>
        public List<Plant> GetPlants()
        {

            BrewDBContext cont = new BrewDBContext();

            List<Plant> plants = cont.Plants.ToList();


            return plants;
        }

        public Plant GetPlantByPlantID(int plantID)
        {
            BrewDBContext cont = new BrewDBContext();

            Plant p = new Plant();
            p = cont.Plants.FirstOrDefault(g => g.PlantID == plantID);
            return p;


        }


        //----------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------
        //          Equipment
        //----------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------



        public List<EQControlLoop> AddEQControlLoopToPlant(int plantID, EQControlLoop loop)
        {


            BrewDBContext cont = new BrewDBContext();

            Plant plant = cont.Plants.FirstOrDefault(g => g.PlantID == plantID);

            plant.PlantLoops.Add(loop);

            cont.Plants.FirstOrDefault(g => g.PlantID == plantID).PlantLoops.Add(loop);
            cont.SaveChanges();

            return cont.EQControlLoops.SelectMany(g => EQControlLoops).Where(g => g.Plant.PlantID == plantID).ToList();

        }

        public List<EQControlLoop> GetEQControlLoopsByPlant(Plant plant)
        {

            BrewDBContext cont = new BrewDBContext();

            List<EQControlLoop> loops = cont.EQControlLoops.SelectMany(g=>EQControlLoops).Where(g=>g.Plant == plant).ToList();

            return loops;

        }


        public List<EQControlLoop> GetEQControlLoopsByPlantID(int plantID)
        {
            BrewDBContext cont = new BrewDBContext();
            List<EQControlLoop> loops = cont.Plants.SelectMany(g => g.PlantLoops).Where(g => g.PlantID == plantID).ToList();

            return loops;

        }

    }
}