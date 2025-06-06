using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pastebin.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangePasteModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PastePath",
                table: "Pastes",
                newName: "UrlHash");

            migrationBuilder.AddColumn<string>(
                name: "BucketKey",
                table: "Pastes",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BucketKey",
                table: "Pastes");

            migrationBuilder.RenameColumn(
                name: "UrlHash",
                table: "Pastes",
                newName: "PastePath");
        }
    }
}
