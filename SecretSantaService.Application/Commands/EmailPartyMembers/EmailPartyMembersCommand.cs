using MediatR;

namespace SecretSantaService.Application.Commands.EmailPartyMembers
{
    public class EmailPartyMembersCommand : IRequest
    {
        public string PartyName { get; set; }
    }
}
