using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Courses.Migrations
{
    /// <inheritdoc />
    public partial class Edit_relatoinships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__courses_Students_StudentId",
                table: "_courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructors_Departments__departmentId",
                table: "Instructors");

            migrationBuilder.DropIndex(
                name: "IX_Instructors__departmentId",
                table: "Instructors");

            migrationBuilder.DropIndex(
                name: "IX__courses_StudentId",
                table: "_courses");

            migrationBuilder.DropColumn(
                name: "_departmentId",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "_courses");

            migrationBuilder.CreateTable(
                name: "CourseStudent",
                columns: table => new
                {
                    StudentsId = table.Column<int>(type: "int", nullable: false),
                    coursesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseStudent", x => new { x.StudentsId, x.coursesId });
                    table.ForeignKey(
                        name: "FK_CourseStudent_Students_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseStudent__courses_coursesId",
                        column: x => x.coursesId,
                        principalTable: "_courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_DeptId",
                table: "Instructors",
                column: "DeptId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseStudent_coursesId",
                table: "CourseStudent",
                column: "coursesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructors_Departments_DeptId",
                table: "Instructors",
                column: "DeptId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instructors_Departments_DeptId",
                table: "Instructors");

            migrationBuilder.DropTable(
                name: "CourseStudent");

            migrationBuilder.DropIndex(
                name: "IX_Instructors_DeptId",
                table: "Instructors");

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

            migrationBuilder.CreateIndex(
                name: "IX_Instructors__departmentId",
                table: "Instructors",
                column: "_departmentId");

            migrationBuilder.CreateIndex(
                name: "IX__courses_StudentId",
                table: "_courses",
                column: "StudentId");

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
    }
}
