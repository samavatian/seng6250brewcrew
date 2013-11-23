using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

  ///added by me
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

//using BrewMLLib.DAL;

//namespace BrewMLLib.Models
namespace BrewMLLib
{
    class BrewModel
    {
    }





    public class BaseEquip
    {

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        public int BaseEquipID { get; set; }

        public string EquipName { get; set; }
        public string AssetTag { get; set; }
        public string Description { get; set; }


        //public virtual EQType Type { get; set; }
        //[ForeignKey("EQType")]
        //public int EQTypeID { get; set; }


        //Make all equipment be assigned to a Plant
        // which can then be re-configured to different units
        //[ForeignKey("PlantID")]

    }

    /// <summary>
    /// basic enum type table - tank, pump, valve, probe
    /// </summary>
    public class EQType
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        public int EQTypeID { get; set; }

        public string TypeDescription { get; set; }



    }

    public partial class EQControlLoop : BaseEquip
    {
        public string TagName { get; set; }

        public float? MeasuredValue { get; set; }
        public float? SetPoint { get; set; }
        public float? Output { get; set; }

        public float? HIHIAlarm { get; set; }
        public float? HIAlarm { get; set; }
        public float? LOAlarm { get; set; }
        public float? LOLOAlarm { get; set; }

        public bool? ReverseActing { get; set; }


        /// <summary>
        /// sim values
        /// </summary>
        public float? SimMeasValue { get; set; }

        /// plus ton of additional fields

        public bool EnableTrending { get; set; }

        public void DoSomething(int blah) { }

        public int PlantID { get; set; }
        public virtual Plant Plant { get; set; }
        public int UnitID { get; set; }
        public virtual Unit Unit { get; set; }

        //public virtual Plant Plant { get; set; }

        //public virtual Unit Unit { get; set; }
        //james - nov 17 see if we can just work with entity types and not they keys
        //[Required]
        //[ForeignKey("Plant")]
        //public int PlantID { get; set; }

        //[Required]
        //[ForeignKey("Unit")]
        //public int UnitID { get; set; }

        //public EQType EQType { get; set; }
        //james - nov 17 see if we can just work with entity types and not they keys
        [ForeignKey("EQType")]
        public int EQTypeID { get; set; }
        public virtual EQType EQType { get; set; }

    }
    /// <summary>
    /// Auxilary Equipment pumps mills
    /// These will have extra commands like on/off 
    /// </summary>
    public class EQAuxilary : BaseEquip
    {
        public int OnOff { get; set; }
        public int Running { get; set; }

        [Required]
        [ForeignKey("Plant")]
        public int PlantID { get; set; }
        public virtual Plant Plant { get; set; }
       

        //[Required]
        [ForeignKey("Unit")]
        public int UnitID { get; set; }
        public virtual Unit Unit { get; set; }

        [ForeignKey("EQType")]
        public int EQTypeID { get; set; }
        public virtual EQType EQType { get; set; }
        
    }

    public class EQVessel : BaseEquip
    {
        public string Name { get; set; }

        public float MaxVolume { get; set; }
        public float MinVolume { get; set; }
        public float MaxTemp { get; set; }

        [Required]
        [ForeignKey("Plant")]
        public int PlantID { get; set; }
        public virtual Plant Plant { get; set; }

        [ForeignKey("Unit")]
        public int UnitID { get; set; }
        public virtual Unit Unit { get; set; }


        [ForeignKey("EQType")]
        public int EQTypeID { get; set; }
        public virtual EQType EQType { get; set; }
       

    }

    /// <summary>
    /// Group of equipment to form a functioning unit of pumps
    /// and control valves
    /// </summary>
    public class Unit
    {

        //[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        //[Key]
        public int UnitID { get; set; }

        public string UnitName { get; set; }

        /// <summary>
        /// potential units that can feed this unit
        /// </summary>
        public ICollection<Unit> InputUnits { get; set; }
        /// <summary>
        /// potential units that output of this unit can feed
        /// </summary>
        //[InverseProperty("UnitID")]
        public virtual ICollection<Unit> OutputUnits { get; set; }
        //[InverseProperty("UnitOperationID")]
        public ICollection<EQUnitOperation> AvailableEQOperations { get; set; }

        //[InverseProperty]
        //[InverseProperty("BaseEquipID")]
        public virtual ICollection<EQControlLoop> UnitLoops { get; set; }
        public virtual ICollection<EQAuxilary> UnitAux { get; set; }
        public virtual ICollection<EQVessel> UnitVessels { get; set; }

        public Batch CurrentBatch { get; set; }

        public EQUnitOperation CurrentEQOperation { get; set; }

        //        public void AddLoop(EQControlLoop loop) { EQControlLoops.Add(loop); }

        [ForeignKey("Plant")]
        public int PlantID { get; set; }
        public virtual Plant Plant { get; set; }
        //[Required]


        public virtual ICollection<RecUnitOperation> RecUnitOperations { get; set; }

    }

    /// <summary>
    /// List of all operations that can occure on these units
    /// Heating, Pumping, etc... these usually take the coordinated effort of multiple equipment 
    /// a concerted effort amongs pumps, valves, controllers.
    /// Can be defined for multiple units - aka  4 brew kettles can all heat, they all have a heat operation
    /// </summary>
    public partial class EQUnitOperation
    {

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        public int UnitOperationID { get; set; }
        public string NameOfOperation { get; set; }

        //public string WhatTheEquipmentCanDo { get; set; }


        public virtual ICollection<Unit> UnitsThisOperationCanRunOn { get; set; }


    }



    /// <summary>
    /// my pub
    /// </summary>
    public class Plant
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int PlantID { get; set; }
        public string PlantName { get; set; }
        public string PlantLocation { get; set; }
        public string PlantAddress { get; set; }
        public string BrewPubMenu { get; set; }

        public virtual ICollection<EQControlLoop> PlantLoops { get; set; }
        public virtual ICollection<EQVessel> PlantVessels { get; set; }
        public virtual ICollection<EQAuxilary> PlantAux { get; set; }
        public virtual ICollection<Unit> Units { get; set; }
        //public List<EQControlLoop> PlantLoops { get; set; }
        //public List<EQVessel> PlantVessels { get; set; }
        //public List<EQAuxilary> PlantAux { get; set; }
        //public List<Unit> Units { get; set; }

        public virtual ICollection<MasterRecipe> ThisPlantsBrands { get; set; }

    }

    /// <summary>
    /// Batch is the equivelant of an S88 Control Recipe which is a copy of a Master Recipe
    /// </summary>
    public class Batch
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int BatchID { get; set; }
        public string BatchName { get; set; }
        public string BatchLocation { get; set; }


        [ForeignKey("MasterRecipe")]
        public int MasterRecipeID { get; set; }
        public virtual MasterRecipe MasterRecipe { get; set; }

        public virtual ICollection<Unit> AllocatedUnits { get; set; }

    }


    public class MasterRecipe
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int MasterRecipeID { get; set; }

        public string BrandName { get; set; }
        public string BrandDescription { get; set; }

        public string QaulityTargets { get; set; }// holding spot could be another set of domain objects for quality data 
        /// <summary>
        /// String of Operations that constitute a Recipe Squence
        /// </summary>
        public virtual ICollection<RecUnitOperation> RecOperations { get; set; }

        /// <summary>
        /// Collection of all ingredients needed in this recipe
        /// </summary>
        public virtual ICollection<Ingredient> Ingredients { get; set; }

        /// <summary>
        /// Navigation property
        /// </summary>
        public virtual ICollection<Plant> Plants { get; set; }
    }

    /// <summary>
    /// This will show what the recipe needs to have happen to make this brand
    /// specific temperature set points, etc...
    /// </summary>
    public partial class RecUnitOperation
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int RecUnitOperationID { get; set; }

        /// <summary>
        /// What does this operation do -- typicall set SP or actions on the UnitOperation
        /// </summary>
        //public string WhatTheRecipeNeedsToHappen { get; set; }
        public string OperationName { get; set; }
        /// <summary>
        /// Will be the set point to the controller
        /// </summary>
        public float SetPoint { get; set; }
        //public float SetPointToEQOperation { get; set; }

        //public string Transition_WhatEndsThisOperation { get; set; }

        public virtual ICollection<Transition> Transitions { get; set; }
        public bool AllTransitionsTrue { get; set; }

        /// <summary>
        /// Navigation property
        /// </summary>
        public virtual ICollection<Unit> AllowedUnits { get; set; }


        public virtual ICollection<MasterRecipe> MasterRecipes { get; set; }

    }

    public enum Operants
    {
        GreaterThan,
        Equals,
        LessThan,
        GreaterThanOrEqual,
        LessThanOrEqual

    }

    public partial class Transition
    {
        public int TransitionID { get; set; }

        public string TransitionName { get; set; }

        public EQControlLoop loop { get; set; }
        public float SetPoint { get; set; }

        public int? Timer { get; set; }
        public int Time { get; set; }

        public bool? EvaluateCondition { get; set; }

        public Operants operant { get; set; }

        public virtual ICollection<RecUnitOperation> RecUnitOperations { get; set; }


    }


    public class Ingredient
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IngredientID { get; set; }

        //[Required]
        //[ForeignKey("IngredientType")]
        //public int IngredientTypeID { get; set; }
        //public IngredientType type { get; set; }

        public int AmountNeeded { get; set; }
        public int? AmountActuallyRecieved { get; set; }

        public IngredientType IngredientType { get; set; }
        [ForeignKey("IngredientType")]
        public int IngredientTypeID { get; set; }
    }

    /// <summary>
    /// base types of all ingredients --- all malts, hops, water, syrups
    /// </summary>
    public class IngredientType
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IngredientTypeID { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
        public int FinancialSystemCode { get; set; }



    }

    public interface IFluentTest
    {
        IFluentTest SetTestString(string d);
        IFluentTest SetTestInt(int s);
        //void Save();

    }

    public class TestEntitieBuilder : IFluentTest
    {
        private TestEntitie _ent;

        public IFluentTest Create()
        {
            _ent = new TestEntitie();
            return this;
        }

        public IFluentTest SetTestString(string w)
        {
            _ent.teststring = w;
            return this;

        }

        public IFluentTest SetTestInt(int q)
        {
            _ent.testint = q;
            return this;
        }

        public TestEntitie Value()
        {
            return _ent;
        }

    }

    public class TestEntitie
    {
        public int TestEntitieID { get; set; }



        public string teststring { get; set; }
        public int testint { get; set; }
        public float testfloat { get; set; }

    }


    public class TestEntitieTwo
    {
        public int TestEntitieTwoID { get; set; }

        public TestEntitieTwo()
        {
        }
        public TestEntitieTwo(string asdf, int asdd, float fve)
        {
            teststring = asdf;
            testint = asdd;
            testfloat = fve;
        }

        public string teststring { get; private set; }
        public int testint { get; private set; }
        public float testfloat { get; private set; }



    }

    public class CollectionOfTestEntitie
    {
        public int CollectionOfTestEntitieID { get; set; }

        public string testcolstring { get; set; }
        public int testcolint { get; set; }
        public float testcolfloat { get; set; }

        public ICollection<TestEntitie> TestEntities { get; set; }
        public ICollection<TestEntitieTwo> TestEntitieTwos { get; set; }

        public void AddEntitie(TestEntitie ent)
        {
            //TestEntities.Add(ent);

            //TestEntities.Add(new TestEntitie().SetTestInt(222).SetTestString("sdssdss").Save());
            TestEntitieBuilder buildertest = new TestEntitieBuilder();
            // buildertest.Create().SetTestString("asdfsdfa").SetTestInt(234234);
            buildertest.Create().SetTestString(ent.teststring).SetTestInt(ent.testint);
            TestEntities.Add(buildertest.Value());
        }

    }




}

