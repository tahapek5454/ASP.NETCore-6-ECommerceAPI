using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "OwnFiles",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OwnFiles_ProductId",
                table: "OwnFiles",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OwnFiles_Products_ProductId",
                table: "OwnFiles",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OwnFiles_Products_ProductId",
                table: "OwnFiles");

            migrationBuilder.DropIndex(
                name: "IX_OwnFiles_ProductId",
                table: "OwnFiles");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "OwnFiles");
        }
    }
}
