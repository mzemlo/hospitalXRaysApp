namespace hospitalAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foreignKeyInPatients : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Patients", name: "ApplicationUser_Id", newName: "UserId");
            RenameIndex(table: "dbo.Patients", name: "IX_ApplicationUser_Id", newName: "IX_UserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Patients", name: "IX_UserId", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.Patients", name: "UserId", newName: "ApplicationUser_Id");
        }
    }
}
