using Exemplo.Domain.Model;
using Exemplo.Service.Commands;
using Exemplo.Service.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Renova.API.Controllers
{
    [ApiController]
    [Route("api/usuario")]
    [Authorize("UserOrAdmin")]
    public class UsuarioController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsuarioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ExemploModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] LoginQuery request)
        {
            var token = await _mediator.Send(request);
            if (token == null)
                return Unauthorized();

            return Ok(token);
        }

        [HttpPost("registrar")]
        [Authorize("AdminOnly")]
        [ProducesResponseType(typeof(ExemploModel), StatusCodes.Status201Created)]

        public async Task<IActionResult> Registrar([FromBody] SignUpCommand command)
        {
            var token = await _mediator.Send(command);

            return Ok(token);
        }
    }
}
