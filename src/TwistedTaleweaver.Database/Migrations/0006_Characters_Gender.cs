using FluentMigrator;

namespace TwistedTaleweaver.Database.Migrations;

[Migration(0006)]
public class Characters_Gender : Migration
{
    public override void Up()
    {
        Create.Table("creature_genders")
            .WithColumn("gender_id")
                .AsByte()
                .PrimaryKey()
            .WithColumn("gender_name")
                .AsString(20)
                .NotNullable()
                .Unique();

        Insert.IntoTable("creature_genders")
            .Row(new { gender_id = 1, gender_name = "Male" })
            .Row(new { gender_id = 2, gender_name = "Female" })
            .Row(new { gender_id = 3, gender_name = "NonBinary" })
            .Row(new { gender_id = 4, gender_name = "Other" });
        
        Alter.Table("creatures")
            .AddColumn("gender_id")
            .AsByte()
            .Nullable();

        Update.Table("creatures")
            .Set(new { gender_id = 1 })
            .AllRows();
        
        Alter.Table("creatures")
            .AlterColumn("gender_id")
            .AsByte()
            .NotNullable();
    }

    public override void Down()
    {
        Delete.Column("gender_id").FromTable("creatures");
    }
}