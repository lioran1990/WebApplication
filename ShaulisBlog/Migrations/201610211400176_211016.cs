namespace ShaulisBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _211016 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CommentToFans", "comment_CommentID", "dbo.BlogComments");
            DropForeignKey("dbo.CommentToFans", "fan_ID", "dbo.Fans");
            DropIndex("dbo.CommentToFans", new[] { "comment_CommentID" });
            DropIndex("dbo.CommentToFans", new[] { "fan_ID" });
            DropTable("dbo.CommentToFans");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CommentToFans",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        comment_CommentID = c.Int(),
                        fan_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.CommentToFans", "fan_ID");
            CreateIndex("dbo.CommentToFans", "comment_CommentID");
            AddForeignKey("dbo.CommentToFans", "fan_ID", "dbo.Fans", "ID");
            AddForeignKey("dbo.CommentToFans", "comment_CommentID", "dbo.BlogComments", "CommentID");
        }
    }
}
