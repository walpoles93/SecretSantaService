using MediatR;
using Microsoft.AspNetCore.Mvc;
using SecretSantaService.Application.Commands.CreateParty;
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
    }
}
