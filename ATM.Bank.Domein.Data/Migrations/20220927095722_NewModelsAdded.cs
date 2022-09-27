using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATM.Bank.Domein.Data.Migrations
{
    public partial class NewModelsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "user");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "user");

            migrationBuilder.DropColumn(
                name: "PersonalNumber",
                table: "user");

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "user",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "user",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_address_UserId",
                table: "address",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_contactInformation_UserId",
                table: "contactInformation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_privateInformation_UserId",
                table: "privateInformation",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "address");

            migrationBuilder.DropTable(
                name: "contactInformation");

            migrationBuilder.DropTable(
                name: "privateInformation");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "user");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "user");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "user",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "user",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonalNumber",
                table: "user",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
