using MediatR;

namespace SecretSantaService.Application.Queries.ViewPairingDetails
{
    public class ViewPairingDetailsQuery : IRequest<ViewPairingDetailsDto>
    {
        public string PartyName { get; set; }
        public string PairingIdentifier { get; set; }
    }
}
