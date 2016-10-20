namespace SocialEngineeringExercise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SMTP : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SmtpConfigModels", "Address", c => c.String());
            AddColumn("dbo.SmtpConfigModels", "DisplayName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SmtpConfigModels", "DisplayName");
            DropColumn("dbo.SmtpConfigModels", "Address");
        }
    }
}
