using FluentMigrator;

namespace TwistedTaleweaver.Database.Migrations;

[Migration(0002)]
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
            .Row(new { status_id = 3, status_name = "Completed" });

        Create.Table("expedition_difficulties")
            .WithColumn("difficulty_id")
                .AsByte()
                .PrimaryKey()
            .WithColumn("difficulty_name")
                .AsString(20)
                .NotNullable()
                .Unique();

        Insert.IntoTable("expedition_difficulties")
            .Row(new { difficulty_id = 1, difficulty_name = "Easy" })
            .Row(new { difficulty_id = 2, difficulty_name = "Medium" })
            .Row(new { difficulty_id = 3, difficulty_name = "Hard" })
            .Row(new { difficulty_id = 4, difficulty_name = "Nightmare" });

        Create.Table("expedition_outcomes")
            .WithColumn("outcome_id")
                .AsByte()
                .PrimaryKey()
            .WithColumn("outcome_name")
                .AsString(20)
                .NotNullable()
                .Unique();

        Insert.IntoTable("expedition_outcomes")
            .Row(new { outcome_id = 1, outcome_name = "Success" })
            .Row(new { outcome_id = 2, outcome_name = "Failure" });

        Create.Table("expeditions")
            .WithColumn("expedition_id")
                .AsGuid()
                .PrimaryKey()
                .WithDefault(SystemMethods.NewGuid)
            .WithColumn("expedition_name")
                .AsString(100)
                .NotNullable()
            .WithColumn("description")
                .AsString(int.MaxValue)
                .NotNullable()
            .WithColumn("status_id")
                .AsByte()
                .NotNullable()
                .WithDefaultValue(1) // Default to "Created"
                .ForeignKey("expedition_statuses", "status_id")
            .WithColumn("difficulty_id")
                .AsByte()
                .NotNullable()
                .ForeignKey("expedition_difficulties", "difficulty_id")
            .WithColumn("max_participants")
                .AsByte()
                .NotNullable()
            .WithColumn("min_participants")
                .AsByte()
                .NotNullable()
            .WithColumn("outcome_id")
                .AsByte()
                .Nullable()
                .ForeignKey("expedition_outcomes", "outcome_id")
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
            .WithColumn("updated_at")
                .AsDateTimeOffset()
                .NotNullable()
                .WithDefault(SystemMethods.CurrentDateTimeOffset);

        Create.Index("idx_expeditions_status_id")
            .OnTable("expeditions")
                .OnColumn("status_id").Ascending();

        Create.Table("expedition_participants")
            .WithColumn("expedition_id")
                .AsGuid()
                .NotNullable()
                .ForeignKey("expeditions", "expedition_id")
            .WithColumn("user_id")
                .AsGuid()
                .NotNullable()
                .ForeignKey("users", "user_id")
            .WithColumn("joined_at")
                .AsDateTimeOffset()
                .NotNullable()
                .WithDefault(SystemMethods.CurrentDateTimeOffset);

        Create.PrimaryKey("PK_expedition_participants")
            .OnTable("expedition_participants")
            .Columns("expedition_id", "user_id");
    }

    public override void Down()
    {
        Delete.Table("expedition_participants");
        Delete.Table("expeditions");
        Delete.Table("expedition_outcomes");
        Delete.Table("expedition_difficulties");
        Delete.Table("expedition_statuses");
    }
}