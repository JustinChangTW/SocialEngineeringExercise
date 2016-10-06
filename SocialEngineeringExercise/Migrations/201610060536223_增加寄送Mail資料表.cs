namespace SocialEngineeringExercise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class 增加寄送Mail資料表 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.SocialEnginnringReplies", newName: "SocialEnginnringReplyModels");
            CreateTable(
                "dbo.EmailMessageModels",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        Subject = c.String(),
                        IsBodyHtml = c.Boolean(nullable: false),
                        Body = c.String(),
                        Prority = c.String(),
                        MailEncoding = c.String(),
                        Attachment = c.String(),
                        AttachmentInline = c.String(),
                    })
                .PrimaryKey(t => t.Guid);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EmailMessageModels");
            RenameTable(name: "dbo.SocialEnginnringReplyModels", newName: "SocialEnginnringReplies");
        }
    }
}
