namespace BrewMLLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
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
                "dbo.Units",
                c => new
                    {
                        UnitID = c.Int(nullable: false, identity: true),
                        UnitName = c.String(),
                        PlantID = c.Int(nullable: false),
                        EQUnitOperation_UnitOperationID = c.Int(),
                        CurrentBatch_BatchID = c.Int(),
                        CurrentEQOperation_UnitOperationID = c.Int(),
                        RecUnitOperation_RecUnitOperationID = c.Int(),
                    })
                .PrimaryKey(t => t.UnitID)
                .ForeignKey("dbo.EQUnitOperations", t => t.EQUnitOperation_UnitOperationID)
                .ForeignKey("dbo.Batches", t => t.CurrentBatch_BatchID)
                .ForeignKey("dbo.EQUnitOperations", t => t.CurrentEQOperation_UnitOperationID)
                .ForeignKey("dbo.RecUnitOperations", t => t.RecUnitOperation_RecUnitOperationID)
                .ForeignKey("dbo.Plants", t => t.PlantID, cascadeDelete: true)
                .Index(t => t.EQUnitOperation_UnitOperationID)
                .Index(t => t.CurrentBatch_BatchID)
                .Index(t => t.CurrentEQOperation_UnitOperationID)
                .Index(t => t.RecUnitOperation_RecUnitOperationID)
                .Index(t => t.PlantID);
            
            CreateTable(
                "dbo.EQUnitOperations",
                c => new
                    {
                        UnitOperationID = c.Int(nullable: false, identity: true),
                        NameOfOperation = c.String(),
                        Unit_UnitID = c.Int(),
                    })
                .PrimaryKey(t => t.UnitOperationID)
                .ForeignKey("dbo.Units", t => t.Unit_UnitID)
                .Index(t => t.Unit_UnitID);
            
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
                        ReverseActing = c.Boolean(),
                        SimMeasValue = c.Single(),
                        EnableTrending = c.Boolean(nullable: false),
                        EquipName = c.String(),
                        AssetTag = c.String(),
                        Description = c.String(),
                        EQType_EQTypeID = c.Int(),
                        Plant_PlantID = c.Int(nullable: false),
                        Unit_UnitID = c.Int(),
                    })
                .PrimaryKey(t => t.BaseEquipID)
                .ForeignKey("dbo.EQTypes", t => t.EQType_EQTypeID)
                .ForeignKey("dbo.Plants", t => t.Plant_PlantID, cascadeDelete: true)
                .ForeignKey("dbo.Units", t => t.Unit_UnitID)
                .Index(t => t.EQType_EQTypeID)
                .Index(t => t.Plant_PlantID)
                .Index(t => t.Unit_UnitID);
            
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
                "dbo.MasterRecipes",
                c => new
                    {
                        MasterRecipeID = c.Int(nullable: false, identity: true),
                        BrandName = c.String(),
                        BrandDescription = c.String(),
                        QaulityTargets = c.String(),
                    })
                .PrimaryKey(t => t.MasterRecipeID);
            
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        IngredientID = c.Int(nullable: false, identity: true),
                        AmountNeeded = c.Int(nullable: false),
                        AmountActuallyRecieved = c.Int(),
                        IngredientTypeID = c.Int(nullable: false),
                        MasterRecipe_MasterRecipeID = c.Int(),
                    })
                .PrimaryKey(t => t.IngredientID)
                .ForeignKey("dbo.IngredientTypes", t => t.IngredientTypeID, cascadeDelete: true)
                .ForeignKey("dbo.MasterRecipes", t => t.MasterRecipe_MasterRecipeID)
                .Index(t => t.IngredientTypeID)
                .Index(t => t.MasterRecipe_MasterRecipeID);
            
            CreateTable(
                "dbo.IngredientTypes",
                c => new
                    {
                        IngredientTypeID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Cost = c.Int(nullable: false),
                        FinancialSystemCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IngredientTypeID);
            
            CreateTable(
                "dbo.RecUnitOperations",
                c => new
                    {
                        RecUnitOperationID = c.Int(nullable: false, identity: true),
                        OperationName = c.String(),
                        SetPoint = c.Single(nullable: false),
                        AllTransitionsTrue = c.Boolean(nullable: false),
                        MasterRecipe_MasterRecipeID = c.Int(),
                    })
                .PrimaryKey(t => t.RecUnitOperationID)
                .ForeignKey("dbo.MasterRecipes", t => t.MasterRecipe_MasterRecipeID)
                .Index(t => t.MasterRecipe_MasterRecipeID);
            
            CreateTable(
                "dbo.Transitions",
                c => new
                    {
                        TransitionID = c.Int(nullable: false, identity: true),
                        TransitionName = c.String(),
                        Timer = c.Int(),
                        Time = c.Int(nullable: false),
                        EvaluateCondition = c.Boolean(),
                        operant = c.Int(nullable: false),
                        loop_BaseEquipID = c.Int(),
                        RecUnitOperation_RecUnitOperationID = c.Int(),
                    })
                .PrimaryKey(t => t.TransitionID)
                .ForeignKey("dbo.EQControlLoops", t => t.loop_BaseEquipID)
                .ForeignKey("dbo.RecUnitOperations", t => t.RecUnitOperation_RecUnitOperationID)
                .Index(t => t.loop_BaseEquipID)
                .Index(t => t.RecUnitOperation_RecUnitOperationID);
            
            CreateTable(
                "dbo.CollectionOfTestEntities",
                c => new
                    {
                        CollectionOfTestEntitieID = c.Int(nullable: false, identity: true),
                        testcolstring = c.String(),
                        testcolint = c.Int(nullable: false),
                        testcolfloat = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.CollectionOfTestEntitieID);
            
            CreateTable(
                "dbo.TestEntities",
                c => new
                    {
                        TestEntitieID = c.Int(nullable: false, identity: true),
                        teststring = c.String(),
                        testint = c.Int(nullable: false),
                        testfloat = c.Single(nullable: false),
                        CollectionOfTestEntitie_CollectionOfTestEntitieID = c.Int(),
                    })
                .PrimaryKey(t => t.TestEntitieID)
                .ForeignKey("dbo.CollectionOfTestEntities", t => t.CollectionOfTestEntitie_CollectionOfTestEntitieID)
                .Index(t => t.CollectionOfTestEntitie_CollectionOfTestEntitieID);
            
            CreateTable(
                "dbo.TestEntitieTwoes",
                c => new
                    {
                        TestEntitieTwoID = c.Int(nullable: false, identity: true),
                        teststring = c.String(),
                        testint = c.Int(nullable: false),
                        testfloat = c.Single(nullable: false),
                        CollectionOfTestEntitie_CollectionOfTestEntitieID = c.Int(),
                    })
                .PrimaryKey(t => t.TestEntitieTwoID)
                .ForeignKey("dbo.CollectionOfTestEntities", t => t.CollectionOfTestEntitie_CollectionOfTestEntitieID)
                .Index(t => t.CollectionOfTestEntitie_CollectionOfTestEntitieID);
            
            CreateTable(
                "dbo.UnitUnits",
                c => new
                    {
                        Unit_UnitID = c.Int(nullable: false),
                        Unit_UnitID1 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Unit_UnitID, t.Unit_UnitID1 })
                .ForeignKey("dbo.Units", t => t.Unit_UnitID)
                .ForeignKey("dbo.Units", t => t.Unit_UnitID1)
                .Index(t => t.Unit_UnitID)
                .Index(t => t.Unit_UnitID1);
            
            CreateTable(
                "dbo.MasterRecipePlants",
                c => new
                    {
                        MasterRecipe_MasterRecipeID = c.Int(nullable: false),
                        Plant_PlantID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MasterRecipe_MasterRecipeID, t.Plant_PlantID })
                .ForeignKey("dbo.MasterRecipes", t => t.MasterRecipe_MasterRecipeID, cascadeDelete: true)
                .ForeignKey("dbo.Plants", t => t.Plant_PlantID, cascadeDelete: true)
                .Index(t => t.MasterRecipe_MasterRecipeID)
                .Index(t => t.Plant_PlantID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TestEntitieTwoes", "CollectionOfTestEntitie_CollectionOfTestEntitieID", "dbo.CollectionOfTestEntities");
            DropForeignKey("dbo.TestEntities", "CollectionOfTestEntitie_CollectionOfTestEntitieID", "dbo.CollectionOfTestEntities");
            DropForeignKey("dbo.Batches", "BatchRecipe_MasterRecipeID", "dbo.MasterRecipes");
            DropForeignKey("dbo.EQVessels", "Unit_UnitID", "dbo.Units");
            DropForeignKey("dbo.EQControlLoops", "Unit_UnitID", "dbo.Units");
            DropForeignKey("dbo.EQAuxilaries", "Unit_UnitID", "dbo.Units");
            DropForeignKey("dbo.Units", "PlantID", "dbo.Plants");
            DropForeignKey("dbo.RecUnitOperations", "MasterRecipe_MasterRecipeID", "dbo.MasterRecipes");
            DropForeignKey("dbo.Transitions", "RecUnitOperation_RecUnitOperationID", "dbo.RecUnitOperations");
            DropForeignKey("dbo.Transitions", "loop_BaseEquipID", "dbo.EQControlLoops");
            DropForeignKey("dbo.Units", "RecUnitOperation_RecUnitOperationID", "dbo.RecUnitOperations");
            DropForeignKey("dbo.MasterRecipePlants", "Plant_PlantID", "dbo.Plants");
            DropForeignKey("dbo.MasterRecipePlants", "MasterRecipe_MasterRecipeID", "dbo.MasterRecipes");
            DropForeignKey("dbo.Ingredients", "MasterRecipe_MasterRecipeID", "dbo.MasterRecipes");
            DropForeignKey("dbo.Ingredients", "IngredientTypeID", "dbo.IngredientTypes");
            DropForeignKey("dbo.EQVessels", "PlantID", "dbo.Plants");
            DropForeignKey("dbo.EQVessels", "EQTypeID", "dbo.EQTypes");
            DropForeignKey("dbo.EQControlLoops", "Plant_PlantID", "dbo.Plants");
            DropForeignKey("dbo.EQControlLoops", "EQType_EQTypeID", "dbo.EQTypes");
            DropForeignKey("dbo.EQAuxilaries", "PlantID", "dbo.Plants");
            DropForeignKey("dbo.EQAuxilaries", "EQTypeID", "dbo.EQTypes");
            DropForeignKey("dbo.UnitUnits", "Unit_UnitID1", "dbo.Units");
            DropForeignKey("dbo.UnitUnits", "Unit_UnitID", "dbo.Units");
            DropForeignKey("dbo.Units", "CurrentEQOperation_UnitOperationID", "dbo.EQUnitOperations");
            DropForeignKey("dbo.Units", "CurrentBatch_BatchID", "dbo.Batches");
            DropForeignKey("dbo.EQUnitOperations", "Unit_UnitID", "dbo.Units");
            DropForeignKey("dbo.Units", "EQUnitOperation_UnitOperationID", "dbo.EQUnitOperations");
            DropIndex("dbo.TestEntitieTwoes", new[] { "CollectionOfTestEntitie_CollectionOfTestEntitieID" });
            DropIndex("dbo.TestEntities", new[] { "CollectionOfTestEntitie_CollectionOfTestEntitieID" });
            DropIndex("dbo.Batches", new[] { "BatchRecipe_MasterRecipeID" });
            DropIndex("dbo.EQVessels", new[] { "Unit_UnitID" });
            DropIndex("dbo.EQControlLoops", new[] { "Unit_UnitID" });
            DropIndex("dbo.EQAuxilaries", new[] { "Unit_UnitID" });
            DropIndex("dbo.Units", new[] { "PlantID" });
            DropIndex("dbo.RecUnitOperations", new[] { "MasterRecipe_MasterRecipeID" });
            DropIndex("dbo.Transitions", new[] { "RecUnitOperation_RecUnitOperationID" });
            DropIndex("dbo.Transitions", new[] { "loop_BaseEquipID" });
            DropIndex("dbo.Units", new[] { "RecUnitOperation_RecUnitOperationID" });
            DropIndex("dbo.MasterRecipePlants", new[] { "Plant_PlantID" });
            DropIndex("dbo.MasterRecipePlants", new[] { "MasterRecipe_MasterRecipeID" });
            DropIndex("dbo.Ingredients", new[] { "MasterRecipe_MasterRecipeID" });
            DropIndex("dbo.Ingredients", new[] { "IngredientTypeID" });
            DropIndex("dbo.EQVessels", new[] { "PlantID" });
            DropIndex("dbo.EQVessels", new[] { "EQTypeID" });
            DropIndex("dbo.EQControlLoops", new[] { "Plant_PlantID" });
            DropIndex("dbo.EQControlLoops", new[] { "EQType_EQTypeID" });
            DropIndex("dbo.EQAuxilaries", new[] { "PlantID" });
            DropIndex("dbo.EQAuxilaries", new[] { "EQTypeID" });
            DropIndex("dbo.UnitUnits", new[] { "Unit_UnitID1" });
            DropIndex("dbo.UnitUnits", new[] { "Unit_UnitID" });
            DropIndex("dbo.Units", new[] { "CurrentEQOperation_UnitOperationID" });
            DropIndex("dbo.Units", new[] { "CurrentBatch_BatchID" });
            DropIndex("dbo.EQUnitOperations", new[] { "Unit_UnitID" });
            DropIndex("dbo.Units", new[] { "EQUnitOperation_UnitOperationID" });
            DropTable("dbo.MasterRecipePlants");
            DropTable("dbo.UnitUnits");
            DropTable("dbo.TestEntitieTwoes");
            DropTable("dbo.TestEntities");
            DropTable("dbo.CollectionOfTestEntities");
            DropTable("dbo.Transitions");
            DropTable("dbo.RecUnitOperations");
            DropTable("dbo.IngredientTypes");
            DropTable("dbo.Ingredients");
            DropTable("dbo.MasterRecipes");
            DropTable("dbo.EQVessels");
            DropTable("dbo.EQControlLoops");
            DropTable("dbo.EQTypes");
            DropTable("dbo.EQAuxilaries");
            DropTable("dbo.Plants");
            DropTable("dbo.EQUnitOperations");
            DropTable("dbo.Units");
            DropTable("dbo.Batches");
        }
    }
}
