using MediatR;
using Microsoft.AspNetCore.Mvc;
using SecretSantaService.Application.Queries.ViewPairingDetails;
using System;
using System.Threading.Tasks;

namespace SecretSantaService.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PairingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PairingsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("{partyName}/{pairingIdentifier}")]
        public async Task<ViewPairingDetailsDto> GetPairingDetails(string partyName, string pairingIdentifier)
        {
            var query = new ViewPairingDetailsQuery
            {
                PartyName = partyName,
                PairingIdentifier = pairingIdentifier,
            };

            return await _mediator.Send(query);
        }
    }
}
