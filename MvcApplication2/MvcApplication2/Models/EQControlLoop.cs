//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcApplication2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class EQControlLoop
    {
        public int BaseEquipID { get; set; }
        public string TagName { get; set; }
        public float MeasuredValue { get; set; }
        public float SetPoint { get; set; }
        public float Output { get; set; }
        public float HIHIAlarm { get; set; }
        public float HIAlarm { get; set; }
        public float LOAlarm { get; set; }
        public float LOLOAlarm { get; set; }
        public float SimMeasValue { get; set; }
        public string EquipName { get; set; }
        public string AssetTag { get; set; }
        public string Description { get; set; }
        public Nullable<int> type_EQTypeID { get; set; }
        public Nullable<int> Unit_UnitID { get; set; }
    
        public virtual EQType EQType { get; set; }
        public virtual Unit Unit { get; set; }
    }
}
