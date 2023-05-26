using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixedDefaultUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "45852439-0d1e-45cb-bd2b-3dfc6f28484b");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Balance", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicUrl", "Role", "SecurityStamp", "Seed", "TwoFactorEnabled", "UserName", "Wallet" },
                values: new object[] { "defaultuser", 0, 0m, "bcec8ad6-b1c6-47f2-8c46-d459b2e68367", "", false, "Ramil", "Zaripov", false, null, null, null, null, null, false, "img/avatars/avatar.jpg", 0, "f5204ca7-7a5c-4ca1-a937-bef028f1cfd7", null, false, "rzaripov", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "defaultuser");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Balance", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicUrl", "Role", "SecurityStamp", "Seed", "TwoFactorEnabled", "UserName", "Wallet" },
                values: new object[] { "45852439-0d1e-45cb-bd2b-3dfc6f28484b", 0, 0m, "72930faf-c6fb-4e14-b6ab-21a93f3a4d8b", "", false, null, null, false, null, null, null, null, null, false, "default_pfp", 0, "53491627-6417-4955-9b09-7daa9c4e70fc", null, false, "rzaripov", null });
        }
    }
}
