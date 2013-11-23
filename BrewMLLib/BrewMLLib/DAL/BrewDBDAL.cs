using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

///What I added
using System.Data.Entity;
//using BrewMLLib.Models;

//namespace BrewMLLib.DAL
namespace BrewMLLib
{
    class BrewDBDAL
    {
    }


    /// <summary>
    /// The EF class that creates the DB
    /// </summary>
    public class BrewDBContext : DbContext 
    {

        //public BrewDBContext() : base("DefaultConnection") { }
        public BrewDBContext()
            : base("DefaultConnection")
        {
            //this.Configuration.LazyLoadingEnabled = false;
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

        public DbSet<RecUnitOperation> RecUnitOperations { get; set; }
        public DbSet<Transition> Transitions { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<IngredientType> IngredientTypes { get; set; }

        public DbSet<TestEntitie> TestEntities { get; set; }
        public DbSet<TestEntitieTwo> TestEntitieTwos { get; set; }
        public DbSet<CollectionOfTestEntitie> CollectionOfTestEntities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Unit>().h
            //modelBuilder.Entity<Unit>().HasMany(e => e.EQControlLoops).WithRequired(e => e.Unit);


            modelBuilder.Entity<Plant>().HasMany(e => e.PlantLoops).WithRequired(g => g.Plant).WillCascadeOnDelete(false);
                
            modelBuilder.Entity<Plant>().HasMany(e => e.PlantAux).WithRequired(e => e.Plant).WillCascadeOnDelete(false);

            modelBuilder.Entity<Plant>().HasMany(e => e.PlantVessels).WithRequired(e => e.Plant).WillCascadeOnDelete(false);


            //modelBuilder.Entity<Plant>().HasMany(e=>e.ThisPlantsBrands).WithMany(e=>e.pla
            //modelBuilder.Entity<EQControlLoop>().w
            
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
            //Unit unit = new Unit();
            //unit = cont.CreateUnit(plant.PlantID, "default");
            //plant.Units.Add(unit);

            cont.Plants.Add(plant);
            cont.SaveChanges();
            Unit unit = new Unit();
            //plant = cont.GetPlantByPlantName(plant.PlantName);
            unit = cont.CreateUnit(plant.PlantID, "default");
            cont.Plants.FirstOrDefault(g => g.PlantID == plant.PlantID).Units.Add(unit);
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
        public Plant GetPlantByPlantName(string plantname)
        {
            BrewDBContext cont = new BrewDBContext();

            Plant p = new Plant();
            p = cont.Plants.FirstOrDefault(g => g.PlantName == plantname);
            return p;


        }


        public EQControlLoop GetLoopByPlantIDAndName(int PlantID, string EqName)
        {
            BrewDBContext contx = new BrewDBContext();
            var test = contx.EQControlLoops
                    .Where(b => b.Plant.PlantID == PlantID)
                    .Where(c => c.EquipName == EqName)
                    .FirstOrDefault();

            EQControlLoop _loop = test;

            return _loop;

        }

        //----------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------
        //          Plant
        //----------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------



        public EQControlLoop CreateNewLoop(string name, string type, Plant plant, Unit unit)
        {
            BrewDBContext contx = new BrewDBContext();
            EQControlLoop _loop = new EQControlLoop();
            //Plant plant = new Plant();

            _loop.AssetTag = "111";
            _loop.Description = "default";
            _loop.EnableTrending = false;
            _loop.EquipName = name;

            if (type == "")
            {

                _loop.EQType = contx.EQTypes.FirstOrDefault();

            }
            else
            {
                _loop.EQType = contx.EQTypes.FirstOrDefault(f => f.TypeDescription == type);
            }
            //_loop.Plant = _parent.GetPlant();
            _loop.Plant = contx.Plants.FirstOrDefault(e => e.PlantID == plant.PlantID);

            if (unit != null)
            {
                _loop.Unit = contx.Units.FirstOrDefault(e => e.UnitID == unit.UnitID);
            }
            else
            {
                _loop.Unit = _loop.Plant.Units.First();
            }
            //if (unit != null)
            //{
            //    _loop.Unit = contx.Units.FirstOrDefault(u => u.UnitID == unit.UnitID);

            //}
            contx.EQControlLoops.Add(_loop);
            contx.Plants.FirstOrDefault(l => l.PlantID == plant.PlantID).PlantLoops.Add(_loop);

            contx.SaveChanges();
            //contx.Plants.FirstOrDefault(g => g.PlantID == plant.PlantID).PlantLoops.Add(contx.EQControlLoops.FirstOrDefault(e => e.EquipName == _loop.BaseEquipID));
            //p.PlantLoops.Add(_loop);
            //contx.SaveChanges();

            //contx.Units.FirstOrDefault(u => u.UnitID == unit.UnitID).UnitLoops.Add(contx.EQControlLoops.FirstOrDefault(e => e.BaseEquipID == _loop.BaseEquipID));

            return _loop;
        }


        public List<EQControlLoop> AddEQControlLoopToPlant(int plantID, EQControlLoop loop)
        {


            BrewDBContext cont = new BrewDBContext();

            Plant plant = cont.Plants.FirstOrDefault(g => g.PlantID == plantID);

            plant.PlantLoops.Add(loop);

            cont.Plants.FirstOrDefault(g => g.PlantID == plantID).PlantLoops.Add(loop);
            cont.SaveChanges();

            return cont.EQControlLoops.SelectMany(g => EQControlLoops).Where(g => g.Plant.PlantID == plantID).ToList();

        }

        public EQControlLoop AddEQControlLoopToUnit(int unitID, string name)
        {


            BrewDBContext cont = new BrewDBContext();
            Unit u = new Unit();

            u = cont.Units.FirstOrDefault(g => g.UnitID == unitID);

            Plant plant = cont.Plants.FirstOrDefault(g => g.PlantID == u.Plant.PlantID);

            EQControlLoop loop = new EQControlLoop();

            loop = plant.PlantLoops.FirstOrDefault(g => g.EquipName == name);

            //plant.PlantLoops.Add(loop);

            cont.Units.FirstOrDefault(g => g.UnitID == unitID).UnitLoops.Add(loop);

            cont.SaveChanges();

            //return cont.EQControlLoops.SelectMany(g => EQControlLoops).Where(g => g.Plant.PlantID == plantID).ToList();
            return loop;

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
            List<EQControlLoop> loops = cont.Plants.SelectMany(g => g.PlantLoops).Where(g => g.Plant.PlantID == plantID).ToList();

            return loops;

        }


        public EQControlLoop GetEQControlLoopsByPlantByName(Plant plant, string name)
        {

            BrewDBContext cont = new BrewDBContext();

            //List<EQControlLoop> loops = cont.EQControlLoops.SelectMany(g => EQControlLoops).Where(g => g.Plant == plant).ToList();

            var loopq = from p in cont.EQControlLoops
                        where (p.EquipName == name) && (p.Plant.PlantID == plant.PlantID)
                        select p;
                      

            EQControlLoop loop = new EQControlLoop();
            loop = loopq.FirstOrDefault();
            return loop;
             

        }

        public Unit CreateUnit(int PlantID, string unitname)
        {
            BrewDBContext contx = new BrewDBContext();
            //Plant p = _parent.GetPlant();
            Unit unit = new Unit();
            Plant p = new Plant();
            p = contx.Plants.FirstOrDefault(s => s.PlantID == PlantID);
            unit = contx.GetUnitByPlantByPlantID(PlantID, unitname);

            //unit = contx.Units.FirstOrDefault(l => l.UnitName == UnitName);
            //_plant = contx.Plants.FirstOrDefault(p => p.PlantName == PlantName);

            //_isnew = false;
            if (unit == null & p != null)
            {



                unit = contx.Units.Create();

                //if (_plant != null)
//                unit.Plant = contx.Plants.FirstOrDefault(h => h.PlantID == p.PlantID);
                unit.Plant = p;
                unit.UnitName = unitname;
                unit.AvailableEQOperations = new List<EQUnitOperation>();
                unit.InputUnits = new List<Unit>();
                unit.OutputUnits = new List<Unit>();
                unit.UnitAux = new List<EQAuxilary>();
                unit.UnitLoops = new List<EQControlLoop>();
                unit.UnitVessels = new List<EQVessel>();

//                contx.Units.Add(unit);

                contx.Plants.FirstOrDefault(g => g.PlantID == p.PlantID).Units.Add(unit);


                contx.SaveChanges();
                //if (_plant != null)
                //contx.Plants.FirstOrDefault(g => g.PlantID == unit.Plant.PlantID).Units.Add(contx.Units.FirstOrDefault(e => e.UnitName == unit.UnitName));
                //contx.SaveChanges();

            }
            //else
            //{
            //    if (_plant != null)
            //        _unit.Plant = contx.Plants.FirstOrDefault(h => h.PlantID == _plant.PlantID);

            //    if (_plant != null)
            //        contx.Plants.FirstOrDefault(g => g.PlantID == _unit.Plant.PlantID).Units.Add(contx.Units.FirstOrDefault(e => e.UnitName == _unit.UnitName));

            //    contx.SaveChanges();

            //}
            contx.SaveChanges();
            unit = contx.Units.FirstOrDefault(l => l.UnitName == unitname);
            return unit;

        }



        //----------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------
        //          Recipe

        //----------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------


        public MasterRecipe CreateMasterRecipe(string Name)
        {

            BrewDBContext cont = new BrewDBContext();

            MasterRecipe _recipe = new MasterRecipe();
            MasterRecipe recip = cont.MasterRecipes.FirstOrDefault(g => g.BrandName == Name);

            if (recip == null)
            {
                _recipe = cont.MasterRecipes.Create();


                _recipe.BrandDescription = "default";
                _recipe.BrandName = Name;
                _recipe.Ingredients = new List<Ingredient>();
                _recipe.RecOperations = new List<RecUnitOperation>();
                _recipe.QaulityTargets = "default";

                _recipe.Plants = new List<Plant>();

                cont.MasterRecipes.Add(_recipe);
                cont.SaveChanges();
                recip = cont.MasterRecipes.FirstOrDefault(g => g.MasterRecipeID == _recipe.MasterRecipeID);

            }
            return recip;


        }


        public RecUnitOperation AddRecUnitOperation(MasterRecipe recx, string Name)
        {

            BrewDBContext cont = new BrewDBContext();
            MasterRecipe rec = cont.MasterRecipes.FirstOrDefault(g=>g.MasterRecipeID == recx.MasterRecipeID);
            RecUnitOperation operation = cont.RecUnitOperations.Create();


            operation = rec.RecOperations.FirstOrDefault(g => g.OperationName == Name);



            if (operation == null)
            {


                operation = cont.RecUnitOperations.Create();

                operation.OperationName = Name;
                operation.SetPoint = 0;
                operation.Transitions = new List<Transition>();
                operation.AllowedUnits = new List<Unit>();

                cont.MasterRecipes.FirstOrDefault(g => g.MasterRecipeID == recx.MasterRecipeID).RecOperations.Add(operation);

                //cont.RecUnitOperations.Add(operation);
                cont.SaveChanges();

                //cont.MasterRecipes.FirstOrDefault(f => f.BrandName == rec.BrandName).RecOperations.Add(_operation);

                //cont.SaveChanges();

            }
            //else
            //{


            //}


            return operation;


        }


        public RecUnitOperation AddUnitToRecUnitOperation(RecUnitOperation op, string Name)
        {
            BrewDBContext contx = new BrewDBContext();

            Unit u = contx.Units.FirstOrDefault(s => s.UnitName == Name);
            RecUnitOperation opx = contx.RecUnitOperations.FirstOrDefault(g => g.RecUnitOperationID == op.RecUnitOperationID);

            if (u != null)
            {
                contx.RecUnitOperations.FirstOrDefault(g => g.RecUnitOperationID == op.RecUnitOperationID).AllowedUnits.Add(u);

                contx.SaveChanges();


            }
            return opx;

        }


        public Transition AddTransition(RecUnitOperation op, string Name)
        {

            BrewDBContext cont = new BrewDBContext();
            //MasterRecipe rec = cont.MasterRecipes.FirstOrDefault(g => g.MasterRecipeID == MasterRecipeID);
            RecUnitOperation operation = new RecUnitOperation();
            Transition tran = new Transition();

            tran = cont.RecUnitOperations.FirstOrDefault(i => i.RecUnitOperationID == op.RecUnitOperationID).Transitions.FirstOrDefault(s => s.TransitionName == Name);

            return tran;
        }
        public List<MasterRecipe> AddMasterRecipeToPlant(int plantID, int masterRecipeID)
        {

            BrewDBContext cont = new BrewDBContext();

            MasterRecipe rec = cont.MasterRecipes.SingleOrDefault(g => g.MasterRecipeID == masterRecipeID);

            cont.Plants.SingleOrDefault(g => g.PlantID == plantID).ThisPlantsBrands.Add(rec);
            cont.SaveChanges();

            List<MasterRecipe> recList = cont.Plants.SelectMany(g => g.ThisPlantsBrands).ToList();



            return recList;

        }

        public List<MasterRecipe> GetMasterRecipeByPlant(int plantID)
        {
            BrewDBContext cont = new BrewDBContext();
            List<MasterRecipe> recList = cont.Plants.SelectMany(g => g.ThisPlantsBrands).ToList();
            return recList;
        }



        //----------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------
        //          Brands  - Master Recipe
        //----------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------



        public Unit GetUnitByPlantByName(string plantname, string unitname)
        {

            BrewDBContext contx = new BrewDBContext();

            var unit = from u in Units 
                       where (u.UnitName ==  unitname) && (u.Plant.PlantName == plantname)
                       select u;

            Unit uu = unit.FirstOrDefault();

            return uu;


        }
        public Unit GetUnitByPlantByPlantID(int plantid, string unitname)
        {

            BrewDBContext contx = new BrewDBContext();

            var unit = from u in Units
                       where (u.UnitName == unitname) && (u.Plant.PlantID == plantid)
                       select u;

            Unit uu = unit.FirstOrDefault();

            return uu;


        }
        //public Plant GetPlantByUnitID(int unitID)
        //{
        //    BrewDBContext contx = new BrewDBContext();

        //    Plant p = new Plant();
                        


        //}

    }
}

