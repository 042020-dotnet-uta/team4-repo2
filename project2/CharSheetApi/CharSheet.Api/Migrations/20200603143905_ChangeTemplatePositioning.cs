using Microsoft.EntityFrameworkCore.Migrations;

namespace CharSheet.Api.Migrations
{
    public partial class ChangeTemplatePositioning : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OffsetLeft",
                table: "FormPositions");

            migrationBuilder.DropColumn(
                name: "OffsetTop",
                table: "FormPositions");

            migrationBuilder.DropColumn(
                name: "XPos",
                table: "FormPositions");

            migrationBuilder.DropColumn(
                name: "YPos",
                table: "FormPositions");

            migrationBuilder.AddColumn<int>(
                name: "X",
                table: "FormPositions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Y",
                table: "FormPositions",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "X",
                table: "FormPositions");

            migrationBuilder.DropColumn(
                name: "Y",
                table: "FormPositions");

            migrationBuilder.AddColumn<int>(
                name: "OffsetLeft",
                table: "FormPositions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OffsetTop",
                table: "FormPositions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "XPos",
                table: "FormPositions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YPos",
                table: "FormPositions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
