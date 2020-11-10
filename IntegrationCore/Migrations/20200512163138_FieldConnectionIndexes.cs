using Microsoft.EntityFrameworkCore.Migrations;

namespace IntegrationCore.Migrations
{
    public partial class FieldConnectionIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FieldConnection_FirstFieldId",
                table: "FieldConnection");

            migrationBuilder.DropIndex(
                name: "IX_FieldConnection_SecondFieldId",
                table: "FieldConnection");

            migrationBuilder.CreateIndex(
                name: "IX_FieldConnection_FirstFieldId",
                table: "FieldConnection",
                column: "FirstFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldConnection_SecondFieldId",
                table: "FieldConnection",
                column: "SecondFieldId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FieldConnection_FirstFieldId",
                table: "FieldConnection");

            migrationBuilder.DropIndex(
                name: "IX_FieldConnection_SecondFieldId",
                table: "FieldConnection");

            migrationBuilder.CreateIndex(
                name: "IX_FieldConnection_FirstFieldId",
                table: "FieldConnection",
                column: "FirstFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FieldConnection_SecondFieldId",
                table: "FieldConnection",
                column: "SecondFieldId",
                unique: true);
        }
    }
}
