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
    
    public partial class Batch
    {
        public Batch()
        {
            this.Units = new HashSet<Unit>();
        }
    
        public int BatchID { get; set; }
        public string BatchName { get; set; }
        public string BatchLocation { get; set; }
        public Nullable<int> BatchRecipe_RecipeID { get; set; }
    
        public virtual Recipe Recipe { get; set; }
        public virtual ICollection<Unit> Units { get; set; }
    }
}
