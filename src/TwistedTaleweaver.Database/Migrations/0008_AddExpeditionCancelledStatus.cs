using FluentMigrator;

namespace TwistedTaleweaver.Database.Migrations;

[Migration(0008)]
public class AddExpeditionCancelledStatus : Migration
{
    public override void Up()
    {
        Insert.IntoTable("expedition_statuses")
            .Row(new { status_id = 5, status_name = "Cancelled" });

        Alter.Table("expeditions")
            .AddColumn("cancelled_at")
                .AsDateTimeOffset()
                .Nullable();
    }

    public override void Down()
    {
        Delete.FromTable("expedition_statuses")
            .Row(new { status_id = 5 });

        Delete.Column("cancelled_at").FromTable("expeditions");
    }
}