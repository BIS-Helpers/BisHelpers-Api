using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BisHelpers.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAcademicRegistrationWithStudentRelatoion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicRegistrations_Students_StudentId",
                table: "AcademicRegistrations");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicRegistrations_Students_StudentId",
                table: "AcademicRegistrations",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicRegistrations_Students_StudentId",
                table: "AcademicRegistrations");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicRegistrations_Students_StudentId",
                table: "AcademicRegistrations",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }
    }
}
