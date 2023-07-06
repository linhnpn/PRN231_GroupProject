using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GroupProject_HRM_Library.Models
{
    public class HumanResourceManagementContext : DbContext
    {
        public HumanResourceManagementContext() { }
        public HumanResourceManagementContext(DbContextOptions<HumanResourceManagementContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            string connectionString = config["ConnectionStrings:Database"];
            optionsBuilder.UseSqlServer(connectionString);
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Bonus> Bonuses { get; set; }
        public virtual DbSet<EmployeeProject> EmployeeProjects { get; set; }
        public virtual DbSet<Income> Incomes { get; set; }
        public virtual DbSet<LeaveLog> LeaveLogs { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<OvertimeLog> OvertimeLogs { get; set; }
        public virtual DbSet<Payroll> Payrolls { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Tax> Taxes { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bonus>(entity =>
            {
                entity.HasKey(e => e.BonusID);

                entity.HasOne(e => e.Employee)
                    .WithMany(i => i.Bonuses)
                    .HasForeignKey(e => e.EmployeeID);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasIndex(e => e.UserName)
                .IsUnique();

                entity.HasIndex(e => e.PhoneNumber)
                .IsUnique();

                entity.HasIndex(e => e.EmailAddress)
                .IsUnique();

                entity.HasIndex(e => e.UserName)
                .IsUnique();

                entity.HasKey(e => e.EmployeeID);

                entity.HasOne(e => e.Role)
                    .WithMany(r => r.Employees)
                    .HasForeignKey(e => e.RoleID);
            });

            modelBuilder.Entity<EmployeeProject>(entity =>
            {
                entity.HasKey(e => new { e.ProjectID, e.EmployeeID });

                entity.HasOne(e => e.Employee)
                    .WithMany(ep => ep.EmployeeProjects)
                    .HasForeignKey(e => e.EmployeeID);

                entity.HasOne(e => e.Project)
                    .WithMany(p => p.EmployeeProjects)
                    .HasForeignKey(e => e.ProjectID);
            });

            modelBuilder.Entity<Income>(entity =>
            {
                entity.HasKey(e => e.IncomeID);

                entity.HasOne(e => e.Employee)
                    .WithMany(i => i.Incomes)
                    .HasForeignKey(e => e.EmployeeID);
            });

            modelBuilder.Entity<LeaveLog>(entity =>
            {
                entity.HasKey(e => e.LeaveLogID);

                entity.HasOne(e => e.Employee)
                    .WithMany(l => l.LeaveLogs)
                    .HasForeignKey(e => e.EmployeeID);
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(e => e.NotificationID);

                entity.HasOne(e => e.Employee)
                    .WithMany(n => n.Notifications)
                    .HasForeignKey(e => e.EmployeeID);
            });

            modelBuilder.Entity<OvertimeLog>(entity =>
            {
                entity.HasKey(e => e.OvertimeID);

                entity.HasOne(e => e.Employee)
                    .WithMany(o => o.OvertimeLogs)
                    .HasForeignKey(e => e.EmployeeID);
            });

            modelBuilder.Entity<Payroll>(entity =>
            {
                entity.HasKey(e => e.PayrollID);

                entity.HasOne(e => e.Employee)
                    .WithMany(p => p.Payrolls)
                    .HasForeignKey(e => e.EmployeeID);
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(e => e.ProjectID);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.RoleID);
            });

            modelBuilder.Entity<Tax>(entity =>
            {
                entity.HasKey(e => e.TaxID);
            });

            base.OnModelCreating(modelBuilder);
        }

    }
}
