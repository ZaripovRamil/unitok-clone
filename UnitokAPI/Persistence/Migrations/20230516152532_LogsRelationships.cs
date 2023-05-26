using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class LogsRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TokenCreationLogs_Tokens_TokenId",
                table: "TokenCreationLogs");

            migrationBuilder.DropIndex(
                name: "IX_TokenCreationLogs_TokenId",
                table: "TokenCreationLogs");

            migrationBuilder.AlterColumn<string>(
                name: "TokenId",
                table: "TokenCreationLogs",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "defaultuser",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "6c262496-b430-440e-a70f-707a113e4879", "50e30247-3b63-4477-8567-174abeede5e7" });

            migrationBuilder.CreateIndex(
                name: "IX_TokenCreationLogs_TokenId",
                table: "TokenCreationLogs",
                column: "TokenId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TokenCreationLogs_Tokens_TokenId",
                table: "TokenCreationLogs",
                column: "TokenId",
                principalTable: "Tokens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TokenCreationLogs_Tokens_TokenId",
                table: "TokenCreationLogs");

            migrationBuilder.DropIndex(
                name: "IX_TokenCreationLogs_TokenId",
                table: "TokenCreationLogs");

            migrationBuilder.AlterColumn<string>(
                name: "TokenId",
                table: "TokenCreationLogs",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "defaultuser",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "bcec8ad6-b1c6-47f2-8c46-d459b2e68367", "f5204ca7-7a5c-4ca1-a937-bef028f1cfd7" });

            migrationBuilder.CreateIndex(
                name: "IX_TokenCreationLogs_TokenId",
                table: "TokenCreationLogs",
                column: "TokenId");

            migrationBuilder.AddForeignKey(
                name: "FK_TokenCreationLogs_Tokens_TokenId",
                table: "TokenCreationLogs",
                column: "TokenId",
                principalTable: "Tokens",
                principalColumn: "Id");
        }
    }
}
