﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcApplication2.Diagrams
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BrewDBTestEntities2 : DbContext
    {
        public BrewDBTestEntities2()
            : base("name=BrewDBTestEntities2")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<EQAuxilary> EQAuxilaries { get; set; }
        public DbSet<EQControlLoop> EQControlLoops { get; set; }
        public DbSet<EQType> EQTypes { get; set; }
        public DbSet<EQUnitOperation> EQUnitOperations { get; set; }
        public DbSet<EQVessel> EQVessels { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<IngredientType> IngredientTypes { get; set; }
        public DbSet<MasterRecipe> MasterRecipes { get; set; }
        public DbSet<Plant> Plants { get; set; }
        public DbSet<RecOperation> RecOperations { get; set; }
        public DbSet<Unit> Units { get; set; }
    }
}
