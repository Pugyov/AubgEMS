using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AubgEMS.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndex_Attendance_EventUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f8db5992-ece6-4314-9756-2c372081a79e", "AQAAAAIAAYagAAAAEJzjspw0AIeG6MNjGTD6W9tqeEXD4lGXqDqJusG2+GpEoizkOIF1oW/9qfEgjHGILw==", "391ed893-0c0a-4aa3-8aaa-2e6e7f9e541a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "eb9175e5-1e90-4a71-b89e-a1ee5d0cc9e9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "38bedbc3-c55b-425e-b88d-a6a8bfa8ebd1", "AQAAAAIAAYagAAAAEBicF6pkhY2uZFzv76Kzl2D0jhO7tvKDFo9/X5cvDCChY49D70cEPHLbC3VJy6miWw==", "018f74e9-396d-43ce-b5af-4d3ac2fc4954" });

            migrationBuilder.CreateIndex(
                name: "IX_EventAttendances_EventId_UserId",
                table: "EventAttendances",
                columns: new[] { "EventId", "UserId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EventAttendances_EventId_UserId",
                table: "EventAttendances");

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
        }
    }
}
