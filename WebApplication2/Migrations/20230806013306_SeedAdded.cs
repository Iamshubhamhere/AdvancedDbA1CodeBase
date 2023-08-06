using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    public partial class SeedAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Laptops_Brands_BrandId",
                table: "Laptops");

            migrationBuilder.DropForeignKey(
                name: "FK_LaptopStore_Laptops_LaptopNumber",
                table: "LaptopStore");

            migrationBuilder.DropForeignKey(
                name: "FK_LaptopStore_Store_StoreNumber1",
                table: "LaptopStore");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LaptopStore",
                table: "LaptopStore");

            migrationBuilder.DropIndex(
                name: "IX_LaptopStore_LaptopNumber",
                table: "LaptopStore");

            migrationBuilder.DropColumn(
                name: "LaptopNumber",
                table: "LaptopStore");

            migrationBuilder.RenameColumn(
                name: "StoreNumber1",
                table: "LaptopStore",
                newName: "LaptopsNumber");

            migrationBuilder.RenameIndex(
                name: "IX_LaptopStore_StoreNumber1",
                table: "LaptopStore",
                newName: "IX_LaptopStore_LaptopsNumber");

            migrationBuilder.RenameColumn(
                name: "Number",
                table: "Laptops",
                newName: "LaptopsNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LaptopStore",
                table: "LaptopStore",
                columns: new[] { "Id", "StoreNumber", "LaptopsNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_LaptopStore_StoreNumber",
                table: "LaptopStore",
                column: "StoreNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Laptops_Brands_BrandId",
                table: "Laptops",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "BrandId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LaptopStore_Laptops_LaptopsNumber",
                table: "LaptopStore",
                column: "LaptopsNumber",
                principalTable: "Laptops",
                principalColumn: "LaptopsNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LaptopStore_Store_StoreNumber",
                table: "LaptopStore",
                column: "StoreNumber",
                principalTable: "Store",
                principalColumn: "StoreNumber",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Laptops_Brands_BrandId",
                table: "Laptops");

            migrationBuilder.DropForeignKey(
                name: "FK_LaptopStore_Laptops_LaptopsNumber",
                table: "LaptopStore");

            migrationBuilder.DropForeignKey(
                name: "FK_LaptopStore_Store_StoreNumber",
                table: "LaptopStore");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LaptopStore",
                table: "LaptopStore");

            migrationBuilder.DropIndex(
                name: "IX_LaptopStore_StoreNumber",
                table: "LaptopStore");

            migrationBuilder.RenameColumn(
                name: "LaptopsNumber",
                table: "LaptopStore",
                newName: "StoreNumber1");

            migrationBuilder.RenameIndex(
                name: "IX_LaptopStore_LaptopsNumber",
                table: "LaptopStore",
                newName: "IX_LaptopStore_StoreNumber1");

            migrationBuilder.RenameColumn(
                name: "LaptopsNumber",
                table: "Laptops",
                newName: "LaptopsNumber");

            migrationBuilder.AddColumn<Guid>(
                name: "LaptopNumber",
                table: "LaptopStore",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_LaptopStore",
                table: "LaptopStore",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_LaptopStore_LaptopNumber",
                table: "LaptopStore",
                column: "LaptopNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Laptops_Brands_BrandId",
                table: "Laptops",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "BrandId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LaptopStore_Laptops_LaptopNumber",
                table: "LaptopStore",
                column: "LaptopNumber",
                principalTable: "Laptops",
                principalColumn: "LaptopsNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LaptopStore_Store_StoreNumber1",
                table: "LaptopStore",
                column: "StoreNumber1",
                principalTable: "Store",
                principalColumn: "StoreNumber",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
