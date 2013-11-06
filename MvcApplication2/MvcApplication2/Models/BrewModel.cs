﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

///added by me
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace MvcApplication2.Models
{
    public class BrewModel
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

    public class EQControlLoop :BaseEquip
    {
        public string TagName { get; set; }

        public float? MeasuredValue { get; set; }
        public float? SetPoint { get; set; }
        public float? Output { get; set; }

        public float? HIHIAlarm { get; set; }
        public float? HIAlarm { get; set; }
        public float? LOAlarm { get; set; }
        public float? LOLOAlarm { get; set; }

        public bool ReverseActing { get; set; }


        /// <summary>
        /// sim values
        /// </summary>
        public float? SimMeasValue { get; set; }

        /// plus ton of additional fields

        public bool EnableTrending { get; set; }

        public void DoSomething (int blah) { }

       
        public virtual Plant Plant { get; set; }
        [Required]
        [ForeignKey("Plant")]
        public int PlantID { get; set; }

        public virtual EQType EQType { get; set; }
        [ForeignKey("EQType")]
        public int EQTypeID { get; set; }

    }
    /// <summary>
    /// Auxilary Equipment pumps mills
    /// These will have extra commands like on/off 
    /// </summary>
    public class EQAuxilary : BaseEquip
    {
        public int OnOff { get; set; }
        public int Running { get; set; }

       
        public virtual Plant Plant { get; set; }
        [Required]
        [ForeignKey("Plant")]
        public int PlantID { get; set; }

        public virtual EQType EQType { get; set; }
        [ForeignKey("EQType")]
        public int EQTypeID { get; set; }
    }

    public class EQVessel: BaseEquip
    {
        public string Name { get; set; }

        public float MaxVolume { get; set; }
        public float MinVolume { get; set; }
        public float MaxTemp { get; set; }

       
        public virtual Plant Plant { get; set; }
        [Required]
        [ForeignKey("Plant")]
        public int PlantID { get; set; }

        public virtual EQType EQType { get; set; }
        [ForeignKey("EQType")]
        public int EQTypeID { get; set; }
       
    }

    /// <summary>
    /// Group of equipment to form a functioning unit of pumps
    /// and control valves
    /// </summary>
    public class Unit
    {

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        public int UnitID { get; set; }

        public string UnitName { get; set; }

        /// <summary>
        /// potential units that can feed this unit
        /// </summary>
        public virtual ICollection<Unit> InputUnits { get; set; }
        /// <summary>
        /// potential units that output of this unit can feed
        /// </summary>
        [InverseProperty("UnitID")]
        public virtual ICollection<Unit> OutputUnits { get; set; }
        [InverseProperty("UnitOperationID")]
        public virtual ICollection<EQUnitOperation> AvailableOperations { get; set; }

        //[InverseProperty]
        [InverseProperty("BaseEquipID")]
        public virtual ICollection<EQControlLoop> UnitLoops { get; set; }
        public virtual ICollection<EQAuxilary> UnitAux { get; set; }
        public virtual ICollection<EQVessel> UnitVessels { get; set; }

        public Batch CurrentBatch { get; set; }

        public EQUnitOperation CurrentEQOperation { get; set; }

//        public void AddLoop(EQControlLoop loop) { EQControlLoops.Add(loop); }

        public virtual Plant Plant { get; set; }
        [Required]
        [ForeignKey("Plant")]
        public int PlantID { get; set; }

    }

    /// <summary>
    /// List of all operations that can occure on these units
    /// Heating, Pumping, etc... these usually take the coordinated effort of multiple equipment 
    /// a concerted effort amongs pumps, valves, controllers.
    /// Can be defined for multiple units - aka  4 brew kettles can all heat, they all have a heat operation
    /// </summary>
    public class EQUnitOperation
    {

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        public int UnitOperationID { get; set; }
        public string NameOfOperation { get; set; }

        public string WhatTheEquipmentCanDo { get; set; }


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

        public MasterRecipe BatchRecipe { get; set; }

        public virtual ICollection<Unit> Units { get; set; } 

    }


    public class MasterRecipe 
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int MasterRecipeID { get; set; }
        public string BrandName { get; set; }

        /// <summary>
        /// String of Operations that constitute a Recipe Squence
        /// </summary>
        public virtual ICollection<RecOperation> RecOperations { get; set; }

        /// <summary>
        /// Collection of all ingredients needed in this recipe
        /// </summary>
        public virtual ICollection<Ingredient> Ingredients { get; set; } 

    }

    /// <summary>
    /// This will show what the recipe needs to have happen to make this brand
    /// specific temperature set points, etc...
    /// </summary>
    public class RecOperation
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int RecOperationID { get; set; }

        /// <summary>
        /// What does this operation do -- typicall set SP or actions on the UnitOperation
        /// </summary>
        public string WhatTheRecipeNeedsToHappen { get; set; }

        public string Transition_WhatEndsThisOperation { get; set; }



    }



    public class Ingredient
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IngredientID { get; set; }

        
        public IngredientType type { get; set; }
        public int AmountNeeded { get; set; }
        public int AmountActuallyRecieved { get; set; }


    }

    /// <summary>
    /// base types of all ingredients --- all malts, hops, water, syrups
    /// </summary>
    public class IngredientType
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IngredientTypeID { get; set; }

        public string Name { get; set; }
        public int Cost { get; set; }
        public int FinancialSystemCode { get; set; }



    }

}