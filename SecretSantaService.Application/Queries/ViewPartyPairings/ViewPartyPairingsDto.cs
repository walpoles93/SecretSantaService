using System;
using System.Collections.Generic;

namespace SecretSantaService.Application.Queries.ViewPartyPairings
{
    public class ViewPartyPairingsDto
    {
        public ViewPartyPairingsDto(string partyName, IEnumerable<PairingDto> pairings)
        {
            PartyName = partyName ?? throw new ArgumentNullException(nameof(partyName));
            Pairings = pairings ?? throw new ArgumentNullException(nameof(pairings));
        }

        public string PartyName { get; set; }
        public IEnumerable<PairingDto> Pairings { get; set; }

        public class PairingDto
        {
            public string DonorName { get; set; }
            public string RecipientName { get; set; }
        }
    }
}
