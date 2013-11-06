namespace MvcApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jddk : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EQTypes",
                c => new
                    {
                        EQTypeID = c.Int(nullable: false, identity: true),
                        TypeDescription = c.String(),
                    })
                .PrimaryKey(t => t.EQTypeID);
            
            CreateTable(
                "dbo.EQControlLoops",
                c => new
                    {
                        BaseEquipID = c.Int(nullable: false, identity: true),
                        TagName = c.String(),
                        MeasuredValue = c.Single(),
                        SetPoint = c.Single(),
                        Output = c.Single(),
                        HIHIAlarm = c.Single(),
                        HIAlarm = c.Single(),
                        LOAlarm = c.Single(),
                        LOLOAlarm = c.Single(),
                        ReverseActing = c.Boolean(nullable: false),
                        SimMeasValue = c.Single(),
                        EnableTrending = c.Boolean(nullable: false),
                        PlantID = c.Int(nullable: false),
                        EQTypeID = c.Int(nullable: false),
                        EquipName = c.String(),
                        AssetTag = c.String(),
                        Description = c.String(),
                        Unit_UnitID = c.Int(),
                    })
                .PrimaryKey(t => t.BaseEquipID)
                .ForeignKey("dbo.Plants", t => t.PlantID, cascadeDelete: true)
                .ForeignKey("dbo.Units", t => t.Unit_UnitID)
                .ForeignKey("dbo.EQTypes", t => t.EQTypeID, cascadeDelete: true)
                .Index(t => t.PlantID)
                .Index(t => t.Unit_UnitID)
                .Index(t => t.EQTypeID);
            
            CreateTable(
                "dbo.Plants",
                c => new
                    {
                        PlantID = c.Int(nullable: false, identity: true),
                        PlantName = c.String(),
                        PlantLocation = c.String(),
                        PlantAddress = c.String(),
                        BrewPubMenu = c.String(),
                    })
                .PrimaryKey(t => t.PlantID);
            
            CreateTable(
                "dbo.EQVessels",
                c => new
                    {
                        BaseEquipID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        MaxVolume = c.Single(nullable: false),
                        MinVolume = c.Single(nullable: false),
                        MaxTemp = c.Single(nullable: false),
                        PlantID = c.Int(nullable: false),
                        EQTypeID = c.Int(nullable: false),
                        EquipName = c.String(),
                        AssetTag = c.String(),
                        Description = c.String(),
                        Unit_UnitID = c.Int(),
                    })
                .PrimaryKey(t => t.BaseEquipID)
                .ForeignKey("dbo.EQTypes", t => t.EQTypeID, cascadeDelete: true)
                .ForeignKey("dbo.Plants", t => t.PlantID, cascadeDelete: true)
                .ForeignKey("dbo.Units", t => t.Unit_UnitID)
                .Index(t => t.EQTypeID)
                .Index(t => t.PlantID)
                .Index(t => t.Unit_UnitID);
            
            CreateTable(
                "dbo.EQAuxilaries",
                c => new
                    {
                        BaseEquipID = c.Int(nullable: false, identity: true),
                        OnOff = c.Int(nullable: false),
                        Running = c.Int(nullable: false),
                        PlantID = c.Int(nullable: false),
                        EQTypeID = c.Int(nullable: false),
                        EquipName = c.String(),
                        AssetTag = c.String(),
                        Description = c.String(),
                        Unit_UnitID = c.Int(),
                    })
                .PrimaryKey(t => t.BaseEquipID)
                .ForeignKey("dbo.EQTypes", t => t.EQTypeID, cascadeDelete: true)
                .ForeignKey("dbo.Plants", t => t.PlantID, cascadeDelete: true)
                .ForeignKey("dbo.Units", t => t.Unit_UnitID)
                .Index(t => t.EQTypeID)
                .Index(t => t.PlantID)
                .Index(t => t.Unit_UnitID);
            
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        UnitID = c.Int(nullable: false, identity: true),
                        UnitName = c.String(),
                        PlantID = c.Int(nullable: false),
                        Unit_UnitID = c.Int(),
                        Unit_UnitID1 = c.Int(),
                        CurrentEQOperation_UnitOperationID = c.Int(),
                        CurrentBatch_BatchID = c.Int(),
                    })
                .PrimaryKey(t => t.UnitID)
                .ForeignKey("dbo.Units", t => t.Unit_UnitID)
                .ForeignKey("dbo.Units", t => t.Unit_UnitID1)
                .ForeignKey("dbo.EQUnitOperations", t => t.CurrentEQOperation_UnitOperationID)
                .ForeignKey("dbo.Batches", t => t.CurrentBatch_BatchID)
                .ForeignKey("dbo.Plants", t => t.PlantID, cascadeDelete: true)
                .Index(t => t.Unit_UnitID)
                .Index(t => t.Unit_UnitID1)
                .Index(t => t.CurrentEQOperation_UnitOperationID)
                .Index(t => t.CurrentBatch_BatchID)
                .Index(t => t.PlantID);
            
            CreateTable(
                "dbo.EQUnitOperations",
                c => new
                    {
                        UnitOperationID = c.Int(nullable: false, identity: true),
                        NameOfOperation = c.String(),
                        WhatTheEquipmentCanDo = c.String(),
                        Unit_UnitID = c.Int(),
                    })
                .PrimaryKey(t => t.UnitOperationID)
                .ForeignKey("dbo.Units", t => t.Unit_UnitID)
                .Index(t => t.Unit_UnitID);
            
            CreateTable(
                "dbo.Batches",
                c => new
                    {
                        BatchID = c.Int(nullable: false, identity: true),
                        BatchName = c.String(),
                        BatchLocation = c.String(),
                        BatchRecipe_MasterRecipeID = c.Int(),
                    })
                .PrimaryKey(t => t.BatchID)
                .ForeignKey("dbo.MasterRecipes", t => t.BatchRecipe_MasterRecipeID)
                .Index(t => t.BatchRecipe_MasterRecipeID);
            
            CreateTable(
                "dbo.MasterRecipes",
                c => new
                    {
                        MasterRecipeID = c.Int(nullable: false, identity: true),
                        BrandName = c.String(),
                    })
                .PrimaryKey(t => t.MasterRecipeID);
            
            CreateTable(
                "dbo.RecOperations",
                c => new
                    {
                        RecOperationID = c.Int(nullable: false, identity: true),
                        WhatTheRecipeNeedsToHappen = c.String(),
                        Transition_WhatEndsThisOperation = c.String(),
                        MasterRecipe_MasterRecipeID = c.Int(),
                    })
                .PrimaryKey(t => t.RecOperationID)
                .ForeignKey("dbo.MasterRecipes", t => t.MasterRecipe_MasterRecipeID)
                .Index(t => t.MasterRecipe_MasterRecipeID);
            
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        IngredientID = c.Int(nullable: false, identity: true),
                        AmountNeeded = c.Int(nullable: false),
                        AmountActuallyRecieved = c.Int(nullable: false),
                        type_IngredientTypeID = c.Int(),
                        MasterRecipe_MasterRecipeID = c.Int(),
                    })
                .PrimaryKey(t => t.IngredientID)
                .ForeignKey("dbo.IngredientTypes", t => t.type_IngredientTypeID)
                .ForeignKey("dbo.MasterRecipes", t => t.MasterRecipe_MasterRecipeID)
                .Index(t => t.type_IngredientTypeID)
                .Index(t => t.MasterRecipe_MasterRecipeID);
            
            CreateTable(
                "dbo.IngredientTypes",
                c => new
                    {
                        IngredientTypeID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Cost = c.Int(nullable: false),
                        FinancialSystemCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IngredientTypeID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Ingredients", new[] { "MasterRecipe_MasterRecipeID" });
            DropIndex("dbo.Ingredients", new[] { "type_IngredientTypeID" });
            DropIndex("dbo.RecOperations", new[] { "MasterRecipe_MasterRecipeID" });
            DropIndex("dbo.Batches", new[] { "BatchRecipe_MasterRecipeID" });
            DropIndex("dbo.EQUnitOperations", new[] { "Unit_UnitID" });
            DropIndex("dbo.Units", new[] { "PlantID" });
            DropIndex("dbo.Units", new[] { "CurrentBatch_BatchID" });
            DropIndex("dbo.Units", new[] { "CurrentEQOperation_UnitOperationID" });
            DropIndex("dbo.Units", new[] { "Unit_UnitID1" });
            DropIndex("dbo.Units", new[] { "Unit_UnitID" });
            DropIndex("dbo.EQAuxilaries", new[] { "Unit_UnitID" });
            DropIndex("dbo.EQAuxilaries", new[] { "PlantID" });
            DropIndex("dbo.EQAuxilaries", new[] { "EQTypeID" });
            DropIndex("dbo.EQVessels", new[] { "Unit_UnitID" });
            DropIndex("dbo.EQVessels", new[] { "PlantID" });
            DropIndex("dbo.EQVessels", new[] { "EQTypeID" });
            DropIndex("dbo.EQControlLoops", new[] { "EQTypeID" });
            DropIndex("dbo.EQControlLoops", new[] { "Unit_UnitID" });
            DropIndex("dbo.EQControlLoops", new[] { "PlantID" });
            DropForeignKey("dbo.Ingredients", "MasterRecipe_MasterRecipeID", "dbo.MasterRecipes");
            DropForeignKey("dbo.Ingredients", "type_IngredientTypeID", "dbo.IngredientTypes");
            DropForeignKey("dbo.RecOperations", "MasterRecipe_MasterRecipeID", "dbo.MasterRecipes");
            DropForeignKey("dbo.Batches", "BatchRecipe_MasterRecipeID", "dbo.MasterRecipes");
            DropForeignKey("dbo.EQUnitOperations", "Unit_UnitID", "dbo.Units");
            DropForeignKey("dbo.Units", "PlantID", "dbo.Plants");
            DropForeignKey("dbo.Units", "CurrentBatch_BatchID", "dbo.Batches");
            DropForeignKey("dbo.Units", "CurrentEQOperation_UnitOperationID", "dbo.EQUnitOperations");
            DropForeignKey("dbo.Units", "Unit_UnitID1", "dbo.Units");
            DropForeignKey("dbo.Units", "Unit_UnitID", "dbo.Units");
            DropForeignKey("dbo.EQAuxilaries", "Unit_UnitID", "dbo.Units");
            DropForeignKey("dbo.EQAuxilaries", "PlantID", "dbo.Plants");
            DropForeignKey("dbo.EQAuxilaries", "EQTypeID", "dbo.EQTypes");
            DropForeignKey("dbo.EQVessels", "Unit_UnitID", "dbo.Units");
            DropForeignKey("dbo.EQVessels", "PlantID", "dbo.Plants");
            DropForeignKey("dbo.EQVessels", "EQTypeID", "dbo.EQTypes");
            DropForeignKey("dbo.EQControlLoops", "EQTypeID", "dbo.EQTypes");
            DropForeignKey("dbo.EQControlLoops", "Unit_UnitID", "dbo.Units");
            DropForeignKey("dbo.EQControlLoops", "PlantID", "dbo.Plants");
            DropTable("dbo.IngredientTypes");
            DropTable("dbo.Ingredients");
            DropTable("dbo.RecOperations");
            DropTable("dbo.MasterRecipes");
            DropTable("dbo.Batches");
            DropTable("dbo.EQUnitOperations");
            DropTable("dbo.Units");
            DropTable("dbo.EQAuxilaries");
            DropTable("dbo.EQVessels");
            DropTable("dbo.Plants");
            DropTable("dbo.EQControlLoops");
            DropTable("dbo.EQTypes");
        }
    }
}
