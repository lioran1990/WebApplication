namespace ShaulisBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationOmriNight : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BlogComments", "CommentDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BlogComments", "CommentDate");
        }
    }
}
