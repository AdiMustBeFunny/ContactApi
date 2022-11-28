using Microsoft.EntityFrameworkCore.Migrations;
using Model.Providers;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //three basic categories
            migrationBuilder.Sql(@"
            INSERT INTO [dbo].[Categories] ([Id],[Name],[ParentCategoryId],[X_CreateTime],[X_Remove_Time]) " +
            $"VALUES('{CategoryIdProvider.BusinessCategoryId}','Służbowy',null,getdate(),null)");

            migrationBuilder.Sql(@"
            INSERT INTO [dbo].[Categories] ([Id],[Name],[ParentCategoryId],[X_CreateTime],[X_Remove_Time]) " +
            $"VALUES('{CategoryIdProvider.PrivateCategoryId}','Prywatny',null,getdate(),null)");

            migrationBuilder.Sql(@"
            INSERT INTO [dbo].[Categories] ([Id],[Name],[ParentCategoryId],[X_CreateTime],[X_Remove_Time]) " +
            $"VALUES('{CategoryIdProvider.CustomCategoryId}','Inny',null,getdate(),null)");

            //two subcategories of BusinessCategory
            migrationBuilder.Sql(@"
            INSERT INTO [dbo].[Categories] ([Id],[Name],[ParentCategoryId],[X_CreateTime],[X_Remove_Time]) " +
            $"VALUES('{CategoryIdProvider.BossCategoryId}','Szef','{CategoryIdProvider.BusinessCategoryId}',getdate(),null)");

            migrationBuilder.Sql(@"
            INSERT INTO [dbo].[Categories] ([Id],[Name],[ParentCategoryId],[X_CreateTime],[X_Remove_Time]) " +
            $"VALUES('{CategoryIdProvider.ClientCategoryId}','Klient','{CategoryIdProvider.BusinessCategoryId}',getdate(),null)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"delete from [dbo].[Categories] where Id = '{CategoryIdProvider.BossCategoryId}'");
            migrationBuilder.Sql($"delete from [dbo].[Categories] where Id = '{CategoryIdProvider.ClientCategoryId}'");
            migrationBuilder.Sql($"delete from [dbo].[Categories] where Id = '{CategoryIdProvider.CustomCategoryId}'");
            migrationBuilder.Sql($"delete from [dbo].[Categories] where Id = '{CategoryIdProvider.PrivateCategoryId}'");
            migrationBuilder.Sql($"delete from [dbo].[Categories] where Id = '{CategoryIdProvider.BusinessCategoryId}'");
        }
    }
}
