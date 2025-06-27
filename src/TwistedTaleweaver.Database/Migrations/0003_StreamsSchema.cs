using FluentMigrator;

namespace TwistedTaleweaver.Database.Migrations;

[Migration(0003)]
public class StreamsSchema : Migration
{
    public override void Up()
    {
        Create.Table("streams")
            .WithColumn("stream_id")
                .AsGuid()
                .PrimaryKey()
                .WithDefault(SystemMethods.NewGuid)
            .WithColumn("broadcaster_user_id")
                .AsGuid()
                .NotNullable()
                .ForeignKey("broadcasters", "user_id")
            .WithColumn("external_stream_id")
                .AsString(36)
                .NotNullable()
                .Unique()
            .WithColumn("started_at")
                .AsDateTime()
                .NotNullable()
            .WithColumn("ended_at")
                .AsDateTime()
                .Nullable();

        Create.Index("IX_streams_broadcaster_started")
            .OnTable("streams")
                .OnColumn("user_id").Ascending()
                .OnColumn("started_at").Ascending();
    }

    public override void Down()
    {
        Delete.Table("streams");
    }
}