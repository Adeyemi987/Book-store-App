using Microsoft.EntityFrameworkCore.Migrations;

namespace StorBookWebApp.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "15c85e0f-01ce-4eb7-8ec4-2b21759e55a4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6b49ab44-0448-4458-a5ab-e307929f307c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "35b0d99b-63ea-49d7-b584-e2082167a2a4", "d6a4ce4a-e51b-4580-bb5c-03c91c5e3eeb", "Regular", "REGULAR" },
                    { "d7f486f8-dcfc-45ee-8381-51434067dad4", "44dac118-9866-4f31-bc7a-89286fef6d23", "Admin", "ADMIN" },
                    { "6677a61c-424d-42c1-958c-a742fd96038b", "08830129-bdc7-4d5b-82a3-52c374d65f37", "SuperAdmin", "SUPERADMIN" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "35b0d99b-63ea-49d7-b584-e2082167a2a4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6677a61c-424d-42c1-958c-a742fd96038b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d7f486f8-dcfc-45ee-8381-51434067dad4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "15c85e0f-01ce-4eb7-8ec4-2b21759e55a4", "cb7afc49-7ec1-48ed-96b2-0e6a2a78a3aa", "Regular", "REGULAR" },
                    { "6b49ab44-0448-4458-a5ab-e307929f307c", "fec08c90-c1a7-4945-8ff5-5d3605b67873", "Admin", "ADMIN" }
                });
        }
    }
}
