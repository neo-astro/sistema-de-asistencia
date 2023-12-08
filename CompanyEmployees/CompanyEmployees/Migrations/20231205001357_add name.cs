using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyEmployees.Migrations
{
    public partial class addname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0d393885-0a4f-436c-bc69-ba133364a8c7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "178c4cdc-bd0f-4f8c-9a1e-e9ba84dd874b");

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Suscripcion",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2f8ca51a-29b5-46e6-b687-989965ca794b", "f49a2cc9-26f6-47c3-b08b-378bd7148422", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "df081342-0186-4550-8212-2f88a5bd81a8", "be95f5ae-8cba-47bc-be0b-11a90c274a60", "Viewer", "VIEWER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2f8ca51a-29b5-46e6-b687-989965ca794b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "df081342-0186-4550-8212-2f88a5bd81a8");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Suscripcion");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0d393885-0a4f-436c-bc69-ba133364a8c7", "d5f220e8-e637-473a-a40b-8e330206e8a6", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "178c4cdc-bd0f-4f8c-9a1e-e9ba84dd874b", "8109cf14-3335-4081-9611-c812160765ef", "Viewer", "VIEWER" });
        }
    }
}
