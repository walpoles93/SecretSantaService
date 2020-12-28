using System;

namespace SecretSantaService.Application.Queries.ViewPairingDetails
{
    public class ViewPairingDetailsDto
    {
        public string PartyName { get; set; }
        public DateTime PartyDate { get; set; }

        public string DonorName { get; set; }

        public string RecipientName { get; set; }
        public string RecipientAddress { get; set; }
    }
}
