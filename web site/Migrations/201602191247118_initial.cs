namespace web_site.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assignments",
                c => new
                    {
                        TaskID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TaskID, t.UserID })
                .ForeignKey("dbo.Tasks", t => t.TaskID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.TaskID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        TaskID = c.Int(nullable: false, identity: true),
                        BeginDateTime = c.DateTime(nullable: false),
                        DeadlineDateTime = c.DateTime(nullable: false),
                        Title = c.String(),
                        Requirements = c.String(),
                    })
                .PrimaryKey(t => t.TaskID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Assignments", "UserID", "dbo.Users");
            DropForeignKey("dbo.Assignments", "TaskID", "dbo.Tasks");
            DropIndex("dbo.Assignments", new[] { "UserID" });
            DropIndex("dbo.Assignments", new[] { "TaskID" });
            DropTable("dbo.Users");
            DropTable("dbo.Tasks");
            DropTable("dbo.Assignments");
        }
    }
}
