using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupProject_HRM_Library.Migrations
{
    public partial class UpdateV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LinkProof",
                table: "LeaveLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LinkProof",
                table: "LeaveLogs");
        }
    }
}
