using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BisHelpers.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAnnouncement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegistrationsLectures_AcademicRegistrations_AcademicRegistrationId",
                table: "RegistrationsLectures");

            migrationBuilder.RenameColumn(
                name: "startDate",
                table: "AcademicSemesters",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "endDate",
                table: "AcademicSemesters",
                newName: "EndDate");

            migrationBuilder.CreateTable(
                name: "Announcements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AcademicLectureId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LastUpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Announcements_AcademicLectures_AcademicLectureId",
                        column: x => x.AcademicLectureId,
                        principalTable: "AcademicLectures",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Announcements_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Announcements_AspNetUsers_LastUpdatedById",
                        column: x => x.LastUpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_AcademicLectureId",
                table: "Announcements",
                column: "AcademicLectureId");

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_CreatedById",
                table: "Announcements",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_LastUpdatedById",
                table: "Announcements",
                column: "LastUpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_RegistrationsLectures_AcademicRegistrations_AcademicRegistrationId",
                table: "RegistrationsLectures",
                column: "AcademicRegistrationId",
                principalTable: "AcademicRegistrations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegistrationsLectures_AcademicRegistrations_AcademicRegistrationId",
                table: "RegistrationsLectures");

            migrationBuilder.DropTable(
                name: "Announcements");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "AcademicSemesters",
                newName: "startDate");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "AcademicSemesters",
                newName: "endDate");

            migrationBuilder.AddForeignKey(
                name: "FK_RegistrationsLectures_AcademicRegistrations_AcademicRegistrationId",
                table: "RegistrationsLectures",
                column: "AcademicRegistrationId",
                principalTable: "AcademicRegistrations",
                principalColumn: "Id");
        }
    }
}
