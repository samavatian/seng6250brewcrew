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
    
    public partial class EQType
    {
        public EQType()
        {
            this.EQAuxilaries = new HashSet<EQAuxilary>();
            this.EQControlLoops = new HashSet<EQControlLoop>();
            this.EQVessels = new HashSet<EQVessel>();
        }
    
        public int EQTypeID { get; set; }
        public string TypeDescription { get; set; }
    
        public virtual ICollection<EQAuxilary> EQAuxilaries { get; set; }
        public virtual ICollection<EQControlLoop> EQControlLoops { get; set; }
        public virtual ICollection<EQVessel> EQVessels { get; set; }
    }
}
