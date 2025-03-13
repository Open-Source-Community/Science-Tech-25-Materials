using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Courses.Migrations
{
    /// <inheritdoc />
    public partial class Data_seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "HiringDate", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Physics" },
                    { 2, new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mathematics" },
                    { 3, new DateTime(2021, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chemistry" },
                    { 4, new DateTime(2018, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Biology" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "FName", "LName", "StAddress", "StAge" },
                values: new object[,]
                {
                    { 1, "John", "Doe", "123 Main St", 20 },
                    { 2, "Jane", "Smith", "456 Elm St", 22 },
                    { 3, "Alice", "Johnson", "789 Oak St", 21 },
                    { 4, "Bob", "Brown", "101 Pine St", 23 },
                    { 5, "Charlie", "Davis", "202 Maple St", 22 }
                });

            migrationBuilder.InsertData(
                table: "_courses",
                columns: new[] { "Id", "Description", "Duration", "Name" },
                values: new object[,]
                {
                    { 1, "Introduction to Algebra and Calculus", 30, "Mathematics" },
                    { 2, "Fundamentals of Physics", 40, "Physics" },
                    { 3, "Basic Principles of Chemistry", 35, "Chemistry" },
                    { 4, "Introduction to Biological Sciences", 25, "Biology" }
                });

            migrationBuilder.InsertData(
                table: "Instructors",
                columns: new[] { "Id", "Address", "Bonus", "DeptId", "HourRate", "Name", "Salary" },
                values: new object[,]
                {
                    { 1, "123 University Ave", 5000.00m, 1, 50.00m, "Dr. Smith", 75000.00m },
                    { 2, "456 College St", 6000.00m, 1, 55.00m, "Dr. Johnson", 80000.00m },
                    { 3, "789 Campus Rd", 7000.00m, 1, 60.00m, "Dr. Brown", 85000.00m },
                    { 4, "101 School Ln", 8000.00m, 2, 65.00m, "Dr. Davis", 90000.00m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "_courses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "_courses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "_courses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "_courses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
