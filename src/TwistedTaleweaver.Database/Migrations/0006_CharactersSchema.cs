using FluentMigrator;

namespace TwistedTaleweaver.Database.Migrations;

[Migration(0006)]
public class CharactersSchema : Migration
{
    public override void Up()
    {
        Create.Table("characters")
            .WithColumn("character_id")
                .AsGuid()
                .PrimaryKey()
                .WithDefault(SystemMethods.NewGuid)
            .WithColumn("user_id")
                .AsGuid()
                .NotNullable()
                .ForeignKey("users", "user_id")
            .WithColumn("broadcaster_user_id")
                .AsGuid()
                .NotNullable()
                .ForeignKey("users", "user_id")
            .WithColumn("created_at")
                .AsDateTimeOffset()
                .NotNullable()
                .WithDefault(SystemMethods.CurrentDateTimeOffset)
            .WithColumn("updated_at")
                .AsDateTimeOffset()
                .NotNullable()
                .WithDefault(SystemMethods.CurrentDateTimeOffset);

        Create.Index("IX_characters_user_broadcaster_status")
            .OnTable("characters")
            .OnColumn("user_id").Ascending()
            .OnColumn("broadcaster_user_id").Ascending();
    }

    public override void Down()
    {
        Delete.Table("characters");
    }
}