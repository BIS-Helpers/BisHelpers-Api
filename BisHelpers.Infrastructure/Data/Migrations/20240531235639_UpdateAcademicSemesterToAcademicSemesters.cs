using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BisHelpers.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAcademicSemesterToAcademicSemesters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicSemester_AspNetUsers_CreatedById",
                table: "AcademicSemester");

            migrationBuilder.DropForeignKey(
                name: "FK_AcademicSemester_AspNetUsers_LastUpdatedById",
                table: "AcademicSemester");

            migrationBuilder.DropForeignKey(
                name: "FK_AcademicSemester_Semesters_SemesterId",
                table: "AcademicSemester");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorsAcademicCourses_AcademicSemester_AcademicSemesterId",
                table: "ProfessorsAcademicCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AcademicSemester",
                table: "AcademicSemester");

            migrationBuilder.RenameTable(
                name: "AcademicSemester",
                newName: "AcademicSemesters");

            migrationBuilder.RenameIndex(
                name: "IX_AcademicSemester_SemesterId",
                table: "AcademicSemesters",
                newName: "IX_AcademicSemesters_SemesterId");

            migrationBuilder.RenameIndex(
                name: "IX_AcademicSemester_LastUpdatedById",
                table: "AcademicSemesters",
                newName: "IX_AcademicSemesters_LastUpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_AcademicSemester_CreatedById",
                table: "AcademicSemesters",
                newName: "IX_AcademicSemesters_CreatedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AcademicSemesters",
                table: "AcademicSemesters",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicSemesters_AspNetUsers_CreatedById",
                table: "AcademicSemesters",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicSemesters_AspNetUsers_LastUpdatedById",
                table: "AcademicSemesters",
                column: "LastUpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicSemesters_Semesters_SemesterId",
                table: "AcademicSemesters",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorsAcademicCourses_AcademicSemesters_AcademicSemesterId",
                table: "ProfessorsAcademicCourses",
                column: "AcademicSemesterId",
                principalTable: "AcademicSemesters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicSemesters_AspNetUsers_CreatedById",
                table: "AcademicSemesters");

            migrationBuilder.DropForeignKey(
                name: "FK_AcademicSemesters_AspNetUsers_LastUpdatedById",
                table: "AcademicSemesters");

            migrationBuilder.DropForeignKey(
                name: "FK_AcademicSemesters_Semesters_SemesterId",
                table: "AcademicSemesters");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorsAcademicCourses_AcademicSemesters_AcademicSemesterId",
                table: "ProfessorsAcademicCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AcademicSemesters",
                table: "AcademicSemesters");

            migrationBuilder.RenameTable(
                name: "AcademicSemesters",
                newName: "AcademicSemester");

            migrationBuilder.RenameIndex(
                name: "IX_AcademicSemesters_SemesterId",
                table: "AcademicSemester",
                newName: "IX_AcademicSemester_SemesterId");

            migrationBuilder.RenameIndex(
                name: "IX_AcademicSemesters_LastUpdatedById",
                table: "AcademicSemester",
                newName: "IX_AcademicSemester_LastUpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_AcademicSemesters_CreatedById",
                table: "AcademicSemester",
                newName: "IX_AcademicSemester_CreatedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AcademicSemester",
                table: "AcademicSemester",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicSemester_AspNetUsers_CreatedById",
                table: "AcademicSemester",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicSemester_AspNetUsers_LastUpdatedById",
                table: "AcademicSemester",
                column: "LastUpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicSemester_Semesters_SemesterId",
                table: "AcademicSemester",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorsAcademicCourses_AcademicSemester_AcademicSemesterId",
                table: "ProfessorsAcademicCourses",
                column: "AcademicSemesterId",
                principalTable: "AcademicSemester",
                principalColumn: "Id");
        }
    }
}
