using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pastebin.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DoubleForeignKeyFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pastes_Users_AuthorId",
                table: "Pastes");

            migrationBuilder.DropForeignKey(
                name: "FK_Pastes_Users_UserId",
                table: "Pastes");

            migrationBuilder.DropIndex(
                name: "IX_Pastes_AuthorId",
                table: "Pastes");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Pastes");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Pastes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pastes_Users_UserId",
                table: "Pastes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pastes_Users_UserId",
                table: "Pastes");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Pastes",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId",
                table: "Pastes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Pastes_AuthorId",
                table: "Pastes",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pastes_Users_AuthorId",
                table: "Pastes",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pastes_Users_UserId",
                table: "Pastes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
