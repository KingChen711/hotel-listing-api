using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotelListingAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateApiUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "44dda4d3-177d-4cae-b318-351142b3b87d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4607c1c7-c66c-4d08-863b-3707c66599b4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "abe63e8c-733e-40bd-98e1-8ba374bd86a1", null, "Administrator", "ADMINISTRATOR" },
                    { "de59276e-d313-4f1c-8917-2429a354cd56", null, "Customer", "CUSTOMER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abe63e8c-733e-40bd-98e1-8ba374bd86a1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "de59276e-d313-4f1c-8917-2429a354cd56");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "44dda4d3-177d-4cae-b318-351142b3b87d", null, "Customer", "CUSTOMER" },
                    { "4607c1c7-c66c-4d08-863b-3707c66599b4", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
