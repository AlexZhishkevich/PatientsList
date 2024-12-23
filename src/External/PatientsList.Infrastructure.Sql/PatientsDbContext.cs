using Microsoft.EntityFrameworkCore;
using PatientsList.Infrastructure.Sql.Configuration;

namespace PatientsList.Infrastructure.Sql
{
    public class PatientsDbContext : DbContext
    {
        public PatientsDbContext(DbContextOptions<PatientsDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PatientInfoEntityConfiguration());
            modelBuilder.ApplyConfiguration(new PatientNameEntityConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
