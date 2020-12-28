using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecretSantaService.Domain.Parties;

namespace SecretSantaService.Infrastructure.Persistence.Configurations
{
    public class PartyMemberConfiguration : IEntityTypeConfiguration<PartyMember>
    {
        public void Configure(EntityTypeBuilder<PartyMember> builder)
        {
            builder.Property(pm => pm.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(pm => pm.Address)
                .HasMaxLength(500)
                .IsRequired();

            builder.HasOne<Party>()
                .WithMany(p => p.PartyMembers)
                .HasForeignKey(pm => pm.PartyId);
        }
    }
}
