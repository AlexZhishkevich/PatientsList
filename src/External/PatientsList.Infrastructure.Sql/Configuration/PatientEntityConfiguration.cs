using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PatientsList.Infrastructure.Sql.Entities;

namespace PatientsList.Infrastructure.Sql.Configuration
{
    internal class PatientEntityConfiguration : IEntityTypeConfiguration<PatientEntity>
    {
        public void Configure(EntityTypeBuilder<PatientEntity> builder)
        {
            builder.ToTable("patients");
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
               .WithOne(pn => pn.PatientEntity)
               .HasForeignKey<PatientEntity>(pi => pi.NameDataFK)
               .IsRequired();
        }
    }
}
