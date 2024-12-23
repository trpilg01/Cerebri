using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cerebri.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddJournalEntryMoodsDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JournalEntryMoodModel_JournalEntries_JournalEntryId",
                table: "JournalEntryMoodModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JournalEntryMoodModel",
                table: "JournalEntryMoodModel");

            migrationBuilder.RenameTable(
                name: "JournalEntryMoodModel",
                newName: "JournalEntryMoods");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JournalEntryMoods",
                table: "JournalEntryMoods",
                columns: new[] { "JournalEntryId", "MoodId" });

            migrationBuilder.AddForeignKey(
                name: "FK_JournalEntryMoods_JournalEntries_JournalEntryId",
                table: "JournalEntryMoods",
                column: "JournalEntryId",
                principalTable: "JournalEntries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JournalEntryMoods_JournalEntries_JournalEntryId",
                table: "JournalEntryMoods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JournalEntryMoods",
                table: "JournalEntryMoods");

            migrationBuilder.RenameTable(
                name: "JournalEntryMoods",
                newName: "JournalEntryMoodModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JournalEntryMoodModel",
                table: "JournalEntryMoodModel",
                columns: new[] { "JournalEntryId", "MoodId" });

            migrationBuilder.AddForeignKey(
                name: "FK_JournalEntryMoodModel_JournalEntries_JournalEntryId",
                table: "JournalEntryMoodModel",
                column: "JournalEntryId",
                principalTable: "JournalEntries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
