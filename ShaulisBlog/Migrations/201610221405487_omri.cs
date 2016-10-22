namespace ShaulisBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class omri : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fans", "_address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Fans", "_address");
        }
    }
}
