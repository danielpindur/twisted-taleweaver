using FluentMigrator;

namespace TwistedTaleweaver.Database.Migrations;

[Migration(0001)]
public class UsersSchema : Migration
{
    public override void Up()
    {
        Execute.Sql("CREATE EXTENSION IF NOT EXISTS \"uuid-ossp\";");
        
        Create.Table("users")
            .WithColumn("user_id")
                .AsGuid()
                .PrimaryKey()
                .WithDefault(SystemMethods.NewGuid)
            .WithColumn("external_user_id")
                .AsString(36)
                .NotNullable()
                .Unique()
            .WithColumn("created_at")
                .AsDateTimeOffset()
                .NotNullable()
                .WithDefault(SystemMethods.CurrentDateTimeOffset);
    }

    public override void Down()
    {
        Delete.Table("users");
        
        Execute.Sql("DROP EXTENSION IF EXISTS \"uuid-ossp\";"); 
    }
}