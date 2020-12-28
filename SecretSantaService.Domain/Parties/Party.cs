using SecretSantaService.Domain.Common.Models;
using System;
using System.Collections.Generic;

namespace SecretSantaService.Domain.Parties
{
    public class Party : Entity
    {
        private readonly List<PartyMember> _partyMembers = new List<PartyMember>();

        public Party(string name, DateTime date)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Date = date;
        }

        private Party() { }

        public string Name { get; private set; }
        public DateTime Date { get; private set; }
        public IReadOnlyCollection<PartyMember> PartyMembers => _partyMembers;

        public void AddPartyMember(string name, string address)
        {
            _partyMembers.Add(new PartyMember(name, address));
        }
    }
}
