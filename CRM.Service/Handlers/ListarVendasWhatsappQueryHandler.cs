using Exemplo.Domain.Model.Dto;
using Exemplo.Persistence;
using Exemplo.Service.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Exemplo.Service.Handlers
{
    public class ListarVendasWhatsappQueryHandler
        : IRequestHandler<ListarVendasWhatsappQuery, List<VendaWhatsappDto>>
    {
        private readonly ExemploDbContext _context;

        public ListarVendasWhatsappQueryHandler(ExemploDbContext context)
        {
            _context = context;
        }

        public async Task<List<VendaWhatsappDto>> Handle(
            ListarVendasWhatsappQuery request,
            CancellationToken cancellationToken)
        {
            var query = _context.VendaWhatsapp
                .AsNoTracking()
                .Include(vw => vw.Venda);

            if (!string.IsNullOrWhiteSpace(request.Pesquisa))
            {
                var filtro = request.Pesquisa.ToLower();
                query = query.Where(vw =>
                    vw.Venda != null &&
                    ((vw.Venda.Cliente ?? string.Empty).ToLower().Contains(filtro) ||
                     (vw.Venda.Contato ?? string.Empty).ToLower().Contains(filtro)));
            }

            return await query
                .Select(vw => new VendaWhatsappDto
                {
                    Id = vw.Id,
                    VendaId = vw.VendaId,
                    WhatsappChatId = vw.WhatsappChatId,
                    WhatsappUserId = vw.WhatsappUserId,
                    Venda = vw.Venda
                })
                .ToListAsync(cancellationToken);
        }
    }
}
