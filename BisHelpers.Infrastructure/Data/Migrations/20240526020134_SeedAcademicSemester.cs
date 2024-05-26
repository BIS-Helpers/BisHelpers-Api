using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BisHelpers.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedAcademicSemester : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AcademicSemesters",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Fall" },
                    { 2, "Winter" },
                    { 3, "Spring" },
                    { 4, "Summer" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AcademicSemesters",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AcademicSemesters",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AcademicSemesters",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AcademicSemesters",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
