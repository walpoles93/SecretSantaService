using System;
using System.Collections.Generic;

namespace SecretSantaService.Application.Queries.ViewPartyLinks
{
    public class ViewPartyLinksDto
    {
        public ViewPartyLinksDto(IEnumerable<ViewPartyMember> partyMembers)
        {
            PartyMembers = partyMembers ?? throw new ArgumentNullException(nameof(partyMembers));
        }

        public IEnumerable<ViewPartyMember> PartyMembers { get; set; }

        public class ViewPartyMember
        {
            public string Name { get; set; }
            public string PartyName { get; set; }
            public string PairingIdentifier { get; set; }
        }
    }
}
