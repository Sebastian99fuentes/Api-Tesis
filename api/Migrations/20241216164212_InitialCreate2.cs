using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3ed52a55-1d19-4f72-a41e-714eb45de3a3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "71774d1c-14e2-4f74-8b41-3ac9590bbf92");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "c838f67e-dada-4c76-818a-7f5d7d4cd440", null, "User", "USER" },
                    { "d824842c-9f32-4fcb-8d48-6e573174d600", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c838f67e-dada-4c76-818a-7f5d7d4cd440");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d824842c-9f32-4fcb-8d48-6e573174d600");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3ed52a55-1d19-4f72-a41e-714eb45de3a3", null, "Admin", "ADMIN" },
                    { "71774d1c-14e2-4f74-8b41-3ac9590bbf92", null, "User", "USER" }
                });
        }
    }
}
