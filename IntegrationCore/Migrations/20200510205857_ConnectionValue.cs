using Microsoft.EntityFrameworkCore.Migrations;

namespace IntegrationCore.Migrations
{
    public partial class ConnectionValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "ConnectionFieldValue",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "ConnectionFieldValue");
        }
    }
}
