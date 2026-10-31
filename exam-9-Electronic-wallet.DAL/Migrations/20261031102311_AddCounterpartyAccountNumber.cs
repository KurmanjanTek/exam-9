using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace exam_9_Electronic_wallet.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddCounterpartyAccountNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CounterpartyAccountNumber",
                table: "Transactions",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CounterpartyAccountNumber",
                table: "Transactions");
        }
    }
}
