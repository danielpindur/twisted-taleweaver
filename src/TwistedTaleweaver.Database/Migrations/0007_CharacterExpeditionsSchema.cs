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
    }

    public override void Down()
    {
        Delete.Table("character_expeditions");
    }
}