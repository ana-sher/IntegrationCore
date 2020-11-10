using Microsoft.EntityFrameworkCore.Migrations;

namespace IntegrationCore.Migrations
{
    public partial class NullableSystemIdForType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TypeDefinition_SystemDefinition_SystemId",
                table: "TypeDefinition");

            migrationBuilder.AlterColumn<int>(
                name: "SystemId",
                table: "TypeDefinition",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TypeDefinition_SystemDefinition_SystemId",
                table: "TypeDefinition",
                column: "SystemId",
                principalTable: "SystemDefinition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TypeDefinition_SystemDefinition_SystemId",
                table: "TypeDefinition");

            migrationBuilder.AlterColumn<int>(
                name: "SystemId",
                table: "TypeDefinition",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TypeDefinition_SystemDefinition_SystemId",
                table: "TypeDefinition",
                column: "SystemId",
                principalTable: "SystemDefinition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
