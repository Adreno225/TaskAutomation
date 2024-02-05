using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskAutomationDB.Migrations
{
    /// <inheritdoc />
    public partial class _8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassFunctions_Classes_ClassId",
                table: "ClassFunctions");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassFunctions_FunctionsParameters_FunctionParameterId",
                table: "ClassFunctions");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassFunctions_Parameters_ParameterId",
                table: "ClassFunctions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassFunctions",
                table: "ClassFunctions");

            migrationBuilder.RenameTable(
                name: "ClassFunctions",
                newName: "ParameterClassFunction");

            migrationBuilder.RenameIndex(
                name: "IX_ClassFunctions_ParameterId",
                table: "ParameterClassFunction",
                newName: "IX_ParameterClassFunction_ParameterId");

            migrationBuilder.RenameIndex(
                name: "IX_ClassFunctions_FunctionParameterId",
                table: "ParameterClassFunction",
                newName: "IX_ParameterClassFunction_FunctionParameterId");

            migrationBuilder.RenameIndex(
                name: "IX_ClassFunctions_ClassId",
                table: "ParameterClassFunction",
                newName: "IX_ParameterClassFunction_ClassId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParameterClassFunction",
                table: "ParameterClassFunction",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ParameterClassFunction_Classes_ClassId",
                table: "ParameterClassFunction",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParameterClassFunction_FunctionsParameters_FunctionParameterId",
                table: "ParameterClassFunction",
                column: "FunctionParameterId",
                principalTable: "FunctionsParameters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParameterClassFunction_Parameters_ParameterId",
                table: "ParameterClassFunction",
                column: "ParameterId",
                principalTable: "Parameters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParameterClassFunction_Classes_ClassId",
                table: "ParameterClassFunction");

            migrationBuilder.DropForeignKey(
                name: "FK_ParameterClassFunction_FunctionsParameters_FunctionParameterId",
                table: "ParameterClassFunction");

            migrationBuilder.DropForeignKey(
                name: "FK_ParameterClassFunction_Parameters_ParameterId",
                table: "ParameterClassFunction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParameterClassFunction",
                table: "ParameterClassFunction");

            migrationBuilder.RenameTable(
                name: "ParameterClassFunction",
                newName: "ClassFunctions");

            migrationBuilder.RenameIndex(
                name: "IX_ParameterClassFunction_ParameterId",
                table: "ClassFunctions",
                newName: "IX_ClassFunctions_ParameterId");

            migrationBuilder.RenameIndex(
                name: "IX_ParameterClassFunction_FunctionParameterId",
                table: "ClassFunctions",
                newName: "IX_ClassFunctions_FunctionParameterId");

            migrationBuilder.RenameIndex(
                name: "IX_ParameterClassFunction_ClassId",
                table: "ClassFunctions",
                newName: "IX_ClassFunctions_ClassId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClassFunctions",
                table: "ClassFunctions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassFunctions_Classes_ClassId",
                table: "ClassFunctions",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassFunctions_FunctionsParameters_FunctionParameterId",
                table: "ClassFunctions",
                column: "FunctionParameterId",
                principalTable: "FunctionsParameters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassFunctions_Parameters_ParameterId",
                table: "ClassFunctions",
                column: "ParameterId",
                principalTable: "Parameters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
