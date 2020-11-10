using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IntegrationCore.Migrations
{
    public partial class AddTransactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IntegrationId",
                table: "FieldConnection",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(nullable: true),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    GivenName = table.Column<string>(nullable: true),
                    IntegrationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_Integration_IntegrationId",
                        column: x => x.IntegrationId,
                        principalTable: "Integration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FieldConnection_IntegrationId",
                table: "FieldConnection",
                column: "IntegrationId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_IntegrationId",
                table: "Transaction",
                column: "IntegrationId");

            migrationBuilder.AddForeignKey(
                name: "FK_FieldConnection_Integration_IntegrationId",
                table: "FieldConnection",
                column: "IntegrationId",
                principalTable: "Integration",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FieldConnection_Integration_IntegrationId",
                table: "FieldConnection");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_FieldConnection_IntegrationId",
                table: "FieldConnection");

            migrationBuilder.DropColumn(
                name: "IntegrationId",
                table: "FieldConnection");
        }
    }
}
