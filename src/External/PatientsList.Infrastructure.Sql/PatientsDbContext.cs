using Microsoft.EntityFrameworkCore;
using PatientsList.Infrastructure.Sql.Configuration;
using PatientsList.Infrastructure.Sql.Entities;

namespace PatientsList.Infrastructure.Sql
{
    public class PatientsDbContext : DbContext
    {
        public PatientsDbContext(DbContextOptions<PatientsDbContext> options) : base(options) { }

        public DbSet<PatientInfoEntity>? PatientsInfoSet { get; set; }
        public DbSet<PatientNameDataEntity>? PatientNamesSet { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PatientInfoEntityConfiguration());
            modelBuilder.ApplyConfiguration(new PatientNameEntityConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
