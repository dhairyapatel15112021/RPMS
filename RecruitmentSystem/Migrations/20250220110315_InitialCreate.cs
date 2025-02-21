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
                name: "Candidate",
                columns: table => new
                {
                    pk_candidate_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    candidate_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    candidate_contact_number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    candidate_email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    candidate_password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cv_path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    candidate_linkdien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidate", x => x.pk_candidate_id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    pk_emp_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    emp_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    emp_email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    emp_contact_number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    emp_designation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    emp_password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    emp_joining_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.pk_emp_id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    pk_role_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role_type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.pk_role_id);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    pk_skills_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    skills_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    skills_description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.pk_skills_id);
                });

            migrationBuilder.CreateTable(
                name: "CandidateEducation",
                columns: table => new
                {
                    pk_candidate_education_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    institute_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    qualification_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    passing_year = table.Column<int>(type: "int", nullable: false),
                    percentage_score = table.Column<double>(type: "float", nullable: false),
                    fk_candidate_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateEducation", x => x.pk_candidate_education_id);
                    table.ForeignKey(
                        name: "FK_CandidateEducation_Candidate_fk_candidate_id",
                        column: x => x.fk_candidate_id,
                        principalTable: "Candidate",
                        principalColumn: "pk_candidate_id");
                });

            migrationBuilder.CreateTable(
                name: "CandidateExperiences",
                columns: table => new
                {
                    pk_candidate_experience = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    company_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    no_of_years = table.Column<int>(type: "int", nullable: false),
                    job_profile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    job_responsibility = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fk_candidate_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateExperiences", x => x.pk_candidate_experience);
                    table.ForeignKey(
                        name: "FK_CandidateExperiences_Candidate_fk_candidate_id",
                        column: x => x.fk_candidate_id,
                        principalTable: "Candidate",
                        principalColumn: "pk_candidate_id");
                });

            migrationBuilder.CreateTable(
                name: "CandidateSkill",
                columns: table => new
                {
                    pk_candidate_skill_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    skill_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    skill_experience = table.Column<int>(type: "int", nullable: false),
                    fk_candidate_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateSkill", x => x.pk_candidate_skill_id);
                    table.ForeignKey(
                        name: "FK_CandidateSkill_Candidate_fk_candidate_id",
                        column: x => x.fk_candidate_id,
                        principalTable: "Candidate",
                        principalColumn: "pk_candidate_id");
                });

            migrationBuilder.CreateTable(
                name: "Docuements",
                columns: table => new
                {
                    pk_document_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    document_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    document_file_path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    verified_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    document_verification_status = table.Column<bool>(type: "bit", nullable: false),
                    uploaded_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fk_candidate_id = table.Column<int>(type: "int", nullable: false),
                    fk_emp_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Docuements", x => x.pk_document_id);
                    table.ForeignKey(
                        name: "FK_Docuements_Candidate_fk_candidate_id",
                        column: x => x.fk_candidate_id,
                        principalTable: "Candidate",
                        principalColumn: "pk_candidate_id");
                    table.ForeignKey(
                        name: "FK_Docuements_Employees_fk_emp_id",
                        column: x => x.fk_emp_id,
                        principalTable: "Employees",
                        principalColumn: "pk_emp_id");
                });

            migrationBuilder.CreateTable(
                name: "Position",
                columns: table => new
                {
                    pk_position_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    position_title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    position_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    position_min_experience = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_open = table.Column<int>(type: "int", nullable: false),
                    comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    position_level = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    position_location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    position_creation_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    salary_range = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    qualification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fk_emp_id = table.Column<int>(type: "int", nullable: false),
                    fk_candidate_key = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position", x => x.pk_position_id);
                    table.ForeignKey(
                        name: "FK_Position_Candidate_fk_candidate_key",
                        column: x => x.fk_candidate_key,
                        principalTable: "Candidate",
                        principalColumn: "pk_candidate_id");
                    table.ForeignKey(
                        name: "FK_Position_Employees_fk_emp_id",
                        column: x => x.fk_emp_id,
                        principalTable: "Employees",
                        principalColumn: "pk_emp_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolesMap",
                columns: table => new
                {
                    pk_role_map_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fk_emp_id = table.Column<int>(type: "int", nullable: false),
                    fk_role_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesMap", x => x.pk_role_map_id);
                    table.ForeignKey(
                        name: "FK_RolesMap_Employees_fk_emp_id",
                        column: x => x.fk_emp_id,
                        principalTable: "Employees",
                        principalColumn: "pk_emp_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolesMap_Roles_fk_role_id",
                        column: x => x.fk_role_id,
                        principalTable: "Roles",
                        principalColumn: "pk_role_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    pk_application_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    application_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fk_position_id = table.Column<int>(type: "int", nullable: false),
                    fk_candidate_id = table.Column<int>(type: "int", nullable: false),
                    applicationStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.pk_application_id);
                    table.ForeignKey(
                        name: "FK_Applications_Candidate_fk_candidate_id",
                        column: x => x.fk_candidate_id,
                        principalTable: "Candidate",
                        principalColumn: "pk_candidate_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Applications_Position_fk_position_id",
                        column: x => x.fk_position_id,
                        principalTable: "Position",
                        principalColumn: "pk_position_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InterviewPanels",
                columns: table => new
                {
                    pk_interview_panel_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fk_emp_interview_id = table.Column<int>(type: "int", nullable: false),
                    fk_position_interview_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterviewPanels", x => x.pk_interview_panel_id);
                    table.ForeignKey(
                        name: "FK_InterviewPanels_Employees_fk_emp_interview_id",
                        column: x => x.fk_emp_interview_id,
                        principalTable: "Employees",
                        principalColumn: "pk_emp_id");
                    table.ForeignKey(
                        name: "FK_InterviewPanels_Position_fk_position_interview_id",
                        column: x => x.fk_position_interview_id,
                        principalTable: "Position",
                        principalColumn: "pk_position_id");
                });

            migrationBuilder.CreateTable(
                name: "PositionSkillMap",
                columns: table => new
                {
                    pk_position_skill_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fk_position_id = table.Column<int>(type: "int", nullable: false),
                    fk_skills_id = table.Column<int>(type: "int", nullable: false),
                    is_min_req_skills = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PositionSkillMap", x => x.pk_position_skill_id);
                    table.ForeignKey(
                        name: "FK_PositionSkillMap_Position_fk_position_id",
                        column: x => x.fk_position_id,
                        principalTable: "Position",
                        principalColumn: "pk_position_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PositionSkillMap_Skills_fk_skills_id",
                        column: x => x.fk_skills_id,
                        principalTable: "Skills",
                        principalColumn: "pk_skills_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReviewerPanels",
                columns: table => new
                {
                    pk_reviwer_panel_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fk_emp_review_id = table.Column<int>(type: "int", nullable: false),
                    fk_position_review_id = table.Column<int>(type: "int", nullable: false),
                    review_deadline = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewerPanels", x => x.pk_reviwer_panel_id);
                    table.ForeignKey(
                        name: "FK_ReviewerPanels_Employees_fk_emp_review_id",
                        column: x => x.fk_emp_review_id,
                        principalTable: "Employees",
                        principalColumn: "pk_emp_id");
                    table.ForeignKey(
                        name: "FK_ReviewerPanels_Position_fk_position_review_id",
                        column: x => x.fk_position_review_id,
                        principalTable: "Position",
                        principalColumn: "pk_position_id");
                });

            migrationBuilder.CreateTable(
                name: "InterviewSchedulers",
                columns: table => new
                {
                    pk_interview_scheduler_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fk_application_id = table.Column<int>(type: "int", nullable: false),
                    no_of_hr_round = table.Column<int>(type: "int", nullable: false),
                    no_of_tech_round = table.Column<int>(type: "int", nullable: false),
                    assesment_link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    interview_link = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterviewSchedulers", x => x.pk_interview_scheduler_id);
                    table.ForeignKey(
                        name: "FK_InterviewSchedulers_Applications_fk_application_id",
                        column: x => x.fk_application_id,
                        principalTable: "Applications",
                        principalColumn: "pk_application_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReviewFeedbacks",
                columns: table => new
                {
                    pk_review_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fk_application_id = table.Column<int>(type: "int", nullable: false),
                    fk_emp_id = table.Column<int>(type: "int", nullable: false),
                    comments = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewFeedbacks", x => x.pk_review_id);
                    table.ForeignKey(
                        name: "FK_ReviewFeedbacks_Applications_fk_application_id",
                        column: x => x.fk_application_id,
                        principalTable: "Applications",
                        principalColumn: "pk_application_id");
                    table.ForeignKey(
                        name: "FK_ReviewFeedbacks_Employees_fk_emp_id",
                        column: x => x.fk_emp_id,
                        principalTable: "Employees",
                        principalColumn: "pk_emp_id");
                });

            migrationBuilder.CreateTable(
                name: "Interviews",
                columns: table => new
                {
                    pk_interview_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fk_interview_scheduler_id = table.Column<int>(type: "int", nullable: false),
                    remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    round_number = table.Column<int>(type: "int", nullable: false),
                    interview_type = table.Column<int>(type: "int", nullable: false),
                    IsDone = table.Column<bool>(type: "bit", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    interview_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    interview_time = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interviews", x => x.pk_interview_id);
                    table.ForeignKey(
                        name: "FK_Interviews_InterviewSchedulers_fk_interview_scheduler_id",
                        column: x => x.fk_interview_scheduler_id,
                        principalTable: "InterviewSchedulers",
                        principalColumn: "pk_interview_scheduler_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InterviewFeedbacks",
                columns: table => new
                {
                    pk_interview_feedback_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fk_emp_id = table.Column<int>(type: "int", nullable: false),
                    comments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fk_interview_id = table.Column<int>(type: "int", nullable: false),
                    fk_position_skill_id = table.Column<int>(type: "int", nullable: false),
                    ratings = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterviewFeedbacks", x => x.pk_interview_feedback_id);
                    table.ForeignKey(
                        name: "FK_InterviewFeedbacks_Employees_fk_emp_id",
                        column: x => x.fk_emp_id,
                        principalTable: "Employees",
                        principalColumn: "pk_emp_id");
                    table.ForeignKey(
                        name: "FK_InterviewFeedbacks_Interviews_fk_interview_id",
                        column: x => x.fk_interview_id,
                        principalTable: "Interviews",
                        principalColumn: "pk_interview_id");
                    table.ForeignKey(
                        name: "FK_InterviewFeedbacks_PositionSkillMap_fk_position_skill_id",
                        column: x => x.fk_position_skill_id,
                        principalTable: "PositionSkillMap",
                        principalColumn: "pk_position_skill_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applications_fk_candidate_id",
                table: "Applications",
                column: "fk_candidate_id");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_fk_position_id",
                table: "Applications",
                column: "fk_position_id");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateEducation_fk_candidate_id",
                table: "CandidateEducation",
                column: "fk_candidate_id");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateExperiences_fk_candidate_id",
                table: "CandidateExperiences",
                column: "fk_candidate_id");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateSkill_fk_candidate_id",
                table: "CandidateSkill",
                column: "fk_candidate_id");

            migrationBuilder.CreateIndex(
                name: "IX_Docuements_fk_candidate_id",
                table: "Docuements",
                column: "fk_candidate_id");

            migrationBuilder.CreateIndex(
                name: "IX_Docuements_fk_emp_id",
                table: "Docuements",
                column: "fk_emp_id");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewFeedbacks_fk_emp_id",
                table: "InterviewFeedbacks",
                column: "fk_emp_id");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewFeedbacks_fk_interview_id",
                table: "InterviewFeedbacks",
                column: "fk_interview_id");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewFeedbacks_fk_position_skill_id",
                table: "InterviewFeedbacks",
                column: "fk_position_skill_id");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewPanels_fk_emp_interview_id",
                table: "InterviewPanels",
                column: "fk_emp_interview_id");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewPanels_fk_position_interview_id",
                table: "InterviewPanels",
                column: "fk_position_interview_id");

            migrationBuilder.CreateIndex(
                name: "IX_Interviews_fk_interview_scheduler_id",
                table: "Interviews",
                column: "fk_interview_scheduler_id");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewSchedulers_fk_application_id",
                table: "InterviewSchedulers",
                column: "fk_application_id");

            migrationBuilder.CreateIndex(
                name: "IX_Position_fk_candidate_key",
                table: "Position",
                column: "fk_candidate_key",
                unique: true,
                filter: "[fk_candidate_key] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Position_fk_emp_id",
                table: "Position",
                column: "fk_emp_id");

            migrationBuilder.CreateIndex(
                name: "IX_PositionSkillMap_fk_position_id",
                table: "PositionSkillMap",
                column: "fk_position_id");

            migrationBuilder.CreateIndex(
                name: "IX_PositionSkillMap_fk_skills_id",
                table: "PositionSkillMap",
                column: "fk_skills_id");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewerPanels_fk_emp_review_id",
                table: "ReviewerPanels",
                column: "fk_emp_review_id");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewerPanels_fk_position_review_id",
                table: "ReviewerPanels",
                column: "fk_position_review_id");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewFeedbacks_fk_application_id",
                table: "ReviewFeedbacks",
                column: "fk_application_id");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewFeedbacks_fk_emp_id",
                table: "ReviewFeedbacks",
                column: "fk_emp_id");

            migrationBuilder.CreateIndex(
                name: "IX_RolesMap_fk_emp_id",
                table: "RolesMap",
                column: "fk_emp_id");

            migrationBuilder.CreateIndex(
                name: "IX_RolesMap_fk_role_id",
                table: "RolesMap",
                column: "fk_role_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidateEducation");

            migrationBuilder.DropTable(
                name: "CandidateExperiences");

            migrationBuilder.DropTable(
                name: "CandidateSkill");

            migrationBuilder.DropTable(
                name: "Docuements");

            migrationBuilder.DropTable(
                name: "InterviewFeedbacks");

            migrationBuilder.DropTable(
                name: "InterviewPanels");

            migrationBuilder.DropTable(
                name: "ReviewerPanels");

            migrationBuilder.DropTable(
                name: "ReviewFeedbacks");

            migrationBuilder.DropTable(
                name: "RolesMap");

            migrationBuilder.DropTable(
                name: "Interviews");

            migrationBuilder.DropTable(
                name: "PositionSkillMap");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "InterviewSchedulers");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "Position");

            migrationBuilder.DropTable(
                name: "Candidate");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
