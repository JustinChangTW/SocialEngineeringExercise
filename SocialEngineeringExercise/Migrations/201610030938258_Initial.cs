namespace SocialEngineeringExercise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SocialEnginnringReplies",
                c => new
                    {
                        SocialEnginnringGuid = c.Guid(nullable: false),
                        EmployeeNo = c.String(),
                        EmployeeName = c.String(),
                        EmployeeEmail = c.String(),
                        Remark = c.String(),
                        ClickTime = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SocialEnginnringGuid);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SocialEnginnringReplies");
        }
    }
}
