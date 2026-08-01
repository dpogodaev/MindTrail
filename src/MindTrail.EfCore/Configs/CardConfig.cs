using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindTrail.Domain.ValueObjects;
using MindTrail.EfCore.Entities;

namespace MindTrail.EfCore.Configs;

public class CardConfig : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> entity)
    {
        entity.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        entity.HasIndex(x => x.Title);
        entity.Property(x => x.Title)
            .HasMaxLength(CardTitle.MaxLength)
            .IsRequired();

        entity.Property(x => x.Content)
            .HasMaxLength(CardContent.MaxLength);
    }
}