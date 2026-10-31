using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace exam_9_Electronic_wallet.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedServiceProviders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ServiceProviders",
                columns: new[] { "Id", "Name", "Type" },
                values: new object[,]
                {
                    { 1, "MegaCom", "Mobile" },
                    { 2, "O!", "Mobile" },
                    { 3, "Beeline", "Mobile" },
                    { 4, "Kyrgyztelecom", "Internet" },
                    { 5, "Electricity", "Utilities" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ServiceProviders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ServiceProviders",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ServiceProviders",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ServiceProviders",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ServiceProviders",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
