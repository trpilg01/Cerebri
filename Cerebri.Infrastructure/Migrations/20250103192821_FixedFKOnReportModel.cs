using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cerebri.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixedFKOnReportModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Users_UserModelId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_UserModelId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "UserModelId",
                table: "Reports");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Reports",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Reports",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<Guid>(
                name: "UserModelId",
                table: "Reports",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reports_UserModelId",
                table: "Reports",
                column: "UserModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Users_UserModelId",
                table: "Reports",
                column: "UserModelId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
