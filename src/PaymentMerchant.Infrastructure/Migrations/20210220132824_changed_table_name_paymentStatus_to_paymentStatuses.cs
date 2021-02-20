using Microsoft.EntityFrameworkCore.Migrations;

namespace PaymentMerchant.Infrastructure.Migrations
{
    public partial class changed_table_name_paymentStatus_to_paymentStatuses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_PaymentStatus_PaymentStatusId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentStatus",
                table: "PaymentStatus");

            migrationBuilder.RenameTable(
                name: "PaymentStatus",
                newName: "PaymentStatuses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentStatuses",
                table: "PaymentStatuses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_PaymentStatuses_PaymentStatusId",
                table: "Transactions",
                column: "PaymentStatusId",
                principalTable: "PaymentStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_PaymentStatuses_PaymentStatusId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentStatuses",
                table: "PaymentStatuses");

            migrationBuilder.RenameTable(
                name: "PaymentStatuses",
                newName: "PaymentStatus");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentStatus",
                table: "PaymentStatus",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_PaymentStatus_PaymentStatusId",
                table: "Transactions",
                column: "PaymentStatusId",
                principalTable: "PaymentStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
