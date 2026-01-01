using FluentMigrator;

namespace TwistedTaleweaver.Database.Migrations;

[Migration(0004)]
public class MessagesSchema : Migration
{
    public override void Up()
    {
        Create.Table("message_types")
            .WithColumn("type_id")
                .AsByte()
                .PrimaryKey()
            .WithColumn("type_name")
                .AsString(20)
                .NotNullable()
                .Unique();

        Insert.IntoTable("message_types")
            .Row(new { type_id = 1, type_name = "Narrative" })
            .Row(new { type_id = 2, type_name = "Action" })
            .Row(new { type_id = 3, type_name = "System" });

        Create.Table("messages")
            .WithColumn("message_id")
                .AsInt64()
                .Identity()
                .PrimaryKey()
            .WithColumn("expedition_id")
                .AsGuid()
                .NotNullable()
                .ForeignKey("expeditions", "expedition_id")
            .WithColumn("sender_character_id")
                .AsGuid()
                .Nullable()
                .ForeignKey("characters", "creature_id")
            .WithColumn("type_id")
                .AsByte()
                .NotNullable()
                .ForeignKey("message_types", "type_id")
            .WithColumn("content")
                .AsString()
                .NotNullable()
            .WithColumn("created_at")
                .AsDateTimeOffset()
                .NotNullable()
                .WithDefault(SystemMethods.CurrentDateTimeOffset);
        
        Create.Index("idx_messages_expedition_created_at")
            .OnTable("messages")
                .OnColumn("expedition_id").Ascending()
                .OnColumn("created_at").Ascending()
                .OnColumn("type_id").Ascending();
    }

    public override void Down()
    {
        Delete.Table("messages");
        Delete.Table("message_types");
    }
}