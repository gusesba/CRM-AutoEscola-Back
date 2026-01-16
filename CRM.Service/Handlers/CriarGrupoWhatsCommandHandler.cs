using Exemplo.Domain.Model;
using Exemplo.Persistence;
using Exemplo.Service.Commands;
using Exemplo.Service.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Exemplo.Service.Handlers
{
    public class CriarGrupoWhatsCommandHandler
        : IRequestHandler<CriarGrupoWhatsCommand, GrupoWhatsappModel>
    {
        private readonly ExemploDbContext _context;

        public CriarGrupoWhatsCommandHandler(ExemploDbContext context)
        {
            _context = context;
        }

        public async Task<GrupoWhatsappModel> Handle(
            CriarGrupoWhatsCommand request,
            CancellationToken cancellationToken)
        {
            // 🔎 Verifica se a venda existe
            var grupoExists = await _context.GrupoWhatsapp
                .FirstOrDefaultAsync(v => v.Nome == request.Nome, cancellationToken);

            if (grupoExists != null)
                throw new ConflictException("Grupo já criado.");

            var usuarioExiste = await _context.Usuario
                .AnyAsync(u => u.Id == request.UsuarioId, cancellationToken);

            if (!usuarioExiste)
                throw new NotFoundException("Usuário não encontrado.");

            if (request.DataInicialDe.HasValue
                && request.DataInicialAte.HasValue
                && request.DataInicialDe.Value.Date > request.DataInicialAte.Value.Date)
                throw new ValidationException("Data inicial não pode ser maior que a data final.");

            var grupo = new GrupoWhatsappModel()
            {
                Nome = request.Nome,
                UsuarioId = request.UsuarioId
            };

            var entity = _context.GrupoWhatsapp.Add(grupo);
            await _context.SaveChangesAsync(cancellationToken);

            if (request.Status.HasValue || request.DataInicialDe.HasValue || request.DataInicialAte.HasValue)
            {
                var leadsQuery = _context.VendaWhatsapp
                    .Include(vw => vw.Venda)
                    .AsQueryable();

                if (request.Status.HasValue)
                    leadsQuery = leadsQuery.Where(vw => vw.Venda.Status == request.Status.Value);

                if (request.DataInicialDe.HasValue)
                    leadsQuery = leadsQuery.Where(vw => vw.Venda.DataInicial >= request.DataInicialDe.Value);

                if (request.DataInicialAte.HasValue)
                    leadsQuery = leadsQuery.Where(vw => vw.Venda.DataInicial <= request.DataInicialAte.Value);

                var vendaWhatsIds = await leadsQuery
                    .Select(vw => vw.Id)
                    .ToListAsync(cancellationToken);

                if (vendaWhatsIds.Count > 0)
                {
                    var gruposVenda = vendaWhatsIds.Select(id => new GrupoVendaWhatsappModel
                    {
                        IdGrupo = entity.Entity.Id,
                        IdVendaWhats = id
                    });

                    _context.GrupoVendaWhatsapp.AddRange(gruposVenda);
                    await _context.SaveChangesAsync(cancellationToken);
                }
            }

            return entity.Entity;
        }
    }
}
