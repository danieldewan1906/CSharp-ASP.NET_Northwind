using Microsoft.EntityFrameworkCore.Migrations;

namespace Northwind.Persistence.Migrations.Northwind
{
    public partial class AddRoleToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2d793467-3d3b-4696-aab4-80408f3f72d9", "6ff16151-0b5b-4ab2-bdce-32928b09d64c", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "02cc7f33-dcf5-45da-88b0-6617f105381c", "d3351f4d-f5b2-4b06-9d50-daf7aeb6f70e", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "02cc7f33-dcf5-45da-88b0-6617f105381c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2d793467-3d3b-4696-aab4-80408f3f72d9");
        }
    }
}
