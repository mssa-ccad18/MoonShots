using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmazingCalcRazorPage.Migrations
{
    /// <inheritdoc />
    public partial class addedfields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "UserProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "UserProfiles");
        }
    }
}
