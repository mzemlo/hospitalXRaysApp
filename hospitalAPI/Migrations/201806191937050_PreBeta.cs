namespace hospitalAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PreBeta : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PatientDiseases", "Patient_Id", "dbo.Patients");
            DropForeignKey("dbo.PatientDiseases", "Disease_Id", "dbo.Diseases");
            DropIndex("dbo.PatientDiseases", new[] { "Patient_Id" });
            DropIndex("dbo.PatientDiseases", new[] { "Disease_Id" });
            DropTable("dbo.Diseases");
            DropTable("dbo.PatientDiseases");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PatientDiseases",
                c => new
                    {
                        Patient_Id = c.Int(nullable: false),
                        Disease_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Patient_Id, t.Disease_Id });
            
            CreateTable(
                "dbo.Diseases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Dexcription = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.PatientDiseases", "Disease_Id");
            CreateIndex("dbo.PatientDiseases", "Patient_Id");
            AddForeignKey("dbo.PatientDiseases", "Disease_Id", "dbo.Diseases", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PatientDiseases", "Patient_Id", "dbo.Patients", "Id", cascadeDelete: true);
        }
    }
}
