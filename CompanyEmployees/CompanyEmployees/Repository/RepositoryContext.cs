using CompanyEmployees.Entities.Configuration;
using CompanyEmployees.Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CompanyEmployees.Repository
{
    public class RepositoryContext : IdentityDbContext<User>
    {
        public RepositoryContext(DbContextOptions options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }

        public DbSet<Company>? Companies { get; set; }
        public DbSet<Employee>? Employees { get; set; }
        public DbSet<Cliente>? Cliente { get; set; }
        public DbSet<TipoSuscripcion>? TipoSuscripcion { get; set; }
        public DbSet<Suscripcion>? Suscripcion { get; set; }
        public DbSet<VentaSuscripcion>? VentaSuscripcion { get; set; }
        public DbSet<RegistroAsistencias>? RegistroAsistencias { get; set; }
  }
}
