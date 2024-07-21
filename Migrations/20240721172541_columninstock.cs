using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class columninstock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4c812931-7615-4e9e-a9cb-1dbbc192011b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc555b58-fb82-4586-843c-d50b703a73c6");

            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "Stocks",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5cdaaef3-f048-4966-a792-69056f6ea334", null, "Admin", "ADMIN" },
                    { "6ad274d1-4c1a-4a92-bc9c-361213a3f09b", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5cdaaef3-f048-4966-a792-69056f6ea334");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6ad274d1-4c1a-4a92-bc9c-361213a3f09b");

            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "Stocks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4c812931-7615-4e9e-a9cb-1dbbc192011b", null, "Admin", "ADMIN" },
                    { "dc555b58-fb82-4586-843c-d50b703a73c6", null, "User", "USER" }
                });
        }
    }
}
