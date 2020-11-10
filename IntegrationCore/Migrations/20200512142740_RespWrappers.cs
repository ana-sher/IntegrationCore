using Microsoft.EntityFrameworkCore.Migrations;

namespace IntegrationCore.Migrations
{
    public partial class RespWrappers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GetByIdFieldWrapper",
                table: "TypeDefinition",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GetFieldWrapper",
                table: "TypeDefinition",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostFieldWrapper",
                table: "TypeDefinition",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PutFieldWrapper",
                table: "TypeDefinition",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GetByIdFieldWrapper",
                table: "TypeDefinition");

            migrationBuilder.DropColumn(
                name: "GetFieldWrapper",
                table: "TypeDefinition");

            migrationBuilder.DropColumn(
                name: "PostFieldWrapper",
                table: "TypeDefinition");

            migrationBuilder.DropColumn(
                name: "PutFieldWrapper",
                table: "TypeDefinition");
        }
    }
}
