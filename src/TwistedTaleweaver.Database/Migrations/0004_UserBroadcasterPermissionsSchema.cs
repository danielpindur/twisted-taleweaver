using FluentMigrator;

namespace TwistedTaleweaver.Database.Migrations;

[Migration(0004)]
public class UserBroadcasterPermissionsSchema : Migration
{
    public override void Up()
    {
        Create.Table("permission_types")
            .WithColumn("permission_type_id")
                .AsByte()
                .PrimaryKey()
            .WithColumn("permission_type_name")
                .AsString(50)
                .NotNullable()
                .Unique();

        Insert.IntoTable("permission_types")
            .Row(new { permission_type_id = 1, permission_type_name = "ManageExpeditions" });
        
        Create.Table("user_broadcaster_permissions")
            .WithColumn("permission_id")
                .AsGuid()
                .PrimaryKey()
                .WithDefault(SystemMethods.NewGuid)
            .WithColumn("user_id")
                .AsGuid()
                .NotNullable()
                .ForeignKey("users", "user_id")
            .WithColumn("broadcaster_user_id")
                .AsGuid()
                .NotNullable()
                .ForeignKey("broadcasters", "user_id")
            .WithColumn("permission_type_id")
                .AsByte()
                .NotNullable()
                .ForeignKey("permission_types", "permission_type_id")
            .WithColumn("is_active")
                .AsBoolean()
                .NotNullable()
                .WithDefaultValue(true)
            .WithColumn("granted_at")
                .AsDateTime()
                .NotNullable()
                .WithDefault(SystemMethods.CurrentDateTime)
            .WithColumn("granted_by_user_id")
                .AsGuid()
                .NotNullable()
                .ForeignKey("users", "user_id")
            .WithColumn("revoked_at")
                .AsDateTime()
                .Nullable()
            .WithColumn("revoked_by_user_id")
                .AsGuid()
                .Nullable()
                .ForeignKey("users", "user_id");
        
        Create.Index("IX_user_broadcaster_permissions_user_broadcaster_active")
            .OnTable("user_broadcaster_permissions")
                .OnColumn("user_id").Ascending()
                .OnColumn("broadcaster_user_id").Ascending()
                .OnColumn("is_active").Ascending();
    }

    public override void Down()
    {
        Delete.Table("user_broadcaster_permissions");
        Delete.Table("permission_types");
    }
}