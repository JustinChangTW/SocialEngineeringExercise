namespace SocialEngineeringExercise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SMTP2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmailMessageModels", "Address", c => c.String());
            AddColumn("dbo.EmailMessageModels", "DisplayName", c => c.String());
            DropColumn("dbo.SmtpConfigModels", "Address");
            DropColumn("dbo.SmtpConfigModels", "DisplayName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SmtpConfigModels", "DisplayName", c => c.String());
            AddColumn("dbo.SmtpConfigModels", "Address", c => c.String());
            DropColumn("dbo.EmailMessageModels", "DisplayName");
            DropColumn("dbo.EmailMessageModels", "Address");
        }
    }
}
