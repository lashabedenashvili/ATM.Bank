using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATM.Bank.Domein.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "card",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PassworSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    DateIssue = table.Column<DateTime>(type: "date", nullable: false),
                    DateExpiry = table.Column<DateTime>(type: "date", nullable: false),
                    Valid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_card", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressType = table.Column<int>(type: "int", nullable: false),
                    address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_address_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "bill",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillNumber = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CardId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_bill_card_CardId",
                        column: x => x.CardId,
                        principalTable: "card",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_bill_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "contactInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactInformationType = table.Column<int>(type: "int", nullable: false),
                    ContactInfo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contactInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_contactInformation_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "privateInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumenType = table.Column<int>(type: "int", nullable: false),
                    DocumentNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    SerialNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    DateIssue = table.Column<DateTime>(type: "date", nullable: false),
                    DateExpiry = table.Column<DateTime>(type: "date", nullable: false),
                    PrivateNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IssuingAutority = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_privateInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_privateInformation_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "transaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillId = table.Column<int>(type: "int", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DebitEmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreditEmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_transaction_bill_BillId",
                        column: x => x.BillId,
                        principalTable: "bill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_address_UserId",
                table: "address",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_bill_CardId",
                table: "bill",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_bill_UserId",
                table: "bill",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_contactInformation_UserId",
                table: "contactInformation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_privateInformation_UserId",
                table: "privateInformation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_BillId",
                table: "transaction",
                column: "BillId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "address");

            migrationBuilder.DropTable(
                name: "contactInformation");

            migrationBuilder.DropTable(
                name: "privateInformation");

            migrationBuilder.DropTable(
                name: "transaction");

            migrationBuilder.DropTable(
                name: "bill");

            migrationBuilder.DropTable(
                name: "card");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
