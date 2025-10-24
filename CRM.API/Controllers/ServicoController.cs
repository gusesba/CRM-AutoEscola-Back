using Exemplo.Domain.Model;
using Exemplo.Service.Commands;
using Exemplo.Service.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Renova.API.Controllers
{
    [ApiController]
    [Route("api/servico")]
    [Authorize("UserOrAdmin")]
    public class ServicoController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ServicoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize("AdminOnly")]
        [ProducesResponseType(typeof(ExemploModel), StatusCodes.Status201Created)]

        public async Task<IActionResult> Registrar([FromBody] CriarServicoCommand command)
        {
            var servico = await _mediator.Send(command);

            return Ok(servico);
        }
    }
}
