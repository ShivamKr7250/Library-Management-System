using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LMS.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class seedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "Code" },
                values: new object[,]
                {
                    { 1, "Science Fiction", "[\"SF001\",\"SF002\",\"SF003\"]" },
                    { 2, "Fantasy", "[\"F001\",\"F002\",\"F003\"]" },
                    { 3, "History", "[\"H001\",\"H002\",\"H003\"]" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorName", "BookName", "CategoryId", "IsBookAvailable", "SerialName" },
                values: new object[,]
                {
                    { 1, "Frank Herbert", "Dune", 1, true, "SF001" },
                    { 2, "J.R.R. Tolkien", "The Hobbit", 2, true, "F001" },
                    { 3, "Sun Tzu", "The Art of War", 3, false, "H001" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
