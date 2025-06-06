using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pastebin.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangePasteModelFieldsName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pastes_Users_UserId",
                table: "Pastes");

            migrationBuilder.RenameColumn(
                name: "BucketKey",
                table: "Pastes",
                newName: "ContentPath");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Pastes",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "UrlHash",
                table: "Pastes",
                type: "character varying(8)",
                maxLength: 8,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Pastes_UrlHash",
                table: "Pastes",
                column: "UrlHash",
                unique: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropIndex(
                name: "IX_Pastes_UrlHash",
                table: "Pastes");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Pastes");

            migrationBuilder.RenameColumn(
                name: "ContentPath",
                table: "Pastes",
                newName: "BucketKey");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(64)",
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Pastes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UrlHash",
                table: "Pastes",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(8)",
                oldMaxLength: 8,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pastes_Users_UserId",
                table: "Pastes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
