using Exemplo.Domain.Model;
using Exemplo.Service.Commands;
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

        //[HttpGet]
        //[ProducesResponseType(typeof(PagedResult<VendaModel>), StatusCodes.Status200OK)]
        //public async Task<IActionResult> BuscarVendas([FromQuery] BuscarVendasQuery query)
        //{
        //    var vendas = await _mediator.Send(query);

        //    return Ok(vendas);
        //}
    }
}
