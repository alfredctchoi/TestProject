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
                        StateId = c.Int(),
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
                .ForeignKey("dbo.States", t => t.StateId)
                .ForeignKey("dbo.Vendors", t => t.VendorId)
                .Index(t => t.VendorId)
                .Index(t => t.StateId)
                .Index(t => t.CreatedUser_UserId)
                .Index(t => t.ModifiedUser_UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Email = c.String(maxLength: 255, unicode: false),
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
                .Index(t => t.Email, unique: true, name: "XI_UserEmail")
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
                "dbo.States",
                c => new
                    {
                        StateId = c.Int(nullable: false, identity: true),
                        CountryId = c.Int(nullable: false),
                        Name = c.String(),
                        Code = c.String(),
                        CreatedUserId = c.Int(),
                        ModifiedUserId = c.Int(),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.StateId)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.CreatedUserId)
                .ForeignKey("dbo.Users", t => t.ModifiedUserId)
                .Index(t => t.CountryId)
                .Index(t => t.CreatedUserId)
                .Index(t => t.ModifiedUserId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsoShort = c.String(maxLength: 10, unicode: false),
                        IsoLong = c.String(maxLength: 10, unicode: false),
                        CreatedUserId = c.Int(),
                        ModifiedUserId = c.Int(),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                        Deleted = c.Boolean(nullable: false),
                        State_StateId = c.Int(),
                    })
                .PrimaryKey(t => t.CountryId)
                .ForeignKey("dbo.Users", t => t.CreatedUserId)
                .ForeignKey("dbo.Users", t => t.ModifiedUserId)
                .ForeignKey("dbo.States", t => t.State_StateId)
                .Index(t => t.IsoShort, unique: true, name: "XI_IsoShort")
                .Index(t => t.IsoLong, unique: true, name: "XI_IsoLong")
                .Index(t => t.CreatedUserId)
                .Index(t => t.ModifiedUserId)
                .Index(t => t.State_StateId);
            
            CreateTable(
                "dbo.Vendors",
                c => new
                    {
                        VendorId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.String(),
                        CountryId = c.Int(nullable: false),
                        Currency = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        CreatedUserId = c.Int(),
                        ModifiedUserId = c.Int(),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.VendorId)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.CreatedUserId)
                .ForeignKey("dbo.Users", t => t.ModifiedUserId)
                .Index(t => t.CountryId)
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
            DropForeignKey("dbo.Vendors", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Banks", "VendorId", "dbo.Vendors");
            DropForeignKey("dbo.Banks", "StateId", "dbo.States");
            DropForeignKey("dbo.States", "ModifiedUserId", "dbo.Users");
            DropForeignKey("dbo.States", "CreatedUserId", "dbo.Users");
            DropForeignKey("dbo.States", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Countries", "State_StateId", "dbo.States");
            DropForeignKey("dbo.Countries", "ModifiedUserId", "dbo.Users");
            DropForeignKey("dbo.Countries", "CreatedUserId", "dbo.Users");
            DropForeignKey("dbo.Banks", "ModifiedUser_UserId", "dbo.Users");
            DropForeignKey("dbo.Banks", "CreatedUser_UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "ModifiedUserId", "dbo.Users");
            DropForeignKey("dbo.Users", "CreatedUserId", "dbo.Users");
            DropIndex("dbo.Sessions", new[] { "UserId" });
            DropIndex("dbo.Vendors", new[] { "ModifiedUserId" });
            DropIndex("dbo.Vendors", new[] { "CreatedUserId" });
            DropIndex("dbo.Vendors", new[] { "CountryId" });
            DropIndex("dbo.Countries", new[] { "State_StateId" });
            DropIndex("dbo.Countries", new[] { "ModifiedUserId" });
            DropIndex("dbo.Countries", new[] { "CreatedUserId" });
            DropIndex("dbo.Countries", "XI_IsoLong");
            DropIndex("dbo.Countries", "XI_IsoShort");
            DropIndex("dbo.States", new[] { "ModifiedUserId" });
            DropIndex("dbo.States", new[] { "CreatedUserId" });
            DropIndex("dbo.States", new[] { "CountryId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "ModifiedUserId" });
            DropIndex("dbo.Users", new[] { "CreatedUserId" });
            DropIndex("dbo.Users", "XI_UserEmail");
            DropIndex("dbo.Banks", new[] { "ModifiedUser_UserId" });
            DropIndex("dbo.Banks", new[] { "CreatedUser_UserId" });
            DropIndex("dbo.Banks", new[] { "StateId" });
            DropIndex("dbo.Banks", new[] { "VendorId" });
            DropTable("dbo.Sessions");
            DropTable("dbo.Vendors");
            DropTable("dbo.Countries");
            DropTable("dbo.States");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Users");
            DropTable("dbo.Banks");
        }
    }
}
