namespace NewSpecialEvent.ResultMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            this.CreateTable(
                "dbo.ControlDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PunchTime = c.Time(nullable: false, precision: 7),
                        ControlCode = c.Int(nullable: false),
                        DayOfWeek = c.String(),
                        Result_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Results", t => t.Result_Id)
                .Index(t => t.Result_Id);

            this.CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Controls = c.String(),
                    })
                .PrimaryKey(t => t.Id);

            this.CreateTable(
                "dbo.Results",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FinishTime = c.DateTime(nullable: false),
                        Time = c.Time(nullable: false, precision: 7),
                        TimeStr = c.String(),
                        ErrorText = c.String(),
                        PunchedControlsStr = c.String(),
                        SplitTimesStr = c.String(),
                        Error = c.Int(),
                        IsValid = c.Boolean(nullable: false),
                        Course_Id = c.Int(),
                        Runner_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.Course_Id)
                .ForeignKey("dbo.Runners", t => t.Runner_Id)
                .Index(t => t.Course_Id)
                .Index(t => t.Runner_Id);

            this.CreateTable(
                "dbo.Runners",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Position = c.Int(nullable: false),
                        Name = c.String(),
                        SiCard = c.Long(nullable: false),
                        DropedOut = c.Boolean(nullable: false),
                        Team_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.Team_Id)
                .Index(t => t.Team_Id);

            this.CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Category = c.String(),
                    })
                .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
            this.DropForeignKey("dbo.Results", "Runner_Id", "dbo.Runners");
            this.DropForeignKey("dbo.Runners", "Team_Id", "dbo.Teams");
            this.DropForeignKey("dbo.ControlDatas", "Result_Id", "dbo.Results");
            this.DropForeignKey("dbo.Results", "Course_Id", "dbo.Courses");
            this.DropIndex("dbo.Runners", new[] { "Team_Id" });
            this.DropIndex("dbo.Results", new[] { "Runner_Id" });
            this.DropIndex("dbo.Results", new[] { "Course_Id" });
            this.DropIndex("dbo.ControlDatas", new[] { "Result_Id" });
            this.DropTable("dbo.Teams");
            this.DropTable("dbo.Runners");
            this.DropTable("dbo.Results");
            this.DropTable("dbo.Courses");
            this.DropTable("dbo.ControlDatas");
        }
    }
}
