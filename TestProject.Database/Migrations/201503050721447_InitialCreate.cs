namespace TestProject.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Banks",
                c => new
                    {
                        VendorId = c.Int(nullable: false),
                        AccountNumber = c.String(),
                        BranchNumber = c.String(),
                        BankCode = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zip = c.String(),
                        CreatedUserId = c.Int(),
                        ModifiedUserId = c.Int(),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                        Deleted = c.Boolean(nullable: false),
                        CreatedUser_UserId = c.Int(),
                        ModifiedUser_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.VendorId)
                .ForeignKey("dbo.Users", t => t.CreatedUser_UserId)
                .ForeignKey("dbo.Users", t => t.ModifiedUser_UserId)
                .ForeignKey("dbo.Vendors", t => t.VendorId)
                .Index(t => t.VendorId)
                .Index(t => t.CreatedUser_UserId)
                .Index(t => t.ModifiedUser_UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Password = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        StatusEnum = c.Int(nullable: false),
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
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserRoleId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(),
                        Role = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserRoleId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Vendors",
                c => new
                    {
                        VendorId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.String(),
                        Currency = c.Int(nullable: false),
                        Country = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        CreatedUserId = c.Int(),
                        ModifiedUserId = c.Int(),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.VendorId)
                .ForeignKey("dbo.Users", t => t.CreatedUserId)
                .ForeignKey("dbo.Users", t => t.ModifiedUserId)
                .Index(t => t.CreatedUserId)
                .Index(t => t.ModifiedUserId);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sessions", "UserId", "dbo.Users");
            DropForeignKey("dbo.Vendors", "ModifiedUserId", "dbo.Users");
            DropForeignKey("dbo.Vendors", "CreatedUserId", "dbo.Users");
            DropForeignKey("dbo.Banks", "VendorId", "dbo.Vendors");
            DropForeignKey("dbo.Banks", "ModifiedUser_UserId", "dbo.Users");
            DropForeignKey("dbo.Banks", "CreatedUser_UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "ModifiedUserId", "dbo.Users");
            DropForeignKey("dbo.Users", "CreatedUserId", "dbo.Users");
            DropIndex("dbo.Sessions", new[] { "UserId" });
            DropIndex("dbo.Vendors", new[] { "ModifiedUserId" });
            DropIndex("dbo.Vendors", new[] { "CreatedUserId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "ModifiedUserId" });
            DropIndex("dbo.Users", new[] { "CreatedUserId" });
            DropIndex("dbo.Banks", new[] { "ModifiedUser_UserId" });
            DropIndex("dbo.Banks", new[] { "CreatedUser_UserId" });
            DropIndex("dbo.Banks", new[] { "VendorId" });
            DropTable("dbo.Sessions");
            DropTable("dbo.Vendors");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Users");
            DropTable("dbo.Banks");
        }
    }
}
