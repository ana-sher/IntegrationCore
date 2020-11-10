using Microsoft.EntityFrameworkCore.Migrations;

namespace IntegrationCore.Migrations
{
    public partial class AddBasicTypeField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBasicType",
                table: "FieldDefinition");

            migrationBuilder.AddColumn<bool>(
                name: "IsBasic",
                table: "TypeDefinition",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBasic",
                table: "TypeDefinition");

            migrationBuilder.AddColumn<bool>(
                name: "IsBasicType",
                table: "FieldDefinition",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
