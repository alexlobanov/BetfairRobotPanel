namespace RobotWebPanel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change_Type : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RobotModels", "CreationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RobotModels", "CreationDate");
        }
    }
}
