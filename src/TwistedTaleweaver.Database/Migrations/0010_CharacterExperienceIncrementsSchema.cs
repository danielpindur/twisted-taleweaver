using FluentMigrator;

namespace TwistedTaleweaver.Database.Migrations;

[Migration(0010)]
public class CharacterExperienceIncrementsSchema : Migration
{
    public override void Up()
    {
        Create.Table("character_experience_increments")
            .WithColumn("character_experience_increment_id")
                .AsInt64()
                .PrimaryKey()
                .Identity()
            .WithColumn("character_id")
                .AsGuid()
                .NotNullable()
                .ForeignKey("characters", "character_id")
            .WithColumn("amount")
                .AsInt32()
                .NotNullable()
            .WithColumn("created_at")
                .AsDateTimeOffset()
                .NotNullable()
                .WithDefault(SystemMethods.CurrentDateTimeOffset)
            .WithColumn("expedition_id")
                .AsGuid()
                .Nullable()
                .ForeignKey("expeditions", "expedition_id");

        Create.Index("IX_character_experience_increments_character_id")
            .OnTable("character_experience_increments")
            .OnColumn("character_id");
    }

    public override void Down()
    {
        Delete.Table("character_experience_increments");
    }
}