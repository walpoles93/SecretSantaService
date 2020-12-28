using MediatR;
using Microsoft.AspNetCore.Mvc;
using SecretSantaService.Application.Commands.CreateParty;
using SecretSantaService.Application.Queries.ViewPartyLinks;
using System;
using System.Threading.Tasks;

namespace SecretSantaService.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartiesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PartiesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<Unit> CreateParty(CreatePartyCommand command) => await _mediator.Send(command);

        [HttpGet("{partyName}")]
        public async Task<ViewPartyLinksDto> GetPartyLinks(string partyName) => await _mediator.Send(new ViewPartyLinksQuery(partyName));
    }
}
