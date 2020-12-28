using SecretSantaService.Domain.Common.Models;
using System;

namespace SecretSantaService.Domain.Pairings
{
    public class Pairing : Entity
    {
        internal Pairing(int partyId, string identifier, int donorId, int recipientId)
        {
            PartyId = partyId;
            Identifier = identifier ?? throw new ArgumentNullException(nameof(identifier));
            DonorId = donorId;
            RecipientId = recipientId;
        }

        private Pairing() { }

        public int PartyId { get; private set; }
        public string Identifier { get; private set; }
        public int DonorId { get; private set; }
        public int RecipientId { get; private set; }
    }
}
