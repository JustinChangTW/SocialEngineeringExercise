namespace SocialEngineeringExercise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddController : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SocialEnginnringReplies", "ClickTime", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SocialEnginnringReplies", "ClickTime", c => c.Boolean(nullable: false));
        }
    }
}
