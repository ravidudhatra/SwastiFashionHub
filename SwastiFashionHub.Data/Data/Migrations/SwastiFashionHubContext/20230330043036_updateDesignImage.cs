using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwastiFashionHub.Data.Data.Migrations.SwastiFashionHubContext
{
    /// <inheritdoc />
    public partial class updateDesignImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "DesignImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileType",
                table: "DesignImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "DesignImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Extension",
                table: "DesignImages");

            migrationBuilder.DropColumn(
                name: "FileType",
                table: "DesignImages");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "DesignImages");
        }
    }
}
