using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
INSERT INTO [dbo].[AspNetUsers]
           ([Id]
           ,[UserName]
           ,[NormalizedUserName]
           ,[Email]
           ,[NormalizedEmail]
           ,[EmailConfirmed]
           ,[PasswordHash]
           ,[SecurityStamp]
           ,[ConcurrencyStamp]
           ,[PhoneNumber]
           ,[PhoneNumberConfirmed]
           ,[TwoFactorEnabled]
           ,[LockoutEnd]
           ,[LockoutEnabled]
           ,[AccessFailedCount])
     VALUES
           ('b1132a1a-4927-4b03-974e-bb0ecfca11ef'
           ,'test'
           ,'TEST'
           ,'t@t.com'
           ,'T@T.COM'
           ,0
           ,'AQAAAAEAACcQAAAAELr8xgqSPZBgP8JEjq6aHGcKCuMFgOb+9SY7QesqX9Hhe9f36QCnE2WrFUaWLwX01g=='
           ,'P2FCD7HJY7HOOUFZR772EVCR5L7JM3XR'
           ,'be6429e8-032d-46ed-a726-8ac8b6b6226a'
           ,null
           ,0
           ,0
           ,null
           ,1
           ,0);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"delete from [dbo].[AspNetUsers] where Id = 'b1132a1a-4927-4b03-974e-bb0ecfca11ef';");
        }
    }
}
