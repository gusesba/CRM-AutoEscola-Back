using Exemplo.Domain.Model.Dto;
using MediatR;

namespace Exemplo.Service.Queries
{
    public class BuscarGruposWhatsappQuery : IRequest<List<GrupoWhatsappDto>>
    {
        public int? Id { get; set; }
        public int? UsuarioId { get; set; }
    }
}
