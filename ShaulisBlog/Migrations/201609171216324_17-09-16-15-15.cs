namespace ShaulisBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1709161515 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlogComments",
                c => new
                    {
                        CommentID = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        _title = c.String(),
                        _author = c.String(),
                        _websiteOfAuthor = c.String(),
                        _text = c.String(),
                    })
                .PrimaryKey(t => t.CommentID)
                .ForeignKey("dbo.BlogPosts", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId);
            
            CreateTable(
                "dbo.BlogPosts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        _title = c.String(nullable: false),
                        _author = c.String(nullable: false),
                        _websiteOfAuthor = c.String(nullable: false),
                        _releaseDate = c.DateTime(nullable: false),
                        _text = c.String(nullable: false),
                        _image = c.String(),
                        _video = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Fans",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        _firstName = c.String(nullable: false),
                        _lastName = c.String(nullable: false),
                        _gender = c.Int(nullable: false),
                        _birthDate = c.DateTime(nullable: false),
                        _seniority = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BlogComments", "PostId", "dbo.BlogPosts");
            DropIndex("dbo.BlogComments", new[] { "PostId" });
            DropTable("dbo.Fans");
            DropTable("dbo.BlogPosts");
            DropTable("dbo.BlogComments");
        }
    }
}
