using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VZ.MoneyFlow.EFData.Migrations
{
    public partial class DbSetsRenewed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountType = table.Column<int>(type: "int", nullable: false),
                    BankAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastFourDigitsOfCard = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentCategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Operations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OperationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OperationType = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Operations_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Operations_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountsCurrencies",
                columns: table => new
                {
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountsCurrencies", x => new { x.AccountId, x.CurrencyId });
                    table.ForeignKey(
                        name: "FK_AccountsCurrencies_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountsCurrencies_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyExchanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OperationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccountCurrencyFromAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AccountCurrencyFromCurrencyId = table.Column<int>(type: "int", nullable: true),
                    AccountCurrencyToAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AccountCurrencyToCurrencyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyExchanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrencyExchanges_AccountsCurrencies_AccountCurrencyFromAccountId_AccountCurrencyFromCurrencyId",
                        columns: x => new { x.AccountCurrencyFromAccountId, x.AccountCurrencyFromCurrencyId },
                        principalTable: "AccountsCurrencies",
                        principalColumns: new[] { "AccountId", "CurrencyId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CurrencyExchanges_AccountsCurrencies_AccountCurrencyToAccountId_AccountCurrencyToCurrencyId",
                        columns: x => new { x.AccountCurrencyToAccountId, x.AccountCurrencyToCurrencyId },
                        principalTable: "AccountsCurrencies",
                        principalColumns: new[] { "AccountId", "CurrencyId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transfers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OperationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccountCurrencyFromAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AccountCurrencyFromCurrencyId = table.Column<int>(type: "int", nullable: true),
                    AccountCurrencyToAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AccountCurrencyToCurrencyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transfers_AccountsCurrencies_AccountCurrencyFromAccountId_AccountCurrencyFromCurrencyId",
                        columns: x => new { x.AccountCurrencyFromAccountId, x.AccountCurrencyFromCurrencyId },
                        principalTable: "AccountsCurrencies",
                        principalColumns: new[] { "AccountId", "CurrencyId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transfers_AccountsCurrencies_AccountCurrencyToAccountId_AccountCurrencyToCurrencyId",
                        columns: x => new { x.AccountCurrencyToAccountId, x.AccountCurrencyToCurrencyId },
                        principalTable: "AccountsCurrencies",
                        principalColumns: new[] { "AccountId", "CurrencyId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserId",
                table: "Accounts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountsCurrencies_CurrencyId",
                table: "AccountsCurrencies",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyExchanges_AccountCurrencyFromAccountId_AccountCurrencyFromCurrencyId",
                table: "CurrencyExchanges",
                columns: new[] { "AccountCurrencyFromAccountId", "AccountCurrencyFromCurrencyId" });

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyExchanges_AccountCurrencyToAccountId_AccountCurrencyToCurrencyId",
                table: "CurrencyExchanges",
                columns: new[] { "AccountCurrencyToAccountId", "AccountCurrencyToCurrencyId" });

            migrationBuilder.CreateIndex(
                name: "IX_Operations_AccountId",
                table: "Operations",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_CategoryId",
                table: "Operations",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_AccountCurrencyFromAccountId_AccountCurrencyFromCurrencyId",
                table: "Transfers",
                columns: new[] { "AccountCurrencyFromAccountId", "AccountCurrencyFromCurrencyId" });

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_AccountCurrencyToAccountId_AccountCurrencyToCurrencyId",
                table: "Transfers",
                columns: new[] { "AccountCurrencyToAccountId", "AccountCurrencyToCurrencyId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrencyExchanges");

            migrationBuilder.DropTable(
                name: "Operations");

            migrationBuilder.DropTable(
                name: "Transfers");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "AccountsCurrencies");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Currencies");
        }
    }
}
