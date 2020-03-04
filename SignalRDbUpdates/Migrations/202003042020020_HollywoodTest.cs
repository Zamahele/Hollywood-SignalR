namespace SignalRDbUpdates.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HollywoodTest : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        TournamentId = c.Int(nullable: false),
                        EventName = c.String(nullable: false),
                        EventNumber = c.Int(nullable: false),
                        EventDateTime = c.DateTime(nullable: false),
                        EventEndDateTime = c.DateTime(),
                        AutoClose = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EventId)
                .ForeignKey("dbo.Tournaments", t => t.TournamentId, cascadeDelete: true)
                .Index(t => t.TournamentId);
            
            CreateTable(
                "dbo.Tournaments",
                c => new
                    {
                        TournamentId = c.Int(nullable: false, identity: true),
                        TournamentName = c.String(),
                    })
                .PrimaryKey(t => t.TournamentId);
            
            CreateTable(
                "dbo.EventDetails",
                c => new
                    {
                        EventDetailId = c.Int(nullable: false, identity: true),
                        EventId = c.Int(nullable: false),
                        EventDetailStatusId = c.Int(nullable: false),
                        EventDetailName = c.String(nullable: false),
                        EventDetailNumber = c.Int(nullable: false),
                        EventDetailOdd = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FinishingPosition = c.Int(nullable: false),
                        FirstTimer = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EventDetailId)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .ForeignKey("dbo.EventDetailStatus", t => t.EventDetailStatusId, cascadeDelete: true)
                .Index(t => t.EventId)
                .Index(t => t.EventDetailStatusId);
            
            CreateTable(
                "dbo.EventDetailStatus",
                c => new
                    {
                        EventDetailStatusId = c.Int(nullable: false, identity: true),
                        EventDetailStatusName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.EventDetailStatusId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
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
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.EventDetails", "EventDetailStatusId", "dbo.EventDetailStatus");
            DropForeignKey("dbo.EventDetails", "EventId", "dbo.Events");
            DropForeignKey("dbo.Events", "TournamentId", "dbo.Tournaments");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.EventDetails", new[] { "EventDetailStatusId" });
            DropIndex("dbo.EventDetails", new[] { "EventId" });
            DropIndex("dbo.Events", new[] { "TournamentId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.EventDetailStatus");
            DropTable("dbo.EventDetails");
            DropTable("dbo.Tournaments");
            DropTable("dbo.Events");
        }
    }
}
