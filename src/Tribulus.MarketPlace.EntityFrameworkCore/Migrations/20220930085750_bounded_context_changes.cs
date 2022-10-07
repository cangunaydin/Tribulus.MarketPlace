using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tribulus.MarketPlace.Migrations
{
    public partial class bounded_context_changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppOrderItems_AppProducts_ProductId",
                table: "AppOrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_AppOrders_AbpUsers_OwnerUserId",
                table: "AppOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_AppProducts_AbpUsers_OwnerUserId",
                table: "AppProducts");

            migrationBuilder.DropIndex(
                name: "IX_AppProducts_OwnerUserId",
                table: "AppProducts");

            migrationBuilder.DropIndex(
                name: "IX_AppOrders_OwnerUserId",
                table: "AppOrders");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "StockCount",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "StockState",
                table: "AppProducts");

            migrationBuilder.CreateTable(
                name: "AppOrderItemQuantities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppOrderItemQuantities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppProductPrices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProductPrices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppProductStocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StockCount = table.Column<int>(type: "int", nullable: false),
                    StockState = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProductStocks", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrderItems_AppProductPrices_ProductId",
                table: "AppOrderItems",
                column: "ProductId",
                principalTable: "AppProductPrices",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppOrderItems_AppProductPrices_ProductId",
                table: "AppOrderItems");

            migrationBuilder.DropTable(
                name: "AppOrderItemQuantities");

            migrationBuilder.DropTable(
                name: "AppProductPrices");

            migrationBuilder.DropTable(
                name: "AppProductStocks");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "AppProducts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "StockCount",
                table: "AppProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StockState",
                table: "AppProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AppProducts_OwnerUserId",
                table: "AppProducts",
                column: "OwnerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppOrders_OwnerUserId",
                table: "AppOrders",
                column: "OwnerUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrderItems_AppProducts_ProductId",
                table: "AppOrderItems",
                column: "ProductId",
                principalTable: "AppProducts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrders_AbpUsers_OwnerUserId",
                table: "AppOrders",
                column: "OwnerUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProducts_AbpUsers_OwnerUserId",
                table: "AppProducts",
                column: "OwnerUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id");
        }
    }
}
