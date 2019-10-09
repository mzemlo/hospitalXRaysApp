namespace hospitalAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatingDataBase : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PatientDiseases", "Patient_Id", "dbo.Patients");
            DropForeignKey("dbo.PatientDiseases", "Disease_Id", "dbo.Diseases");
            DropForeignKey("dbo.Patients", "UserId", "dbo.Users");
            DropIndex("dbo.Patients", new[] { "UserId" });
            DropIndex("dbo.PatientDiseases", new[] { "Patient_Id" });
            DropIndex("dbo.PatientDiseases", new[] { "Disease_Id" });
            AddColumn("dbo.Photos", "XrayPhotoBlobSource", c => c.String());
            AddColumn("dbo.Photos", "ColoredPhotoBlobSource", c => c.String());
            AddColumn("dbo.Photos", "IsColored", c => c.Boolean(nullable: false));
            AddColumn("dbo.Photos", "DiseaseName", c => c.String());
            AddColumn("dbo.Photos", "DiseaseDescription", c => c.String());
            AlterColumn("dbo.Patients", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Patients", "UserId");
            //AddForeignKey("dbo.Patients", "UserId", "dbo.AspNetUsers", "Id");
            DropTable("dbo.Diseases");
            DropTable("dbo.Users");
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
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Password = c.String(),
                        TypeOfAccount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Diseases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Dexcription = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Patients", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Patients", new[] { "UserId" });
            AlterColumn("dbo.Patients", "UserId", c => c.Int(nullable: false));
            DropColumn("dbo.Photos", "DiseaseDescription");
            DropColumn("dbo.Photos", "DiseaseName");
            DropColumn("dbo.Photos", "IsColored");
            DropColumn("dbo.Photos", "ColoredPhotoBlobSource");
            DropColumn("dbo.Photos", "XrayPhotoBlobSource");
            CreateIndex("dbo.PatientDiseases", "Disease_Id");
            CreateIndex("dbo.PatientDiseases", "Patient_Id");
            CreateIndex("dbo.Patients", "UserId");
            AddForeignKey("dbo.Patients", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PatientDiseases", "Disease_Id", "dbo.Diseases", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PatientDiseases", "Patient_Id", "dbo.Patients", "Id", cascadeDelete: true);
        }
    }
}
