namespace DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HopelijkDeFix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "Recipe_Id", "dbo.Recipes");
            DropIndex("dbo.Products", new[] { "Recipe_Id" });
            CreateTable(
                "dbo.RecipeProducts",
                c => new
                    {
                        Recipe_Id = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Recipe_Id, t.Product_Id })
                .ForeignKey("dbo.Recipes", t => t.Recipe_Id, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.Recipe_Id)
                .Index(t => t.Product_Id);
            
            DropColumn("dbo.Products", "Recipe_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Recipe_Id", c => c.Int());
            DropForeignKey("dbo.RecipeProducts", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.RecipeProducts", "Recipe_Id", "dbo.Recipes");
            DropIndex("dbo.RecipeProducts", new[] { "Product_Id" });
            DropIndex("dbo.RecipeProducts", new[] { "Recipe_Id" });
            DropTable("dbo.RecipeProducts");
            CreateIndex("dbo.Products", "Recipe_Id");
            AddForeignKey("dbo.Products", "Recipe_Id", "dbo.Recipes", "Id");
        }
    }
}
