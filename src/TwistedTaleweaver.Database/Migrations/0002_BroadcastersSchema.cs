using FluentMigrator;

namespace TwistedTaleweaver.Database.Migrations;

[Migration(0002)]
public class BroadcastersSchema : Migration
{
    public override void Up()
    {
        Create.Table("broadcasters")
            .WithColumn("user_id")
                .AsGuid()
                .PrimaryKey()
                .ForeignKey("users", "user_id");
    }

    public override void Down()
    {
        Delete.Table("broadcasters");
    }
}