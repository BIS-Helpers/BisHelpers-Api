using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BisHelpers.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RegistrationModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "DateOfJoin",
                table: "Students",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "BirthDate",
                table: "AspNetUsers",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateTable(
                name: "AcademicCourses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreditHours = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicCourses", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "Professors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LastUpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Professors_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Professors_AspNetUsers_LastUpdatedById",
                        column: x => x.LastUpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProfessorsAcademicCourses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(type: "int", nullable: false),
                    AcademicCoursesId = table.Column<int>(type: "int", nullable: false),
                    AcademicSemesterId = table.Column<int>(type: "int", nullable: false),
                    ProfessorId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LastUpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessorsAcademicCourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfessorsAcademicCourses_AcademicCourses_AcademicCoursesId",
                        column: x => x.AcademicCoursesId,
                        principalTable: "AcademicCourses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProfessorsAcademicCourses_AcademicSemesters_AcademicSemesterId",
                        column: x => x.AcademicSemesterId,
                        principalTable: "AcademicSemesters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProfessorsAcademicCourses_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProfessorsAcademicCourses_AspNetUsers_LastUpdatedById",
                        column: x => x.LastUpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProfessorsAcademicCourses_Professors_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AcademicLectures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupNumber = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    ProfessorAcademicCourseId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LastUpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicLectures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcademicLectures_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AcademicLectures_AspNetUsers_LastUpdatedById",
                        column: x => x.LastUpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AcademicLectures_ProfessorsAcademicCourses_ProfessorAcademicCourseId",
                        column: x => x.ProfessorAcademicCourseId,
                        principalTable: "ProfessorsAcademicCourses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AcademicRegistrations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    AcademicLectureId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LastUpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicRegistrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcademicRegistrations_AcademicLectures_AcademicLectureId",
                        column: x => x.AcademicLectureId,
                        principalTable: "AcademicLectures",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AcademicRegistrations_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AcademicRegistrations_AspNetUsers_LastUpdatedById",
                        column: x => x.LastUpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AcademicRegistrations_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_CollegeId",
                table: "Students",
                column: "CollegeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AcademicCourses_Code",
                table: "AcademicCourses",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AcademicCourses_Name",
                table: "AcademicCourses",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AcademicLectures_CreatedById",
                table: "AcademicLectures",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicLectures_GroupNumber_ProfessorAcademicCourseId",
                table: "AcademicLectures",
                columns: new[] { "GroupNumber", "ProfessorAcademicCourseId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AcademicLectures_LastUpdatedById",
                table: "AcademicLectures",
                column: "LastUpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicLectures_ProfessorAcademicCourseId",
                table: "AcademicLectures",
                column: "ProfessorAcademicCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicRegistrations_AcademicLectureId",
                table: "AcademicRegistrations",
                column: "AcademicLectureId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicRegistrations_CreatedById",
                table: "AcademicRegistrations",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicRegistrations_LastUpdatedById",
                table: "AcademicRegistrations",
                column: "LastUpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicRegistrations_StudentId_AcademicLectureId",
                table: "AcademicRegistrations",
                columns: new[] { "StudentId", "AcademicLectureId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AcademicSemesters_Name",
                table: "AcademicSemesters",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Professors_CreatedById",
                table: "Professors",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Professors_LastUpdatedById",
                table: "Professors",
                column: "LastUpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorsAcademicCourses_AcademicCoursesId",
                table: "ProfessorsAcademicCourses",
                column: "AcademicCoursesId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorsAcademicCourses_AcademicSemesterId_AcademicCoursesId_ProfessorId_Year",
                table: "ProfessorsAcademicCourses",
                columns: new[] { "AcademicSemesterId", "AcademicCoursesId", "ProfessorId", "Year" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorsAcademicCourses_CreatedById",
                table: "ProfessorsAcademicCourses",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorsAcademicCourses_LastUpdatedById",
                table: "ProfessorsAcademicCourses",
                column: "LastUpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorsAcademicCourses_ProfessorId",
                table: "ProfessorsAcademicCourses",
                column: "ProfessorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcademicRegistrations");

            migrationBuilder.DropTable(
                name: "AcademicLectures");

            migrationBuilder.DropTable(
                name: "ProfessorsAcademicCourses");

            migrationBuilder.DropTable(
                name: "AcademicCourses");

            migrationBuilder.DropTable(
                name: "AcademicSemesters");

            migrationBuilder.DropTable(
                name: "Professors");

            migrationBuilder.DropIndex(
                name: "IX_Students_CollegeId",
                table: "Students");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfJoin",
                table: "Students",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }
    }
}
