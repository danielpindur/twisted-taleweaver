using FluentMigrator;

namespace TwistedTaleweaver.Database.Migrations;

[Migration(0005)]
public class ExpeditionTurnOrdersSchema : Migration
{
    public override void Up()
    {
        Create.Table("expedition_turn_orders")
            .WithColumn("expedition_id")
                .AsGuid()
                .PrimaryKey()
                .ForeignKey("expeditions", "expedition_id")
            .WithColumn("current_turn_index")
                .AsInt32()
                .NotNullable()
            .WithColumn("awaiting_action")
                .AsBoolean()
                .NotNullable()
            .WithColumn("turns")
                .AsCustom("jsonb")
                .NotNullable();
    }

    public override void Down()
    {
        Delete.Table("expedition_turn_orders");
    }
}