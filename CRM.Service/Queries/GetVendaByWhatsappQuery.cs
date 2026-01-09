using Exemplo.Domain.Model;
using MediatR;

namespace Exemplo.Service.Queries
{
    public class GetVendaByWhatsappQuery : IRequest<VendaModel>
    {
        public string WhatsappChatId { get; set; } = string.Empty;
        public string WhatsappUserId { get; set; } = string.Empty;
    }
}
