using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecruitmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "interview_link",
                table: "InterviewSchedulers");

            migrationBuilder.AddColumn<string>(
                name: "interview_link",
                table: "Interviews",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "interview_link",
                table: "Interviews");

            migrationBuilder.AddColumn<string>(
                name: "interview_link",
                table: "InterviewSchedulers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
