using FluentMigrator;
using FluentMigrator.Postgres;

namespace TwistedTaleweaver.Database.Migrations;

[Migration(0006)]
public class OutboxSchema : Migration
{
    public override void Up()
    {
        Create.Table("outbox_entries")
            .WithColumn("outbox_entry_id")
                .AsGuid()
                .PrimaryKey()
                .WithDefault(SystemMethods.NewGuid)
            .WithColumn("created_at")
                .AsDateTimeOffset()
                .NotNullable()
                .WithDefault(SystemMethods.CurrentDateTimeOffset)
            .WithColumn("topic")
                .AsString(255)
                .NotNullable()
            .WithColumn("partition_key")
                .AsString(255)
                .Nullable()
            .WithColumn("headers")
                .AsString()
                .Nullable()
            .WithColumn("payload")
                .AsString()
                .NotNullable()
            .WithColumn("processed_at")
                .AsDateTimeOffset()
                .Nullable()
            .WithColumn("remaining_attempts")
                .AsByte()
                .NotNullable();
        
        Create.Index("ix_outbox_unprocessed_created_at")
            .OnTable("outbox_entries")
                .OnColumn("created_at").Ascending()
                .WithOptions()
                    .Filter("processed_at IS NULL");
    }

    public override void Down()
    {
        Delete.Table("outbox_entries");
    }
}