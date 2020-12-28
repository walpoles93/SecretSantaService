using MediatR;
using System;
using System.Collections.Generic;

namespace SecretSantaService.Application.Commands.CreateParty
{
    public class CreatePartyCommand : IRequest
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<CreatePartyMember> PartyMembers { get; set; }

        public class CreatePartyMember
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }
        }
    }
}
