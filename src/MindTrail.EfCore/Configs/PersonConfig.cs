using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindTrail.Domain.ValueObjects;
using MindTrail.EfCore.Entities;

namespace MindTrail.EfCore.Configs;

public class PersonConfig : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> entity)
    {
        entity.HasIndex(x => x.FullName);
        entity.Property(x => x.FullName)
            .HasMaxLength(PersonFullName.MaxNameLength)
            .IsRequired();

        entity.HasOne(p => p.BirthCountry)
            .WithMany()
            .HasForeignKey(p => p.BirthCountryId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}