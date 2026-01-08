using FluentMigrator;
using FluentMigrator.Postgres;

namespace TwistedTaleweaver.Database.Migrations;

[Migration(0007)]
public class MonstersSchema : Migration
{
    public override void Up()
    {
        Create.Table("monsters")
            .WithColumn("creature_id")
                .AsGuid()
                .NotNullable()
                .PrimaryKey()
                .ForeignKey("creatures", "creature_id")
            .WithColumn("expedition_id")
                .AsGuid()
                .NotNullable()
                .ForeignKey("expeditions", "expedition_id")
            .WithColumn("challenge_rating")
                .AsByte()
                .NotNullable();

        Create.Index("ix_monsters_expedition_id")
            .OnTable("monsters")
            .OnColumn("expedition_id");
    }

    public override void Down()
    {
        Delete.Table("monsters");
    }
}