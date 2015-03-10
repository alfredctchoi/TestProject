namespace TestProject.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sessions",
                c => new
                    {
                        SessionId = c.Long(nullable: false, identity: true),
                        Token = c.Guid(nullable: false),
                        UserId = c.Int(nullable: false),
                        Expires = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SessionId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Password = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Role = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        CreatedUserId = c.Int(),
                        ModifiedUserId = c.Int(),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Users", t => t.CreatedUserId)
                .ForeignKey("dbo.Users", t => t.ModifiedUserId)
                .Index(t => t.CreatedUserId)
                .Index(t => t.ModifiedUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sessions", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "ModifiedUserId", "dbo.Users");
            DropForeignKey("dbo.Users", "CreatedUserId", "dbo.Users");
            DropIndex("dbo.Users", new[] { "ModifiedUserId" });
            DropIndex("dbo.Users", new[] { "CreatedUserId" });
            DropIndex("dbo.Sessions", new[] { "UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.Sessions");
        }
    }
}
