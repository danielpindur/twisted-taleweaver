using FluentMigrator;

namespace TwistedTaleweaver.Database.Migrations;

[Migration(0005)]
public class ExpeditionsSchema : Migration
{
    public override void Up()
    {
        Create.Table("expedition_statuses")
            .WithColumn("status_id")
                .AsByte()
                .PrimaryKey()
            .WithColumn("status_name")
                .AsString(20)
                .NotNullable()
                .Unique();
        
        Insert.IntoTable("expedition_statuses")
            .Row(new { status_id = 1, status_name = "Created" })
            .Row(new { status_id = 2, status_name = "Started" })
            .Row(new { status_id = 3, status_name = "Completed" })
            .Row(new { status_id = 4, status_name = "Failed" });
        
        Create.Table("expeditions")
            .WithColumn("expedition_id")
                .AsGuid()
                .PrimaryKey()
                .WithDefault(SystemMethods.NewGuid)
            .WithColumn("stream_id")
                .AsGuid()
                .NotNullable()
                .ForeignKey("streams", "stream_id")
            .WithColumn("status_id")
                .AsByte()
                .NotNullable()
                .WithDefaultValue(1) // Default to "Created"
                .ForeignKey("expedition_statuses", "status_id")
            .WithColumn("created_by_user_id")
                .AsGuid()
                .NotNullable()
                .ForeignKey("users", "user_id")
            .WithColumn("created_at")
                .AsDateTimeOffset()
                .NotNullable()
                .WithDefault(SystemMethods.CurrentDateTimeOffset)
            .WithColumn("started_at")
                .AsDateTimeOffset()
                .Nullable()
            .WithColumn("completed_at")
                .AsDateTimeOffset()
                .Nullable()
            .WithColumn("failed_at")
                .AsDateTimeOffset()
                .Nullable()
            .WithColumn("updated_at")
                .AsDateTimeOffset()
                .NotNullable()
                .WithDefault(SystemMethods.CurrentDateTimeOffset);

        Create.Index("IX_expeditions_stream_status")
            .OnTable("expeditions")
                .OnColumn("stream_id").Ascending()
                .OnColumn("status_id").Ascending();
    }

    public override void Down()
    {
        Delete.Table("expeditions");
        Delete.Table("expedition_statuses");
    }
}