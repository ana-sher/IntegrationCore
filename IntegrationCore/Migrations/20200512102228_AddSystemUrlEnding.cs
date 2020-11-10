using Microsoft.EntityFrameworkCore.Migrations;

namespace IntegrationCore.Migrations
{
    public partial class AddSystemUrlEnding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeleteUrlEnding",
                table: "SystemDefinition",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GetByIdentifierUrlEnding",
                table: "SystemDefinition",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GetUrlEnding",
                table: "SystemDefinition",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostUrlEnding",
                table: "SystemDefinition",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PutUrlEnding",
                table: "SystemDefinition",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleteUrlEnding",
                table: "SystemDefinition");

            migrationBuilder.DropColumn(
                name: "GetByIdentifierUrlEnding",
                table: "SystemDefinition");

            migrationBuilder.DropColumn(
                name: "GetUrlEnding",
                table: "SystemDefinition");

            migrationBuilder.DropColumn(
                name: "PostUrlEnding",
                table: "SystemDefinition");

            migrationBuilder.DropColumn(
                name: "PutUrlEnding",
                table: "SystemDefinition");
        }
    }
}
