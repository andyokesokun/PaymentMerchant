using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PaymentMerchant.Infrastructure.Migrations
{
    public partial class Added_date_to_transactionTbl_also_changed_securityCode_dataType_to_int : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "SecurityCode",
                table: "CreditCards",
                type: "INTEGER",
                maxLength: 3,
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 3,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Transactions");

            migrationBuilder.AlterColumn<string>(
                name: "SecurityCode",
                table: "CreditCards",
                type: "TEXT",
                maxLength: 3,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldMaxLength: 3);
        }
    }
}
