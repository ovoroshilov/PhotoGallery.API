using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoGallery.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddExtensionAndUrlColumnsToImageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileExtension",
                table: "Images",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Images",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileExtension",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Images");
        }
    }
}
