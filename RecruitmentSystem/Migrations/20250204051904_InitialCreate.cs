using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecruitmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Pk_emp_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpDesignation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Emp_joining_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Pk_emp_id);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    pk_skills_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    skills_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    skills_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_min_req_skills = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.pk_skills_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Skills");
        }
    }
}
