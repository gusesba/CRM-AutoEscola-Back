using Exemplo.Domain.Model.Dto;
using Exemplo.Domain.Settings;
using Exemplo.Service.Config;

namespace Exemplo.Service.Queries
{
    public class BuscarUsuariosQuery : BasePaginatedRequest<PagedResult<UsuarioDto>>
    {
        public int? Id { get; set; }
        public string? Nome { get; set; }
        public string? Usuario { get; set; }
    }
}
