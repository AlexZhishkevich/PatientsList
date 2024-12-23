using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PatientsList.Infrastructure.Sql.Entities;

namespace PatientsList.Infrastructure.Sql.Configuration
{
    internal class PatientInfoEntityConfiguration : IEntityTypeConfiguration<PatientInfoEntity>
    {
        public void Configure(EntityTypeBuilder<PatientInfoEntity> builder)
        {
            builder.ToTable("patients_data");
            builder.HasKey(pi => pi.Id);

            builder
                .Property(pi => pi.Gender)
                .HasColumnName("gender")
                .IsRequired(false);

            builder
                .Property(pi => pi.BirthDate)
                .HasColumnName("birth_date")
                .IsRequired();

            builder
                .Property(pi => pi.Active)
                .HasColumnName("active")
                .IsRequired(false);

            builder
               .HasOne(pi => pi.NameDataEntity)
               .WithOne(pn => pn.PatientInfoEntity)
               .HasForeignKey<PatientInfoEntity>(pi => pi.NameDataFK)
               .IsRequired();
        }
    }
}
