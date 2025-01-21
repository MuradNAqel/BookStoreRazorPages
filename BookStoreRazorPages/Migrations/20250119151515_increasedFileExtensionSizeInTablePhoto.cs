using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreRazorPages.Migrations
{
    /// <inheritdoc />
    public partial class increasedFileExtensionSizeInTablePhoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FileExtension",
                table: "Photo",
                type: "VarChar(6)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VarChar(4)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FileExtension",
                table: "Photo",
                type: "VarChar(4)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VarChar(6)");
        }
    }
}
