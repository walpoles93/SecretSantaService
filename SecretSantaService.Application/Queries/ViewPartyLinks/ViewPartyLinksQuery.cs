using MediatR;
using System;

namespace SecretSantaService.Application.Queries.ViewPartyLinks
{
    public class ViewPartyLinksQuery : IRequest<ViewPartyLinksDto>
    {
        public ViewPartyLinksQuery(string partyName)
        {
            PartyName = partyName ?? throw new ArgumentNullException(nameof(partyName));
        }

        public string PartyName { get; set; }
    }
}
