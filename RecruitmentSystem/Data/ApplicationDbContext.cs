using Microsoft.EntityFrameworkCore;
using RecruitmentSystem.Models;

namespace RecruitmentSystem.Data{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}
   
        public DbSet<SkillsModel> Skills {get;set;}
             public DbSet<EmployeesModel> Employees {get;set;}
        public DbSet<CandidateModel> Candidate {get;set;}
        public DbSet<PositionModel> Position {get;set;}

        public DbSet<PositionSkillMapModel> PositionSkillMap {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder){

            // registering primary key
            modelBuilder.Entity<EmployeesModel>().HasKey(e => e.pk_emp_id);
            modelBuilder.Entity<SkillsModel>().HasKey(e => e.pk_skills_id);
            modelBuilder.Entity<CandidateModel>().HasKey(e => e.pk_candidate_id);
            modelBuilder.Entity<PositionModel>().HasKey(e => e.pk_position_id);
            modelBuilder.Entity<PositionSkillMapModel>().HasKey(e => e.pk_position_skill_id);

            // Registering Relationship

              modelBuilder.Entity<EmployeesModel>()
                    .HasMany(e => e.positions)
                    .WithOne(e => e.Employees)
                    .HasForeignKey(e => e.fk_emp_id)
                    .HasPrincipalKey(e => e.pk_emp_id);
                
             modelBuilder.Entity<PositionModel>()
                    .HasOne(e => e.Employees)
                    .WithMany(e => e.positions)
                    .HasForeignKey(e => e.fk_emp_id)
                    .IsRequired();

            modelBuilder.Entity<CandidateModel>()
                    .HasOne(e => e.Position)
                    .WithOne(e => e.Candidate)
                    .HasForeignKey<PositionModel>(e => e.fk_candidate_key)
                    .IsRequired(false);
            
            modelBuilder.Entity<PositionModel>()
                .HasOne(e => e.Candidate)
                .WithOne(e => e.Position)
                .HasForeignKey<PositionModel>(e => e.fk_candidate_key)
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

        }

    }
}
