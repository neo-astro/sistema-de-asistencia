using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyEmployees.Migrations
{
    public partial class final : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2f8ca51a-29b5-46e6-b687-989965ca794b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "df081342-0186-4550-8212-2f88a5bd81a8");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0ac52d8c-5054-4a1b-aef1-124c5a336e5c", "42499bf3-9c05-4b2d-adbb-086b9d21e49c", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dd2fa056-d045-4be5-a365-001db781d79e", "64cf3526-0705-4280-a2d8-83949043e8b2", "Viewer", "VIEWER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0ac52d8c-5054-4a1b-aef1-124c5a336e5c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dd2fa056-d045-4be5-a365-001db781d79e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2f8ca51a-29b5-46e6-b687-989965ca794b", "f49a2cc9-26f6-47c3-b08b-378bd7148422", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "df081342-0186-4550-8212-2f88a5bd81a8", "be95f5ae-8cba-47bc-be0b-11a90c274a60", "Viewer", "VIEWER" });
        }
    }
}
