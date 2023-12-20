using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TutorCenter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Updatetutortable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TutorId",
                table: "Course",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Course_TutorId",
                table: "Course",
                column: "TutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Tutor_TutorId",
                table: "Course",
                column: "TutorId",
                principalTable: "Tutor",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_Tutor_TutorId",
                table: "Course");

            migrationBuilder.DropIndex(
                name: "IX_Course_TutorId",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "TutorId",
                table: "Course");
        }
    }
}
