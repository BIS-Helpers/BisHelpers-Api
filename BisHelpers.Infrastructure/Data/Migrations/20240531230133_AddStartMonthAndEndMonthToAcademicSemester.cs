using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BisHelpers.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStartMonthAndEndMonthToAcademicSemester : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorsAcademicCourses_AcademicSemesters_AcademicSemesterId",
                table: "ProfessorsAcademicCourses");

            migrationBuilder.DropTable(
                name: "AcademicSemesters");

            migrationBuilder.DropIndex(
                name: "IX_ProfessorsAcademicCourses_AcademicSemesterId_AcademicCourseId_ProfessorId_Year",
                table: "ProfessorsAcademicCourses");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "ProfessorsAcademicCourses");

            migrationBuilder.CreateTable(
                name: "Semesters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semesters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AcademicSemester",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    startDate = table.Column<DateOnly>(type: "date", nullable: false),
                    endDate = table.Column<DateOnly>(type: "date", nullable: false),
                    SemesterId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LastUpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicSemester", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcademicSemester_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AcademicSemester_AspNetUsers_LastUpdatedById",
                        column: x => x.LastUpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AcademicSemester_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Semesters",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Fall" },
                    { 2, "Winter" },
                    { 3, "Spring" },
                    { 4, "Summer" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorsAcademicCourses_AcademicSemesterId_AcademicCourseId_ProfessorId",
                table: "ProfessorsAcademicCourses",
                columns: new[] { "AcademicSemesterId", "AcademicCourseId", "ProfessorId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AcademicSemester_CreatedById",
                table: "AcademicSemester",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicSemester_LastUpdatedById",
                table: "AcademicSemester",
                column: "LastUpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicSemester_SemesterId",
                table: "AcademicSemester",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_Name",
                table: "Semesters",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorsAcademicCourses_AcademicSemester_AcademicSemesterId",
                table: "ProfessorsAcademicCourses",
                column: "AcademicSemesterId",
                principalTable: "AcademicSemester",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorsAcademicCourses_AcademicSemester_AcademicSemesterId",
                table: "ProfessorsAcademicCourses");

            migrationBuilder.DropTable(
                name: "AcademicSemester");

            migrationBuilder.DropTable(
                name: "Semesters");

            migrationBuilder.DropIndex(
                name: "IX_ProfessorsAcademicCourses_AcademicSemesterId_AcademicCourseId_ProfessorId",
                table: "ProfessorsAcademicCourses");

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "ProfessorsAcademicCourses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AcademicSemesters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicSemesters", x => x.Id);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorsAcademicCourses_AcademicSemesterId_AcademicCourseId_ProfessorId_Year",
                table: "ProfessorsAcademicCourses",
                columns: new[] { "AcademicSemesterId", "AcademicCourseId", "ProfessorId", "Year" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AcademicSemesters_Name",
                table: "AcademicSemesters",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorsAcademicCourses_AcademicSemesters_AcademicSemesterId",
                table: "ProfessorsAcademicCourses",
                column: "AcademicSemesterId",
                principalTable: "AcademicSemesters",
                principalColumn: "Id");
        }
    }
}
