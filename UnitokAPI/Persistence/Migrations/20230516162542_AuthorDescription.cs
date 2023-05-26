using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AuthorDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "defaultuser",
                columns: new[] { "ConcurrencyStamp", "Description", "SecurityStamp" },
                values: new object[] { "fdce837a-acd0-4a11-949c-31d9ddf50b6c", "bread sphere hung itself", "e36abf3c-429c-40ce-8d61-769f04522fb2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "defaultuser",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "6c262496-b430-440e-a70f-707a113e4879", "50e30247-3b63-4477-8567-174abeede5e7" });
        }
    }
}
