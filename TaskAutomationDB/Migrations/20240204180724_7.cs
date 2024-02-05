using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskAutomationDB.Migrations
{
    /// <inheritdoc />
    public partial class _7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassParameter");

            migrationBuilder.DropTable(
                name: "FunctionParameterParameter");

            migrationBuilder.CreateTable(
                name: "ClassFunctions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParameterId = table.Column<int>(type: "int", nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    FunctionParameterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassFunctions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassFunctions_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassFunctions_FunctionsParameters_FunctionParameterId",
                        column: x => x.FunctionParameterId,
                        principalTable: "FunctionsParameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassFunctions_Parameters_ParameterId",
                        column: x => x.ParameterId,
                        principalTable: "Parameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassFunctions_ClassId",
                table: "ClassFunctions",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassFunctions_FunctionParameterId",
                table: "ClassFunctions",
                column: "FunctionParameterId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassFunctions_ParameterId",
                table: "ClassFunctions",
                column: "ParameterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassFunctions");

            migrationBuilder.CreateTable(
                name: "ClassParameter",
                columns: table => new
                {
                    ClassesId = table.Column<int>(type: "int", nullable: false),
                    ParametersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassParameter", x => new { x.ClassesId, x.ParametersId });
                    table.ForeignKey(
                        name: "FK_ClassParameter_Classes_ClassesId",
                        column: x => x.ClassesId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassParameter_Parameters_ParametersId",
                        column: x => x.ParametersId,
                        principalTable: "Parameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FunctionParameterParameter",
                columns: table => new
                {
                    FunctionParametersId = table.Column<int>(type: "int", nullable: false),
                    ParametersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FunctionParameterParameter", x => new { x.FunctionParametersId, x.ParametersId });
                    table.ForeignKey(
                        name: "FK_FunctionParameterParameter_FunctionsParameters_FunctionParametersId",
                        column: x => x.FunctionParametersId,
                        principalTable: "FunctionsParameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FunctionParameterParameter_Parameters_ParametersId",
                        column: x => x.ParametersId,
                        principalTable: "Parameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassParameter_ParametersId",
                table: "ClassParameter",
                column: "ParametersId");

            migrationBuilder.CreateIndex(
                name: "IX_FunctionParameterParameter_ParametersId",
                table: "FunctionParameterParameter",
                column: "ParametersId");
        }
    }
}
