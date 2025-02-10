using DocumentFormat.OpenXml.ExtendedProperties;
using Microsoft.EntityFrameworkCore;
using RecruitmentSystem.Models;

namespace RecruitmentSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<SkillsModel> Skills { get; set; }
        public DbSet<EmployeesModel> Employees { get; set; }
        public DbSet<CandidateModel> Candidate { get; set; }
        public DbSet<PositionModel> Position { get; set; }
        public DbSet<PositionSkillMapModel> PositionSkillMap { get; set; }
        public DbSet<RoleModel> Roles { get; set; }
        public DbSet<EmpRoleMapModel> RolesMap { get; set; }
        public DbSet<DocuementModel> Docuements { get; set; }
        // public DbSet<ReviewerPanelModel> ReviewerPanels { get; set; }
        // public DbSet<InterviewPanelModel> InterviewPanels { get; set; }
        public DbSet<ApplicationModel> Applications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // registering primary key
            modelBuilder.Entity<EmployeesModel>().HasKey(m => m.pk_emp_id);
            modelBuilder.Entity<SkillsModel>().HasKey(m => m.pk_skills_id);
            modelBuilder.Entity<CandidateModel>().HasKey(m => m.pk_candidate_id);
            modelBuilder.Entity<PositionModel>().HasKey(m => m.pk_position_id);
            modelBuilder.Entity<PositionSkillMapModel>().HasKey(m => m.pk_position_skill_id);
            modelBuilder.Entity<RoleModel>().HasKey(m => m.pk_role_id);
            modelBuilder.Entity<EmpRoleMapModel>().HasKey(m => m.pk_role_map_id);
            modelBuilder.Entity<DocuementModel>().HasKey(m => m.pk_document_id);
            //  modelBuilder.Entity<ReviewerPanelModel>().HasKey(m => m.pk_reviwer_panel_id);
            //  modelBuilder.Entity<InterviewPanelModel>().HasKey(m => m.pk_interview_panel_id);
            modelBuilder.Entity<ApplicationModel>().HasKey(m => m.pk_application_id);

            // Registering Relationship

            // relationship for which employee create position
            modelBuilder.Entity<EmployeesModel>()
                  .HasMany(e => e.positions)
                  .WithOne(p => p.Employees)
                  .HasForeignKey(e => e.fk_emp_id)
                  .HasPrincipalKey(p => p.pk_emp_id);

            modelBuilder.Entity<PositionModel>()
                   .HasOne(e => e.Employees)
                   .WithMany(p => p.positions)
                   .HasForeignKey(e => e.fk_emp_id)
                   .IsRequired();


            // relationship for candidate and position for setting up selected candidate for particluar position.
            modelBuilder.Entity<CandidateModel>()
                    .HasOne(c => c.Position)
                    .WithOne(p => p.Candidate)
                    .HasForeignKey<PositionModel>(p => p.fk_candidate_key)
                    .IsRequired(false);

            modelBuilder.Entity<PositionModel>()
                .HasOne(p => p.Candidate)
                .WithOne(c => c.Position)
                .HasForeignKey<PositionModel>(p => p.fk_candidate_key)
                .IsRequired(false);

            modelBuilder.Entity<PositionModel>()
                .HasMany(e => e.PositionSkill)
                .WithOne(e => e.Position)
                .HasForeignKey(e => e.fk_position_id)
                .HasPrincipalKey(e => e.pk_position_id);

            modelBuilder.Entity<PositionSkillMapModel>()
                .HasOne(e => e.Position)
                .WithMany(e => e.PositionSkill)
                .HasForeignKey(e => e.fk_position_id)
                .IsRequired();

            modelBuilder.Entity<SkillsModel>()
                .HasMany(e => e.PositionSkill)
                .WithOne(e => e.Skills)
                .HasForeignKey(e => e.fk_skills_id)
                .HasPrincipalKey(e => e.pk_skills_id);

            modelBuilder.Entity<PositionSkillMapModel>()
                .HasOne(e => e.Skills)
                .WithMany(e => e.PositionSkill)
                .HasForeignKey(e => e.fk_skills_id)
                .IsRequired();

            // relationship for employee and roles

            modelBuilder.Entity<EmployeesModel>()
            .HasMany(e => e.RoleMap)
            .WithOne(e => e.Employees)
            .HasForeignKey(e => e.fk_emp_id)
            .HasPrincipalKey(e => e.pk_emp_id);

            modelBuilder.Entity<EmpRoleMapModel>()
            .HasOne(e => e.Employees)
            .WithMany(e => e.RoleMap)
            .HasForeignKey(e => e.fk_emp_id)
            .IsRequired();

            modelBuilder.Entity<RoleModel>()
            .HasMany(e => e.RoleMap)
            .WithOne(e => e.Role)
            .HasForeignKey(e => e.fk_role_id)
            .HasPrincipalKey(e => e.pk_role_id);

            modelBuilder.Entity<EmpRoleMapModel>()
            .HasOne(e => e.Role)
            .WithMany(e => e.RoleMap)
            .HasForeignKey(e => e.fk_role_id)
            .IsRequired();

            // relationship between document and candidate
            modelBuilder.Entity<CandidateModel>()
            .HasMany(c => c.Document)
            .WithOne(e => e.candidate)
            .HasForeignKey(d => d.fk_candidate_id)
            .HasPrincipalKey(c => c.pk_candidate_id)
            .IsRequired(false);

            modelBuilder.Entity<DocuementModel>()
            .HasOne(e => e.candidate)
            .WithMany(e => e.Document)
            .HasForeignKey(e => e.fk_candidate_id)
            .IsRequired(false);

            // relationship between document and employee, which emp verified which document
            modelBuilder.Entity<EmployeesModel>()
            .HasMany(e => e.Document)
            .WithOne(d => d.employees)
            .HasForeignKey(d => d.fk_emp_id)
            .HasPrincipalKey(e => e.pk_emp_id)
            .IsRequired(false);

            modelBuilder.Entity<DocuementModel>()
            .HasOne(d => d.employees)
            .WithMany(e => e.Document)
            .HasForeignKey(d => d.fk_emp_id)
            .IsRequired(false);

            // relationship between Employee and ReviewPanel
            // modelBuilder.Entity<EmployeesModel>()
            // .HasMany(e => e.Reviews)
            // .WithOne(r => r.employees)
            // .HasForeignKey(r => r.fk_emp_review_id)
            // .HasPrincipalKey(e => e.pk_emp_id);

            // modelBuilder.Entity<ReviewerPanelModel>()
            // .HasOne(r => r.employees)
            // .WithMany(e => e.Reviews)
            // .HasForeignKey(r => r.fk_emp_review_id)
            // .OnDelete(DeleteBehavior.NoAction)
            // .IsRequired();

            // relationship between Position and ReviewPanel
            //     modelBuilder.Entity<PositionModel>()
            //    .HasMany(p => p.Reviews)
            //    .WithOne(r => r.position)
            //    .HasForeignKey(r => r.fk_position_review_id)
            //    .HasPrincipalKey(p => p.pk_position_id);

            //     modelBuilder.Entity<ReviewerPanelModel>()
            //     .HasOne(r => r.position)
            //     .WithMany(p => p.Reviews)
            //     .HasForeignKey(r => r.fk_position_review_id)
            //     .OnDelete(DeleteBehavior.NoAction)
            //     .IsRequired();

            // relationship between Emp and InterviewPanel
            // modelBuilder.Entity<EmployeesModel>()
            // .HasMany(e => e.Interviews)
            // .WithOne(i => i.employees)
            // .HasForeignKey(i => i.fk_emp_interview_id)
            // .HasPrincipalKey(e => e.pk_emp_id);

            // modelBuilder.Entity<InterviewPanelModel>()
            // .HasOne(i => i.employees)
            // .WithMany(e => e.Interviews)
            // .HasForeignKey(i => i.fk_emp_interview_id)
            // .OnDelete(DeleteBehavior.NoAction)
            // .IsRequired();

            // relationship between Position and InterviewPanel
            // modelBuilder.Entity<PositionModel>()
            // .HasMany(p => p.Interviews)
            // .WithOne(i => i.position)
            // .HasForeignKey(i => i.fk_position_interview_id)
            // .HasPrincipalKey(p => p.pk_position_id);

            // modelBuilder.Entity<InterviewPanelModel>()
            // .HasOne(i => i.position)
            // .WithMany(p => p.Interviews)
            // .HasForeignKey(i => i.fk_position_interview_id)
            // .OnDelete(DeleteBehavior.NoAction)
            // .IsRequired();

            // relationship between application and candidate
            modelBuilder.Entity<CandidateModel>()
            .HasMany(c => c.Applications)
            .WithOne(a => a.candidate)
            .HasForeignKey(a => a.fk_candidate_id)
            .HasPrincipalKey(c => c.pk_candidate_id);

            modelBuilder.Entity<ApplicationModel>()
            .HasOne(a => a.candidate)
            .WithMany(c => c.Applications)
            .HasForeignKey(a => a.fk_candidate_id)
            .IsRequired();

            // relationship between application and position
            modelBuilder.Entity<PositionModel>()
            .HasMany(p => p.Applications)
            .WithOne(a => a.position)
            .HasForeignKey(a => a.fk_position_id)
            .HasPrincipalKey(p => p.pk_position_id);

            modelBuilder.Entity<ApplicationModel>()
            .HasOne(a => a.position)
            .WithMany(p => p.Applications)
            .HasForeignKey(a => a.fk_position_id)
            .IsRequired();

        }

    }
}
