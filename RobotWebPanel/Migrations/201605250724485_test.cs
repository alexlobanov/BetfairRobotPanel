namespace RobotWebPanel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IdentityRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CreationDateTime = c.DateTime(nullable: false),
                        AccessDateTime = c.DateTime(nullable: false),
                        AccessLevel = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        AspNetUsers_Id = c.String(maxLength: 128),
                        AspNetUsers_Id1 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AspNetUsers_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AspNetUsers_Id1)
                .Index(t => t.AspNetUsers_Id)
                .Index(t => t.AspNetUsers_Id1);
            
            CreateTable(
                "dbo.IdentityUserLogins",
                c => new
                    {
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        AspNetUsers_Id = c.String(maxLength: 128),
                        AspNetUsers_Id1 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.AspNetUsers_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AspNetUsers_Id1)
                .Index(t => t.AspNetUsers_Id)
                .Index(t => t.AspNetUsers_Id1);
            
            CreateTable(
                "dbo.IdentityUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        AspNetUsers_Id = c.String(maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.AspNetUsers_Id)
                .ForeignKey("dbo.IdentityRoles", t => t.IdentityRole_Id)
                .Index(t => t.AspNetUsers_Id)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "dbo.RobotModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        AccessDate = c.DateTime(nullable: false),
                        LastBalance = c.Single(nullable: false),
                        LastAccessDate = c.DateTime(nullable: false),
                        CountStartsProgramm = c.Long(nullable: false),
                        UniqGuid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsersAspNetRoles",
                c => new
                    {
                        AspNetUsers_Id = c.String(nullable: false, maxLength: 128),
                        AspNetRoles_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.AspNetUsers_Id, t.AspNetRoles_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.AspNetUsers_Id, cascadeDelete: true)
                .ForeignKey("dbo.IdentityRoles", t => t.AspNetRoles_Id, cascadeDelete: true)
                .Index(t => t.AspNetUsers_Id)
                .Index(t => t.AspNetRoles_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRoles", "IdentityRole_Id", "dbo.IdentityRoles");
            DropForeignKey("dbo.IdentityUserRoles", "AspNetUsers_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.IdentityUserLogins", "AspNetUsers_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.IdentityUserClaims", "AspNetUsers_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.IdentityUserLogins", "AspNetUsers_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.IdentityUserClaims", "AspNetUsers_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsersAspNetRoles", "AspNetRoles_Id", "dbo.IdentityRoles");
            DropForeignKey("dbo.AspNetUsersAspNetRoles", "AspNetUsers_Id", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUsersAspNetRoles", new[] { "AspNetRoles_Id" });
            DropIndex("dbo.AspNetUsersAspNetRoles", new[] { "AspNetUsers_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "AspNetUsers_Id" });
            DropIndex("dbo.IdentityUserLogins", new[] { "AspNetUsers_Id1" });
            DropIndex("dbo.IdentityUserLogins", new[] { "AspNetUsers_Id" });
            DropIndex("dbo.IdentityUserClaims", new[] { "AspNetUsers_Id1" });
            DropIndex("dbo.IdentityUserClaims", new[] { "AspNetUsers_Id" });
            DropTable("dbo.AspNetUsersAspNetRoles");
            DropTable("dbo.RobotModels");
            DropTable("dbo.IdentityUserRoles");
            DropTable("dbo.IdentityUserLogins");
            DropTable("dbo.IdentityUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.IdentityRoles");
        }
    }
}
