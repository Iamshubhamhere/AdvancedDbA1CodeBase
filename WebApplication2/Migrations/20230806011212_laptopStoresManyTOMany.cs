using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    public partial class laptopStoresManyTOMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LaptopStore",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StoreNumber1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StoreNumber = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LaptopNumber = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaptopStore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LaptopStore_Laptops_LaptopNumber",
                        column: x => x.LaptopNumber,
                        principalTable: "Laptops",
                        principalColumn: "Number",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LaptopStore_Store_StoreNumber1",
                        column: x => x.StoreNumber1,
                        principalTable: "Store",
                        principalColumn: "StoreNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LaptopStore_LaptopNumber",
                table: "LaptopStore",
                column: "LaptopNumber");

            migrationBuilder.CreateIndex(
                name: "IX_LaptopStore_StoreNumber1",
                table: "LaptopStore",
                column: "StoreNumber1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LaptopStore");
        }
    }
}
