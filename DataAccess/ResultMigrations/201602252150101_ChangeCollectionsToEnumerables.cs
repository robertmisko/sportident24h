namespace NewSpecialEvent.ResultMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeCollectionsToEnumerables : DbMigration
    {
        public override void Up()
        {
            this.DropForeignKey("dbo.ControlDatas", "Result_Id", "dbo.Results");
            this.DropIndex("dbo.ControlDatas", new[] { "Result_Id" });
            this.DropColumn("dbo.ControlDatas", "Result_Id");
        }
        
        public override void Down()
        {
            this.AddColumn("dbo.ControlDatas", "Result_Id", c => c.Int());
            this.CreateIndex("dbo.ControlDatas", "Result_Id");
            this.AddForeignKey("dbo.ControlDatas", "Result_Id", "dbo.Results", "Id");
        }
    }
}
