using Exemplo.Domain.Model;
using Exemplo.Persistence;
using Exemplo.Service.Commands;
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
                throw new InvalidOperationException("Grupo já criado");

            var grupo = new GrupoWhatsappModel()
            {
                Nome = request.Nome
            };

            var entity = _context.GrupoWhatsapp.Add(grupo);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Entity;
        }
    }
}
