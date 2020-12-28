using Microsoft.EntityFrameworkCore;
using SecretSantaService.Domain.Pairings;
using SecretSantaService.Domain.Parties;
using System.Threading;
using System.Threading.Tasks;

namespace SecretSantaService.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Party> Parties { get; }
        DbSet<PartyMember> PartyMembers { get; }
        DbSet<Pairing> Pairings { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
