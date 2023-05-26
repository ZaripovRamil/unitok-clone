using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDefaultPfp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "defaultuser",
                columns: new[] { "ConcurrencyStamp", "ProfilePicUrl", "SecurityStamp" },
                values: new object[] { "5f2d5c14-1e0c-433c-a7db-943769f6aedb", "default_pfp.jpg", "d735ecb7-bcad-46ca-9063-dbf701657f98" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "defaultuser",
                columns: new[] { "ConcurrencyStamp", "ProfilePicUrl", "SecurityStamp" },
                values: new object[] { "fdce837a-acd0-4a11-949c-31d9ddf50b6c", "img/avatars/avatar.jpg", "e36abf3c-429c-40ce-8d61-769f04522fb2" });
        }
    }
}
