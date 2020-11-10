using Microsoft.EntityFrameworkCore.Migrations;

namespace IntegrationCore.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SystemDefinition",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    TransferType = table.Column<int>(nullable: false),
                    DataType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemDefinition", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConnectionFieldDefinition",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Role = table.Column<int>(nullable: false),
                    SystemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectionFieldDefinition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConnectionFieldDefinition_SystemDefinition_SystemId",
                        column: x => x.SystemId,
                        principalTable: "SystemDefinition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TypeDefinition",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    SystemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeDefinition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TypeDefinition_SystemDefinition_SystemId",
                        column: x => x.SystemId,
                        principalTable: "SystemDefinition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FieldDefinition",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    TypeOfFieldId = table.Column<int>(nullable: false),
                    IsArray = table.Column<bool>(nullable: false),
                    IsBasicType = table.Column<bool>(nullable: false),
                    Required = table.Column<bool>(nullable: false),
                    DefaultValue = table.Column<string>(nullable: true),
                    TypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldDefinition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FieldDefinition_TypeDefinition_TypeId",
                        column: x => x.TypeId,
                        principalTable: "TypeDefinition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Integration",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    TypeFromId = table.Column<int>(nullable: false),
                    TypeToId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Integration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Integration_TypeDefinition_TypeFromId",
                        column: x => x.TypeFromId,
                        principalTable: "TypeDefinition",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Integration_TypeDefinition_TypeToId",
                        column: x => x.TypeToId,
                        principalTable: "TypeDefinition",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FieldConnection",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstFieldId = table.Column<int>(nullable: false),
                    SecondFieldId = table.Column<int>(nullable: false),
                    FirstFieldFilterFunction = table.Column<string>(nullable: true),
                    SecondFieldFilterFunction = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldConnection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FieldConnection_FieldDefinition_FirstFieldId",
                        column: x => x.FirstFieldId,
                        principalTable: "FieldDefinition",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FieldConnection_FieldDefinition_SecondFieldId",
                        column: x => x.SecondFieldId,
                        principalTable: "FieldDefinition",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ConnectionFieldValue",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConnectionFieldId = table.Column<int>(nullable: false),
                    IntegrationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectionFieldValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConnectionFieldValue_ConnectionFieldDefinition_ConnectionFieldId",
                        column: x => x.ConnectionFieldId,
                        principalTable: "ConnectionFieldDefinition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConnectionFieldValue_Integration_IntegrationId",
                        column: x => x.IntegrationId,
                        principalTable: "Integration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConnectionFieldDefinition_SystemId",
                table: "ConnectionFieldDefinition",
                column: "SystemId");

            migrationBuilder.CreateIndex(
                name: "IX_ConnectionFieldValue_ConnectionFieldId",
                table: "ConnectionFieldValue",
                column: "ConnectionFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_ConnectionFieldValue_IntegrationId",
                table: "ConnectionFieldValue",
                column: "IntegrationId");

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

            migrationBuilder.CreateIndex(
                name: "IX_FieldDefinition_TypeId",
                table: "FieldDefinition",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Integration_TypeFromId",
                table: "Integration",
                column: "TypeFromId");

            migrationBuilder.CreateIndex(
                name: "IX_Integration_TypeToId",
                table: "Integration",
                column: "TypeToId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeDefinition_SystemId",
                table: "TypeDefinition",
                column: "SystemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConnectionFieldValue");

            migrationBuilder.DropTable(
                name: "FieldConnection");

            migrationBuilder.DropTable(
                name: "ConnectionFieldDefinition");

            migrationBuilder.DropTable(
                name: "Integration");

            migrationBuilder.DropTable(
                name: "FieldDefinition");

            migrationBuilder.DropTable(
                name: "TypeDefinition");

            migrationBuilder.DropTable(
                name: "SystemDefinition");
        }
    }
}
