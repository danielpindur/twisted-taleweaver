using FluentMigrator;

namespace TwistedTaleweaver.Database.Migrations;

[Migration(0009)]
public class ExpeditionOutcomesSchema : Migration
{
    public override void Up()
    {
        Create.Table("expedition_outcome_results")
            .WithColumn("result_id")
                .AsByte()
                .PrimaryKey()
            .WithColumn("result_name")
                .AsString(20)
                .NotNullable()
                .Unique();

        Insert.IntoTable("expedition_outcome_results")
            .Row(new { result_id = 1, result_name = "Success" })
            .Row(new { result_id = 2, result_name = "Failure" });

        Create.Table("expedition_outcomes")
            .WithColumn("expedition_id")
                .AsGuid()
                .PrimaryKey()
                .NotNullable()
                .ForeignKey("expeditions", "expedition_id")
            .WithColumn("result_id")
                .AsByte()
                .NotNullable()
                .ForeignKey("expedition_outcome_results", "result_id")
            .WithColumn("narrations")
                .AsCustom("json")
                .NotNullable()
            .WithColumn("encounters")
                .AsCustom("jsonb")
                .NotNullable();
    }

    public override void Down()
    {
        Delete.Table("expedition_outcomes");
        Delete.Table("expedition_outcome_results");
    }
}