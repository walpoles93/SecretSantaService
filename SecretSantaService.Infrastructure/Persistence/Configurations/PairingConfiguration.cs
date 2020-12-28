using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecretSantaService.Domain.Pairings;
using SecretSantaService.Domain.Parties;

namespace SecretSantaService.Infrastructure.Persistence.Configurations
{
    public class PairingConfiguration : IEntityTypeConfiguration<Pairing>
    {
        public void Configure(EntityTypeBuilder<Pairing> builder)
        {
            builder.Property(p => p.Identifier)
                .HasMaxLength(8)
                .IsRequired();

            builder.HasOne<Party>()
                .WithMany()
                .HasForeignKey(p => p.PartyId);

            builder.HasOne<PartyMember>()
                .WithMany()
                .HasForeignKey(p => p.DonorId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<PartyMember>()
                .WithMany()
                .HasForeignKey(p => p.RecipientId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
