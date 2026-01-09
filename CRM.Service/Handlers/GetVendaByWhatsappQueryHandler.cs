using Exemplo.Domain.Model;
using Exemplo.Domain.Model.Dto;
using Exemplo.Domain.Model.Enum;
using Exemplo.Persistence;
using Exemplo.Service.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Exemplo.Service.Handlers
{
    public class GetVendaByWhatsappQueryHandler : IRequestHandler<GetVendaByWhatsappQuery, VendaModel>
    {
        private readonly ExemploDbContext _context;

        public GetVendaByWhatsappQueryHandler(ExemploDbContext context)
        {
            _context = context;
        }

        public async Task<VendaModel> Handle(
            GetVendaByWhatsappQuery request,
            CancellationToken cancellationToken
        )
        {
            var vinculada = await _context.VendaWhatsapp
            .Include(x => x.Venda)
            .FirstOrDefaultAsync(x =>
                x.WhatsappChatId == request.WhatsappChatId &&
                x.WhatsappUserId == request.WhatsappUserId,
                cancellationToken);

            if (vinculada != null)
            {
                return vinculada.Venda;
            }

            var phone = request.WhatsappChatId.Split('@')[0];

            string Normalize(string input)
            {
                return new string(input.Where(char.IsDigit).ToArray());
            }

            var normalizedPhone = Normalize(phone);

            var venda = await _context.Venda
                .AsNoTracking()
                .FirstOrDefaultAsync(v =>
                    normalizedPhone.Contains(
                        v.Contato
                            .Replace(" ", "")
                            .Replace("-", "")
                            .Replace("(", "")
                            .Replace(")", "")
                    ),
                    cancellationToken
                );

            return venda;
        }

    }
}
