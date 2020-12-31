using MediatR;
using Microsoft.AspNetCore.Mvc;
using SecretSantaService.Application.Queries.ViewPartyPairings;
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

        [HttpGet]
        public async Task<ViewPartyPairingsDto> GetPairingDetails(string partyName)
            => await _mediator.Send(new ViewPartyPairingsQuery { Name = partyName });
    }
}
