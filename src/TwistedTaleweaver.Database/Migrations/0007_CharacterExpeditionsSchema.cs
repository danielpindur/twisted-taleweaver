using FluentMigrator;

namespace TwistedTaleweaver.Database.Migrations;

[Migration(0007)]
public class CharacterExpeditionsSchema : Migration
{
    public override void Up()
    {
        Create.Table("character_expeditions")
            .WithColumn("character_id")
                .AsGuid()
                .NotNullable()
                .ForeignKey("characters", "character_id")
            .WithColumn("expedition_id")
                .AsGuid()
                .NotNullable()
                .ForeignKey("expeditions", "expedition_id")
            .WithColumn("joined_at")
                .AsDateTimeOffset()
                .NotNullable()
                .WithDefault(SystemMethods.CurrentDateTimeOffset);
        
        Create.PrimaryKey("PK_character_expeditions")
            .OnTable("character_expeditions")
            .Columns("character_id", "expedition_id");

        Create.Index("IX_character_expeditions_expedition")
            .OnTable("character_expeditions")
            .OnColumn("expedition_id").Ascending();
    }

    public override void Down()
    {
        Delete.Table("character_expeditions");
    }
}