using FluentMigrator;

namespace TwistedTaleweaver.Database.Migrations;

[Migration(0005)]
public class Expeditions_MinParticipants : Migration
{
    public override void Up()
    {
        Alter.Table("expeditions")
            .AddColumn("min_participants")
            .AsByte()
            .Nullable();

        Update.Table("expeditions")
            .Set(new { min_participants = 1 })
            .AllRows();
        
        Alter.Table("expeditions")
            .AlterColumn("min_participants")
            .AsByte()
            .NotNullable();
    }

    public override void Down()
    {
        Delete.Column("min_participants").FromTable("expeditions");
    }
}