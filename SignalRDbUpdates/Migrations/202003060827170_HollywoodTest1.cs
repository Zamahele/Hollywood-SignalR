namespace SignalRDbUpdates.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HollywoodTest1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EventDetails", "FinishingPosition", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EventDetails", "FinishingPosition", c => c.Int(nullable: false));
        }
    }
}
