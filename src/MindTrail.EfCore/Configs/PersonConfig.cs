using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindTrail.EfCore.Entities;

namespace MindTrail.EfCore.Configs;

public class PersonConfig : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> entity)
    {
        entity.Property(x => x.FullName).HasMaxLength(64).IsRequired();
        entity.HasIndex(x => x.FullName);

        entity.HasOne(p => p.BirthCountry).WithMany()
            .HasForeignKey(p => p.BirthCountryId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}