using SecretSantaService.Domain.Common.Models;
using System;

namespace SecretSantaService.Domain.Parties
{
    public class PartyMember : Entity
    {
        public PartyMember(string name, string email, string address)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Address = address ?? throw new ArgumentNullException(nameof(address));
        }

        private PartyMember() { }

        // populated by EF Core
        public int PartyId { get; private set; }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Address { get; private set; }
    }
}
