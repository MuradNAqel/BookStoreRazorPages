using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreRazorPages.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseRestructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Photo_BookId",
                table: "Photo");

            migrationBuilder.DropColumn(
                name: "Bytes",
                table: "Photo");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Photo");

            migrationBuilder.RenameColumn(
                name: "Age",
                table: "Author",
                newName: "Nationality");

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Photo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Biography",
                table: "Author",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Author",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Author",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Photo_BookId",
                table: "Photo",
                column: "BookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Photo_BookId",
                table: "Photo");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "Photo");

            migrationBuilder.DropColumn(
                name: "Biography",
                table: "Author");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Author");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Author");

            migrationBuilder.RenameColumn(
                name: "Nationality",
                table: "Author",
                newName: "Age");

            migrationBuilder.AddColumn<byte[]>(
                name: "Bytes",
                table: "Photo",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Photo",
                type: "VarChar(250)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Photo_BookId",
                table: "Photo",
                column: "BookId",
                unique: true);
        }
    }
}
