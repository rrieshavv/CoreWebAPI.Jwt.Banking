using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task.Data.Migrations
{
    /// <inheritdoc />
    public partial class userWithdrawlsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fb36c373-e836-4dbd-8fe8-fb16c86ba709");

            migrationBuilder.AlterColumn<double>(
                name: "DepositAmount",
                table: "UserDeposits",
                type: "float",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "UserWithdraws",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WithdrawnAmount = table.Column<double>(type: "float", nullable: false),
                    WithdrawnOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWithdraws", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserWithdraws_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b02f1055-f376-4cd3-acc6-30ef710a03ae", "1", "User", "User" });

            migrationBuilder.CreateIndex(
                name: "IX_UserWithdraws_UserId",
                table: "UserWithdraws",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserWithdraws");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b02f1055-f376-4cd3-acc6-30ef710a03ae");

            migrationBuilder.AlterColumn<string>(
                name: "DepositAmount",
                table: "UserDeposits",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fb36c373-e836-4dbd-8fe8-fb16c86ba709", "1", "User", "User" });
        }
    }
}
