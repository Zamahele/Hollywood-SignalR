namespace SignalRDbUpdates.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HollywoodTest2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EventDetails", "FinishingPosition", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EventDetails", "FinishingPosition", c => c.Int());
        }
    }
}
