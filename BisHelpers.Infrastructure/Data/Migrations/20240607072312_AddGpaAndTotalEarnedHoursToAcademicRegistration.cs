using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BisHelpers.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddGpaAndTotalEarnedHoursToAcademicRegistration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicRegistrations_AcademicLectures_AcademicLectureId",
                table: "AcademicRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_AcademicRegistrations_AcademicLectureId",
                table: "AcademicRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_AcademicRegistrations_StudentId_AcademicLectureId",
                table: "AcademicRegistrations");

            migrationBuilder.RenameColumn(
                name: "AcademicLectureId",
                table: "AcademicRegistrations",
                newName: "TotalEarnedHours");

            migrationBuilder.AddColumn<double>(
                name: "Gpa",
                table: "AcademicRegistrations",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<string>(
                name: "Day",
                table: "AcademicLectures",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "RegistrationsLectures",
                columns: table => new
                {
                    AcademicRegistrationId = table.Column<int>(type: "int", nullable: false),
                    AcademicLectureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationsLectures", x => new { x.AcademicLectureId, x.AcademicRegistrationId });
                    table.ForeignKey(
                        name: "FK_RegistrationsLectures_AcademicLectures_AcademicLectureId",
                        column: x => x.AcademicLectureId,
                        principalTable: "AcademicLectures",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RegistrationsLectures_AcademicRegistrations_AcademicRegistrationId",
                        column: x => x.AcademicRegistrationId,
                        principalTable: "AcademicRegistrations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcademicRegistrations_StudentId",
                table: "AcademicRegistrations",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicLectures_Day_StartTime_ProfessorAcademicCourseId",
                table: "AcademicLectures",
                columns: new[] { "Day", "StartTime", "ProfessorAcademicCourseId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationsLectures_AcademicRegistrationId",
                table: "RegistrationsLectures",
                column: "AcademicRegistrationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistrationsLectures");

            migrationBuilder.DropIndex(
                name: "IX_AcademicRegistrations_StudentId",
                table: "AcademicRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_AcademicLectures_Day_StartTime_ProfessorAcademicCourseId",
                table: "AcademicLectures");

            migrationBuilder.DropColumn(
                name: "Gpa",
                table: "AcademicRegistrations");

            migrationBuilder.RenameColumn(
                name: "TotalEarnedHours",
                table: "AcademicRegistrations",
                newName: "AcademicLectureId");

            migrationBuilder.AlterColumn<string>(
                name: "Day",
                table: "AcademicLectures",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicRegistrations_AcademicLectureId",
                table: "AcademicRegistrations",
                column: "AcademicLectureId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicRegistrations_StudentId_AcademicLectureId",
                table: "AcademicRegistrations",
                columns: new[] { "StudentId", "AcademicLectureId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicRegistrations_AcademicLectures_AcademicLectureId",
                table: "AcademicRegistrations",
                column: "AcademicLectureId",
                principalTable: "AcademicLectures",
                principalColumn: "Id");
        }
    }
}
