namespace SocialEngineeringExercise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class INITAL : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SmtpConfigModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Host = c.String(),
                        Port = c.Int(nullable: false),
                        Password = c.String(),
                        EnableSsl = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.EmailMessageModels", "AttachmentInline", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EmailMessageModels", "AttachmentInline", c => c.String());
            DropTable("dbo.SmtpConfigModels");
        }
    }
}
