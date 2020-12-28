using Microsoft.EntityFrameworkCore;
using SecretSantaService.Application.Common.Interfaces;
using SecretSantaService.Domain.Pairings;
using SecretSantaService.Domain.Parties;
using System.Reflection;

namespace SecretSantaService.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Party> Parties { get; set; }

        public DbSet<PartyMember> PartyMembers { get; set; }

        public DbSet<Pairing> Pairings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
