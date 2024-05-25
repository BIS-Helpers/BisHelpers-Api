using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BisHelpers.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AcademicCoursesSeedingAndUpdateLectureDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "AcademicLectures");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "AcademicLectures",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AcademicCourses",
                columns: new[] { "Id", "Code", "CreditHours", "Name" },
                values: new object[,]
                {
                    { 1, "Acc101", 3, "Principles of Accounting 1" },
                    { 2, "Acc102", 3, "Principles of Accounting 2" },
                    { 3, "Acc201", 3, "Accounting for Partnerships" },
                    { 4, "Acc202", 3, "Accounting for Corporations" },
                    { 5, "Eco101", 3, "Principle of Economics" },
                    { 6, "Eco201", 3, "Economics of Money and Banking" },
                    { 7, "Man101", 3, "Principles of Management" },
                    { 8, "Man102", 3, "Behavior Management" },
                    { 9, "Man201", 3, "Production and Operations Management" },
                    { 10, "Man202", 3, "Management of Marketing" },
                    { 11, "Mmt101", 3, "Marketing" },
                    { 12, "Sta201", 3, "Statistics" },
                    { 13, "Acc301", 3, "Cost Accounting" },
                    { 14, "Acc292", 3, "Principles of Auditing" },
                    { 15, "Acc491", 3, "Managerial Accounting" },
                    { 16, "Man301", 3, "Financial Management and Investment" },
                    { 17, "Man302", 3, "Human Resources Management" },
                    { 18, "Acc391", 3, "Tax Accounting" },
                    { 19, "Acc492", 3, "Accounting in Financial Institutions" },
                    { 20, "Eco292", 3, "The Economics of Foreign Trade" },
                    { 21, "Eco392", 3, "Economic Planning" },
                    { 22, "Eco393", 3, "Economics of Public Finance" },
                    { 23, "Eco401", 3, "Economics of Information" },
                    { 24, "Man203", 3, "Material Management" },
                    { 25, "Man303", 3, "Management of Specialized Institutions" },
                    { 26, "Pos201", 3, "International Relations" },
                    { 27, "BIS101", 3, "Introduction to Computer" },
                    { 28, "BIS103", 3, "Information Systems" },
                    { 29, "BIS201", 3, "Programming 1" },
                    { 30, "BIS203", 3, "Operative Systems" },
                    { 31, "BIS303", 3, "System Analysis 1" },
                    { 32, "BIS304", 3, "System Analysis 2" },
                    { 33, "BIS307", 3, "Programming 2" },
                    { 34, "BIS302", 3, "Database" },
                    { 35, "BIS305", 3, "E-Commerce" },
                    { 36, "BIS306", 3, "Management Information Systems" },
                    { 37, "BIS310", 3, "Operations Research" },
                    { 38, "BIS402", 3, "Decision Support System" },
                    { 39, "BIS403", 3, "Computer Networks" },
                    { 40, "BIS405", 3, "Data Security" },
                    { 41, "BIS412", 3, "Project 1" },
                    { 42, "BIS413", 3, "Project 2" },
                    { 43, "BIS308", 3, "E-Commerce Sites" },
                    { 44, "BIS309", 3, "Medical Information Systems and Health" },
                    { 45, "BIS601", 3, "Internet Applications" },
                    { 46, "BIS606", 3, "Advanced E-Commerce" },
                    { 47, "BIS407", 3, "Accounting Information Systems" },
                    { 48, "BIS408", 3, "Advanced Systems Analysis" },
                    { 49, "BIS409", 3, "Software Engineering" },
                    { 50, "BIS410", 3, "Project Management" },
                    { 51, "HU111", 2, "English 1" },
                    { 52, "HU112", 2, "English 2" },
                    { 54, "HU334", 3, "Professional Ethics" },
                    { 55, "HU313", 2, "Human Rights" },
                    { 56, "HU210", 3, "Principle of Law" },
                    { 57, "HU331", 3, "Communication & Negotiation" },
                    { 58, "HU332", 3, "Creative Thinking" },
                    { 59, "HU333", 3, "Media" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "AcademicCourses",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "AcademicLectures");

            migrationBuilder.AddColumn<TimeOnly>(
                name: "StartTime",
                table: "AcademicLectures",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }
    }
}
