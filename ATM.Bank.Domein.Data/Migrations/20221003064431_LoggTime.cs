using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATM.Bank.Domein.Data.Migrations
{
    public partial class LoggTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoggTime",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardId = table.Column<int>(type: "int", nullable: false),
                    LogIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LogOut = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoggTime", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoggTime_card_CardId",
                        column: x => x.CardId,
                        principalTable: "card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoggTime_CardId",
                table: "LoggTime",
                column: "CardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoggTime");
        }
    }
}
