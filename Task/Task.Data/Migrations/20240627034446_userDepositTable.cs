using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task.Data.Migrations
{
    /// <inheritdoc />
    public partial class userDepositTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserDeposits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepositAmount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepositedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDeposits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDeposits_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fb36c373-e836-4dbd-8fe8-fb16c86ba709", "1", "User", "User" });

            migrationBuilder.CreateIndex(
                name: "IX_UserDeposits_UserId",
                table: "UserDeposits",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDeposits");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fb36c373-e836-4dbd-8fe8-fb16c86ba709");
        }
    }
}
