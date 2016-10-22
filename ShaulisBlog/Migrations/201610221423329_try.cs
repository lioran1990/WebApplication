namespace ShaulisBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _try : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Fans", "_address", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Fans", "_address", c => c.String());
        }
    }
}
