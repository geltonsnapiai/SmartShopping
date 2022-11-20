using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartShopping.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNamingAndId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceRecords_Products_ProductId",
                table: "PriceRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_PriceRecords_Shops_ShopId",
                table: "PriceRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductProductTag_ProductTags_TagsId",
                table: "ProductProductTag");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductProductTag_Products_ProductsId",
                table: "ProductProductTag");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductShop_Products_ProductsId",
                table: "ProductShop");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductShop_Shops_ShopsId",
                table: "ProductShop");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shops",
                table: "Shops");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductTags",
                table: "ProductTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PriceRecords",
                table: "PriceRecords");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Shops",
                newName: "Shop");

            migrationBuilder.RenameTable(
                name: "ProductTags",
                newName: "ProductTag");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Product");

            migrationBuilder.RenameTable(
                name: "PriceRecords",
                newName: "PriceRecord");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Id",
                table: "User",
                newName: "IX_User_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Email",
                table: "User",
                newName: "IX_User_Email");

            migrationBuilder.RenameIndex(
                name: "IX_Shops_Id",
                table: "Shop",
                newName: "IX_Shop_Id");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTags_Id",
                table: "ProductTag",
                newName: "IX_ProductTag_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Products_Id",
                table: "Product",
                newName: "IX_Product_Id");

            migrationBuilder.RenameIndex(
                name: "IX_PriceRecords_ShopId",
                table: "PriceRecord",
                newName: "IX_PriceRecord_ShopId");

            migrationBuilder.RenameIndex(
                name: "IX_PriceRecords_ProductId",
                table: "PriceRecord",
                newName: "IX_PriceRecord_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_PriceRecords_Id",
                table: "PriceRecord",
                newName: "IX_PriceRecord_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shop",
                table: "Shop",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductTag",
                table: "ProductTag",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PriceRecord",
                table: "PriceRecord",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PriceRecord_Product_ProductId",
                table: "PriceRecord",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PriceRecord_Shop_ShopId",
                table: "PriceRecord",
                column: "ShopId",
                principalTable: "Shop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProductTag_ProductTag_TagsId",
                table: "ProductProductTag",
                column: "TagsId",
                principalTable: "ProductTag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProductTag_Product_ProductsId",
                table: "ProductProductTag",
                column: "ProductsId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductShop_Product_ProductsId",
                table: "ProductShop",
                column: "ProductsId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductShop_Shop_ShopsId",
                table: "ProductShop",
                column: "ShopsId",
                principalTable: "Shop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceRecord_Product_ProductId",
                table: "PriceRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_PriceRecord_Shop_ShopId",
                table: "PriceRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductProductTag_ProductTag_TagsId",
                table: "ProductProductTag");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductProductTag_Product_ProductsId",
                table: "ProductProductTag");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductShop_Product_ProductsId",
                table: "ProductShop");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductShop_Shop_ShopsId",
                table: "ProductShop");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shop",
                table: "Shop");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductTag",
                table: "ProductTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PriceRecord",
                table: "PriceRecord");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Shop",
                newName: "Shops");

            migrationBuilder.RenameTable(
                name: "ProductTag",
                newName: "ProductTags");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "PriceRecord",
                newName: "PriceRecords");

            migrationBuilder.RenameIndex(
                name: "IX_User_Id",
                table: "Users",
                newName: "IX_Users_Id");

            migrationBuilder.RenameIndex(
                name: "IX_User_Email",
                table: "Users",
                newName: "IX_Users_Email");

            migrationBuilder.RenameIndex(
                name: "IX_Shop_Id",
                table: "Shops",
                newName: "IX_Shops_Id");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTag_Id",
                table: "ProductTags",
                newName: "IX_ProductTags_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Product_Id",
                table: "Products",
                newName: "IX_Products_Id");

            migrationBuilder.RenameIndex(
                name: "IX_PriceRecord_ShopId",
                table: "PriceRecords",
                newName: "IX_PriceRecords_ShopId");

            migrationBuilder.RenameIndex(
                name: "IX_PriceRecord_ProductId",
                table: "PriceRecords",
                newName: "IX_PriceRecords_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_PriceRecord_Id",
                table: "PriceRecords",
                newName: "IX_PriceRecords_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shops",
                table: "Shops",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductTags",
                table: "ProductTags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PriceRecords",
                table: "PriceRecords",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PriceRecords_Products_ProductId",
                table: "PriceRecords",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PriceRecords_Shops_ShopId",
                table: "PriceRecords",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProductTag_ProductTags_TagsId",
                table: "ProductProductTag",
                column: "TagsId",
                principalTable: "ProductTags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProductTag_Products_ProductsId",
                table: "ProductProductTag",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductShop_Products_ProductsId",
                table: "ProductShop",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductShop_Shops_ShopsId",
                table: "ProductShop",
                column: "ShopsId",
                principalTable: "Shops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
