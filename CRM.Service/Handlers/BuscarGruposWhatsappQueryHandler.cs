using Exemplo.Domain.Model;
using Exemplo.Domain.Model.Dto;
using Exemplo.Persistence;
using Exemplo.Service.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Exemplo.Service.Handlers
{
    public class BuscarGruposWhatsappQueryHandler : IRequestHandler<BuscarGruposWhatsappQuery, List<GrupoWhatsappDto>>
    {
        private readonly ExemploDbContext _context;

        public BuscarGruposWhatsappQueryHandler(ExemploDbContext context)
        {
            _context = context;
        }

        public async Task<List<GrupoWhatsappDto>> Handle(
            BuscarGruposWhatsappQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<GrupoWhatsappModel> query = _context.GrupoWhatsapp
                .AsNoTracking()
                .Include(g => g.GruposVendaWhatsapp)
                .ThenInclude(gv => gv.VendaWhatsapp);

            if (request.Id.HasValue)
            {
                query = query.Where(g => g.Id == request.Id.Value);
            }

            return await query
                .OrderBy(g => g.Id)
                .Select(g => new GrupoWhatsappDto
                {
                    Id = g.Id,
                    Nome = g.Nome,
                    Conversas = g.GruposVendaWhatsapp
                        .OrderBy(gv => gv.Id)
                        .Select(gv => new GrupoWhatsappConversaDto
                        {
                            Id = gv.Id,
                            VendaWhatsappId = gv.IdVendaWhats,
                            VendaId = gv.VendaWhatsapp.VendaId,
                            WhatsappChatId = gv.VendaWhatsapp.WhatsappChatId,
                            WhatsappUserId = gv.VendaWhatsapp.WhatsappUserId
                        })
                        .ToList()
                })
                .ToListAsync(cancellationToken);
        }
    }
}
