using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Courses.Migrations
{
    /// <inheritdoc />
    public partial class Mapping_relatioin_by_convensions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeptId",
                table: "Instructors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "_departmentId",
                table: "Instructors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "_courses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CourseInstructor",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    InstructorsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseInstructor", x => new { x.CourseId, x.InstructorsId });
                    table.ForeignKey(
                        name: "FK_CourseInstructor_Instructors_InstructorsId",
                        column: x => x.InstructorsId,
                        principalTable: "Instructors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseInstructor__courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "_courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Instructors__departmentId",
                table: "Instructors",
                column: "_departmentId");

            migrationBuilder.CreateIndex(
                name: "IX__courses_StudentId",
                table: "_courses",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseInstructor_InstructorsId",
                table: "CourseInstructor",
                column: "InstructorsId");

            migrationBuilder.AddForeignKey(
                name: "FK__courses_Students_StudentId",
                table: "_courses",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructors_Departments__departmentId",
                table: "Instructors",
                column: "_departmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__courses_Students_StudentId",
                table: "_courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructors_Departments__departmentId",
                table: "Instructors");

            migrationBuilder.DropTable(
                name: "CourseInstructor");

            migrationBuilder.DropIndex(
                name: "IX_Instructors__departmentId",
                table: "Instructors");

            migrationBuilder.DropIndex(
                name: "IX__courses_StudentId",
                table: "_courses");

            migrationBuilder.DropColumn(
                name: "DeptId",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "_departmentId",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "_courses");
        }
    }
}
