//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcApplication2.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class UnitOperation
    {
        public UnitOperation()
        {
            this.Units = new HashSet<Unit>();
        }
    
        public int UnitOperationID { get; set; }
        public string WhatTheEquipmentCanDo { get; set; }
    
        public virtual ICollection<Unit> Units { get; set; }
    }
}