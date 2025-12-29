using Exemplo.Domain.Model;
using Exemplo.Domain.Settings;
using Exemplo.Service.Commands;
using Exemplo.Service.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Renova.API.Controllers
{
    [ApiController]
    [Route("api/venda")]
    [Authorize("UserOrAdmin")]
    public class VendaController : ControllerBase
    {
        private readonly IMediator _mediator;
        public VendaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(VendaModel), StatusCodes.Status201Created)]

        public async Task<IActionResult> Registrar([FromBody] CriarVendaCommand command)
        {
            var venda = await _mediator.Send(command);

            return Created($"/api/venda/{venda.Id}",venda);
        }

        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<VendaModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> BuscarVendas([FromQuery] BuscarVendasQuery query)
        {
            var vendas = await _mediator.Send(query);

            return Ok(vendas);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(VendaModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> BuscarVendaById([FromRoute] BuscarVendaByIdQuery query)
        {
            var venda = await _mediator.Send(query);

            return Ok(venda);
        }

        [HttpPut]
        [ProducesResponseType(typeof(VendaModel), StatusCodes.Status200OK)]

        public async Task<IActionResult> Editar([FromBody] EditarVendaCommand command)
        {
            var venda = await _mediator.Send(command);

            return Ok(venda);
        }

        [HttpPatch("transferir")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> TransferirVendas([FromBody] TransferirVendasCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> Dashboard([FromQuery] DashboardQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
