namespace SocialEngineeringExercise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class INITAL1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SocialEnginnringReplyModels", "HostUrlRoot", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SocialEnginnringReplyModels", "HostUrlRoot");
        }
    }
}
