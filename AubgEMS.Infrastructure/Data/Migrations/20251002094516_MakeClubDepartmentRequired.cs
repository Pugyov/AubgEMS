using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AubgEMS.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class MakeClubDepartmentRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clubs_Departments_DepartmentId",
                table: "Clubs");
            
            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Name" },
                values: new object[] { 11, "University-wide (Independent Clubs)" });
            
            migrationBuilder.Sql("UPDATE `Clubs` SET `DepartmentId` = 11 WHERE `DepartmentId` IS NULL;");
            
            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Clubs",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
            
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c31b54bd-e618-4707-a6d1-a981e9dbc69d", "AQAAAAIAAYagAAAAEH67umwht9eQaoKORlnvelhqkfl+/6qLphU4PNs+Ir5ifrcIIlZv1qlUDRKjaaVTpQ==", "9b862235-b767-4ae6-a8bf-ea1b5e632a9f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "eb9175e5-1e90-4a71-b89e-a1ee5d0cc9e9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "509b5d54-fc3e-4a69-bdcd-1bec472b8fa8", "AQAAAAIAAYagAAAAENN8j3iPdRRNr5jAnto4CHwDsxAux1Fpz7Y1MWZ7PhMPhg6ue7u21zh3eMUei1jOlw==", "42e9920a-e7f1-488e-9f88-45bfbfc5ce81" });
            
            migrationBuilder.AddForeignKey(
                name: "FK_Clubs_Departments_DepartmentId",
                table: "Clubs",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clubs_Departments_DepartmentId",
                table: "Clubs");
            
            migrationBuilder.Sql("UPDATE `Clubs` SET `DepartmentId` = NULL WHERE `DepartmentId` = 11;");
            
            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 11);
            
            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Clubs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
            
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5d9d7e56-1279-4d2b-ba3d-871b68434ee6", "AQAAAAIAAYagAAAAEExXcG1SLzfcL/TBDGcpGkgo97GjEiRAkHxHaLSc7YGN1ScSYpRlBbWtOFGzVGc5fQ==", "b7103ea0-d289-435d-8b01-ba739a1adc44" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "eb9175e5-1e90-4a71-b89e-a1ee5d0cc9e9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "afbe7bc2-5c79-4e2d-b906-060e110f467c", "AQAAAAIAAYagAAAAEHc+8XXTDy2I3gDU03fk/wBOchMrW5clW2Mookpa2mM6eWvs9G9G1BwJ66XMHA8c2g==", "be122832-9527-4fc6-8ebb-12cea37b82c0" });
            
            migrationBuilder.AddForeignKey(
                name: "FK_Clubs_Departments_DepartmentId",
                table: "Clubs",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}