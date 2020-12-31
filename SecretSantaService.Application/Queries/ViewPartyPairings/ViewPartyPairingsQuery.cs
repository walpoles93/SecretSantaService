using MediatR;

namespace SecretSantaService.Application.Queries.ViewPartyPairings
{
    public class ViewPartyPairingsQuery : IRequest<ViewPartyPairingsDto>
    {
        public string Name { get; set; }
    }
}
