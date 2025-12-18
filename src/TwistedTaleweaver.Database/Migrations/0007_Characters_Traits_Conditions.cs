using FluentMigrator;

namespace TwistedTaleweaver.Database.Migrations;

[Migration(0007)]
public class Characters_Traits_Conditions : Migration
{
    public override void Up()
    {
        Alter.Table("creature_traits")
            .AddColumn("condition_description")
            .AsString(255)
            .Nullable();

        Execute.Sql(@"
            UPDATE creature_traits ct
            SET condition_description = ctc.condition_description
            FROM creature_trait_conditions ctc
            WHERE ctc.trait_id = ct.trait_id");

        Delete.Table("creature_trait_conditions");
    }

    public override void Down()
    {
        Create.Table("creature_trait_conditions")
            .WithColumn("condition_id")
                .AsGuid()
                .PrimaryKey()
                .WithDefault(SystemMethods.NewGuid)
            .WithColumn("trait_id")
                .AsGuid()
                .NotNullable()
                .ForeignKey("creature_traits", "trait_id")
            .WithColumn("condition_description")
                .AsString(255)
                .NotNullable();
        
        Execute.Sql(@"
            INSERT INTO creature_trait_conditions
            (trait_id, condition_description)
            SELECT
                ct.condition_id,
                ct.condition_description
            FROM creature_traits ct
            WHERE ct.condition_description IS NOT NULL");
        
        Delete.Column("condition_description").FromTable("creature_traits");
    }
}