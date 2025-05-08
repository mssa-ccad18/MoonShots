using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmazingCalcRazorPage.Migrations
{
    /// <inheritdoc />
    public partial class addingBMITCATS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BMI",
                table: "UserProfiles",
                newName: "BMIValue");

            migrationBuilder.AddColumn<string>(
                name: "BMICategory",
                table: "UserProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsMale",
                table: "UserProfiles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BMICategory",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "IsMale",
                table: "UserProfiles");

            migrationBuilder.RenameColumn(
                name: "BMIValue",
                table: "UserProfiles",
                newName: "BMI");
        }
    }
}
