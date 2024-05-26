using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BisHelpers.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDayToAcademicLecture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorsAcademicCourses_AcademicCourses_AcademicCoursesId",
                table: "ProfessorsAcademicCourses");

            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "AcademicLectures");

            migrationBuilder.RenameColumn(
                name: "AcademicCoursesId",
                table: "ProfessorsAcademicCourses",
                newName: "AcademicCourseId");

            migrationBuilder.RenameIndex(
                name: "IX_ProfessorsAcademicCourses_AcademicSemesterId_AcademicCoursesId_ProfessorId_Year",
                table: "ProfessorsAcademicCourses",
                newName: "IX_ProfessorsAcademicCourses_AcademicSemesterId_AcademicCourseId_ProfessorId_Year");

            migrationBuilder.RenameIndex(
                name: "IX_ProfessorsAcademicCourses_AcademicCoursesId",
                table: "ProfessorsAcademicCourses",
                newName: "IX_ProfessorsAcademicCourses_AcademicCourseId");

            migrationBuilder.AddColumn<string>(
                name: "Day",
                table: "AcademicLectures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<TimeOnly>(
                name: "StartTime",
                table: "AcademicLectures",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorsAcademicCourses_AcademicCourses_AcademicCourseId",
                table: "ProfessorsAcademicCourses",
                column: "AcademicCourseId",
                principalTable: "AcademicCourses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorsAcademicCourses_AcademicCourses_AcademicCourseId",
                table: "ProfessorsAcademicCourses");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "AcademicLectures");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "AcademicLectures");

            migrationBuilder.RenameColumn(
                name: "AcademicCourseId",
                table: "ProfessorsAcademicCourses",
                newName: "AcademicCoursesId");

            migrationBuilder.RenameIndex(
                name: "IX_ProfessorsAcademicCourses_AcademicSemesterId_AcademicCourseId_ProfessorId_Year",
                table: "ProfessorsAcademicCourses",
                newName: "IX_ProfessorsAcademicCourses_AcademicSemesterId_AcademicCoursesId_ProfessorId_Year");

            migrationBuilder.RenameIndex(
                name: "IX_ProfessorsAcademicCourses_AcademicCourseId",
                table: "ProfessorsAcademicCourses",
                newName: "IX_ProfessorsAcademicCourses_AcademicCoursesId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "AcademicLectures",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorsAcademicCourses_AcademicCourses_AcademicCoursesId",
                table: "ProfessorsAcademicCourses",
                column: "AcademicCoursesId",
                principalTable: "AcademicCourses",
                principalColumn: "Id");
        }
    }
}
