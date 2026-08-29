using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindTrail.Domain.ValueObjects;
using MindTrail.EfCore.Entities;

namespace MindTrail.EfCore.Configs;

/// <summary>
/// Configures the entity type mapping for <see cref="Card"/>.
/// </summary>
public class CardConfig : IEntityTypeConfiguration<Card>
{
    /// <inheritdoc/>
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