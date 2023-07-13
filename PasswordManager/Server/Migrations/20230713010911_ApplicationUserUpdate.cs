using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PasswordManager.Server.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationUserUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountApplicationUser_Account_AccountsId",
                table: "AccountApplicationUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Account",
                table: "Account");

            migrationBuilder.RenameTable(
                name: "Account",
                newName: "Accounts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ApplicationUserUpdates",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserUpdates", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AccountApplicationUser_Accounts_AccountsId",
                table: "AccountApplicationUser",
                column: "AccountsId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountApplicationUser_Accounts_AccountsId",
                table: "AccountApplicationUser");

            migrationBuilder.DropTable(
                name: "ApplicationUserUpdates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.RenameTable(
                name: "Accounts",
                newName: "Account");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Account",
                table: "Account",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountApplicationUser_Account_AccountsId",
                table: "AccountApplicationUser",
                column: "AccountsId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
