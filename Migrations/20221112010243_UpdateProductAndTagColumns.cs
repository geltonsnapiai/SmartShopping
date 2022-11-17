using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartShopping.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductAndTagColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "ProductTags",
                newName: "DisplayName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Products",
                newName: "DisplayName");

            migrationBuilder.AddColumn<string>(
                name: "SimplifiedName",
                table: "ProductTags",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SimplifiedName",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SimplifiedName",
                table: "ProductTags");

            migrationBuilder.DropColumn(
                name: "SimplifiedName",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "DisplayName",
                table: "ProductTags",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "DisplayName",
                table: "Products",
                newName: "Name");
        }
    }
}
