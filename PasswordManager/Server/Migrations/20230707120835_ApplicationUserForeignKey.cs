using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PasswordManager.Server.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationUserForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_AspNetUsers_ApplicationUserId",
                table: "Account");

            migrationBuilder.DropIndex(
                name: "IX_Account_ApplicationUserId",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Account");

            migrationBuilder.CreateTable(
                name: "AccountApplicationUser",
                columns: table => new
                {
                    AccountsId = table.Column<int>(type: "INTEGER", nullable: false),
                    UsersId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountApplicationUser", x => new { x.AccountsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_AccountApplicationUser_Account_AccountsId",
                        column: x => x.AccountsId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountApplicationUser_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountApplicationUser_UsersId",
                table: "AccountApplicationUser",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountApplicationUser");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Account",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Account_ApplicationUserId",
                table: "Account",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_AspNetUsers_ApplicationUserId",
                table: "Account",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
