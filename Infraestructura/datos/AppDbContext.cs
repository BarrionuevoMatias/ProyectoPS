using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Dominio.entidades;

namespace Infraestructura.datos
{
    public class AppDbContext : DbContext
    {
        public DbSet<ApproverRole> approverRoles { get; set; }
        public DbSet<ApprovalStatus> approvalStatuses { get; set; }
        public DbSet<Area> areas { get; set; }
        public DbSet<ProjectType> projectTypes { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<ApprovalRule> approvalRules { get; set; }
        public DbSet<ProjectProposal> projectProposals { get; set; }
        public DbSet<ProjectApprovalStep> projectApprovalSteps { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Area>(entity => {
                entity.ToTable("Area");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                .HasColumnType("int")
                .ValueGeneratedOnAdd();
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<ProjectType>(entity => {
                entity.ToTable("Projectype");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnType("int")
                    .ValueGeneratedOnAdd();
                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<ApprovalStatus>(entity => { 
                entity.ToTable("ApprovalStatus");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnType("int")
                    .ValueGeneratedOnAdd();
                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<ApproverRole>(entity => { 
                entity.ToTable("ApproverRole");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnType("int")
                    .ValueGeneratedOnAdd();
                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<User>(entity => { 
                entity.ToTable("User");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnType("int")
                    .ValueGeneratedOnAdd();
                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasMaxLength(25);
                entity.Property(e => e.Email)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasMaxLength(100);
                entity.HasOne(u => u.ApproverRole)
                    .WithMany(r => r.Users)
                    .HasForeignKey(u => u.Role);
            });

            modelBuilder.Entity<ApprovalRule>(entity =>
            {
                entity.ToTable("ApprovalRule");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnType("int")
                    .ValueGeneratedOnAdd();
                entity.Property(e => e.MinAmount)
                    .IsRequired()
                    .HasColumnType("decimal(38,2)");
                entity.Property(e => e.MaxAmount)
                    .IsRequired()
                    .HasColumnType("decimal(38,2)");
                entity.Property(e => e.StepOrder)
                    .IsRequired()
                    .HasColumnType("int");

                entity.HasOne(entity => entity.AreaNavigation)
                    .WithMany(area => area.ApprovalRules)
                    .HasForeignKey(entity => entity.Area);

                entity.HasOne(entity => entity.TypeNavigation)
                    .WithMany(type => type.ApprovalRules)
                    .HasForeignKey(entity => entity.type);

                entity.HasOne(entity => entity.ApproverRole)
                    .WithMany(role => role.ApprovalRules)
                    .HasForeignKey(entity => entity.ApproverRoleId);

            });

            modelBuilder.Entity<ProjectProposal>(entity =>
            {
                entity.ToTable("ProjectProposal");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                .HasColumnType("int")
                .ValueGeneratedOnAdd();
                entity.Property(e => e.Title)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasMaxLength(255);
                entity.Property(e => e.Description)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnType("varchar(max)");
                entity.Property(e => e.EstimatedAmount)
                    .IsRequired()
                    .HasColumnType("decimal(38,2)");
                entity.Property(e => e.EstimatedDuration)
                    .IsRequired()
                    .HasColumnType("int");

                entity.HasOne(entity => entity.AreaNavigation)
                    .WithMany(area => area.ProjectProposals)
                    .HasForeignKey(entity => entity.Area)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(entity => entity.TypeNavigation)
                    .WithMany(type => type.ProjectProposals)
                    .HasForeignKey(entity => entity.type)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(entity => entity.StatusNavigation)
                    .WithMany(status => status.ProjectProposals)
                    .HasForeignKey(entity => entity.Status)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(entity => entity.CreatedByNavigation)
                    .WithMany(user => user.ProjectProposals)
                    .HasForeignKey(entity => entity.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);

            });

            modelBuilder.Entity<ProjectApprovalStep>(entity =>
            {
                entity.ToTable("ProjectApprovalStep");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                .HasColumnType("int")
                .ValueGeneratedOnAdd();
                entity.Property(e => e.StepOrder)
                    .IsRequired()
                    .HasColumnType("int");
                entity.Property(e => e.DecisionDate)
                    
                    .HasColumnType("datetime");
                entity.Property(e => e.Observations)
                    
                    .IsUnicode(false)
                    .HasColumnType("varchar(max)");
                entity.Property(e => e.ApproverUserId)
                    
                    .HasColumnType("int");
                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnType("int");

                entity.HasOne(entity => entity.ProjectProposal)
                    .WithMany(proposal => proposal.ProjectApprovalSteps)
                    .HasForeignKey(entity => entity.ProjectProposalId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(entity => entity.ApproverRole)
                    .WithMany(role => role.ProjectApprovalSteps)
                    .HasForeignKey(entity => entity.ApproverRoleId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(entity => entity.StatusNavigation)
                    .WithMany(status => status.ProjectApprovalSteps)
                    .HasForeignKey(entity => entity.Status)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(entity => entity.ApproverUser)
                    .WithMany(user => user.ProjectApprovalSteps)
                    .HasForeignKey(entity => entity.ApproverUserId)
                    .OnDelete(DeleteBehavior.NoAction);


            });
            SeedData(modelBuilder);
        }
        

        private void SeedData(ModelBuilder modelBuilder)
        {
            // ApproverRoles
            modelBuilder.Entity<ApproverRole>().HasData(
                new ApproverRole { Id = 1, Name = "Líder de Área" },
                new ApproverRole { Id = 2, Name = "Gerente" },
                new ApproverRole { Id = 3, Name = "Director" },
                new ApproverRole { Id = 4, Name = "Comité Técnico" }
            );

            // ApprovalStatuses
            modelBuilder.Entity<ApprovalStatus>().HasData(
                new ApprovalStatus { Id = 1, Name = "Pending" },
                new ApprovalStatus { Id = 2, Name = "Approved" },
                new ApprovalStatus { Id = 3, Name = "Rejected" },
                new ApprovalStatus { Id = 4, Name = "Observed" }
            );

            // Areas
            modelBuilder.Entity<Area>().HasData(
                new Area { Id = 1, Nombre = "Finanzas" },
                new Area { Id = 2, Nombre = "Tecnología" },
                new Area { Id = 3, Nombre = "Recursos Humanos" },
                new Area { Id = 4, Nombre = "Operaciones" }
            );

            // ProjectTypes
            modelBuilder.Entity<ProjectType>().HasData(
                new ProjectType { Id = 1, Name = "Mejora de procesos" },
                new ProjectType { Id = 2, Name = "Innovación y desarrollo" },
                new ProjectType { Id = 3, Name = "Infraestructura" },
                new ProjectType { Id = 4, Name = "Capacitación interna" }
            );

            // Users
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Jose Ferreyra", Email = "jferreyra@unaj.com", Role = 2 },
                new User { Id = 2, Name = "Ana Lucero", Email = "alucero@unaj.com", Role = 1 },
                new User { Id = 3, Name = "Gonzalo Molinas", Email = "gmolinas@unaj.com", Role = 2 },
                new User { Id = 4, Name = "Lucas Olivera", Email = "lolivera@unaj.com", Role = 3 },
                new User { Id = 5, Name = "Danilo Fagundez", Email = "dfagundez@unaj.com", Role = 4 },
                new User { Id = 6, Name = "Gabriel Galli", Email = "ggalli@unaj.com", Role = 4 }
            );

            // ApprovalRules
            modelBuilder.Entity<ApprovalRule>().HasData(
                new ApprovalRule { Id = 1, MinAmount = 0, MaxAmount = 100000, Area = null, type = null, StepOrder = 1, ApproverRoleId = 1 },
                new ApprovalRule { Id = 2, MinAmount = 5000, MaxAmount = 20000, Area = null, type = null, StepOrder = 2, ApproverRoleId = 2 },
                new ApprovalRule { Id = 3, MinAmount = 0, MaxAmount = 20000, Area = 2, type = 2, StepOrder = 1, ApproverRoleId = 2 },
                new ApprovalRule { Id = 4, MinAmount = 20000, MaxAmount = decimal.MaxValue, Area = null, type = null, StepOrder = 3, ApproverRoleId = 3 },
                new ApprovalRule { Id = 5, MinAmount = 5000, MaxAmount = decimal.MaxValue, Area = 1, type = 1, StepOrder = 2, ApproverRoleId = 2 },
                new ApprovalRule { Id = 6, MinAmount = 0, MaxAmount = 10000, Area = null, type = 2, StepOrder = 1, ApproverRoleId = 1 },
                new ApprovalRule { Id = 7, MinAmount = 0, MaxAmount = 10000, Area = 2, type = 1, StepOrder = 1, ApproverRoleId = 4 },
                new ApprovalRule { Id = 8, MinAmount = 10000, MaxAmount = 30000, Area = 2, type = null, StepOrder = 2, ApproverRoleId = 2 },
                new ApprovalRule { Id = 9, MinAmount = 30000, MaxAmount = decimal.MaxValue, Area = 3, type = null, StepOrder = 2, ApproverRoleId = 3 },
                new ApprovalRule { Id = 10, MinAmount = 0, MaxAmount = 50000, Area = null, type = 4, StepOrder = 1, ApproverRoleId = 4 }
            );
        }

    }
}
