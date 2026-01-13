using Exemplo.Domain.Model;
using MediatR;

namespace Exemplo.Service.Commands
{
    public class CriarGrupoWhatsCommand : IRequest<GrupoWhatsappModel>
    {
        public string Nome { get; set; }
    }
}
