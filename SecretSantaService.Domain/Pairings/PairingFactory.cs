using SecretSantaService.Domain.Common.Interfaces;
using System;

namespace SecretSantaService.Domain.Pairings
{
    public class PairingFactory : IPairingFactory
    {
        private readonly IShortUuidGenerator _uuidGenerator;

        public PairingFactory(IShortUuidGenerator uuidGenerator)
        {
            _uuidGenerator = uuidGenerator ?? throw new ArgumentNullException(nameof(uuidGenerator));
        }

        public Pairing CreatePairing(int partyId, int donorId, int recipientId)
        {
            var uuid = _uuidGenerator.Next();

            return new Pairing(partyId, uuid, donorId, recipientId);
        }
    }
}
