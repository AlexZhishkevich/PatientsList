using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PatientsList.Infrastructure.Sql.Entities;

namespace PatientsList.Infrastructure.Sql.Configuration
{
    internal class PatientNameEntityConfiguration : IEntityTypeConfiguration<PatientNameDataEntity>
    {
        public void Configure(EntityTypeBuilder<PatientNameDataEntity> builder)
        {
            builder.ToTable("patient_names");
            builder.HasKey(pi => pi.Id);

            builder
                .Property(pi => pi.Use)
                .HasColumnName("use")
                .IsRequired(false);

            builder
                .Property(pi => pi.Family)
                .HasMaxLength(255)
                .HasColumnName("family");

            builder
                .Property(pi => pi.GivenNames)
                .HasMaxLength(255)
                .HasColumnName("given");
        }
    }
}
