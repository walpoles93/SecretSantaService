using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecretSantaService.Domain.Parties;

namespace SecretSantaService.Infrastructure.Persistence.Configurations
{
    public class PartyConfiguration : IEntityTypeConfiguration<Party>
    {
        public void Configure(EntityTypeBuilder<Party> builder)
        {
            builder.Property(p => p.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(p => p.Date)
                .IsRequired();

            builder.Metadata
                .FindNavigation(nameof(Party.PartyMembers))
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
