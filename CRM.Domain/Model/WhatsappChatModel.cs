using System.Text.Json.Serialization;

namespace Exemplo.Domain.Model
{
    public class WhatsappChatModel
    {
        public string Id { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public bool IsGroup { get; set; }
        public int UnreadCount { get; set; }
        public string? ProfilePicUrl { get; set; }
        public string? LastMessageBody { get; set; }
        public long? LastMessageTimestamp { get; set; }
        public int UsuarioId { get; set; }

        [JsonIgnore]
        public UsuarioModel Usuario { get; set; }

        [JsonIgnore]
        public ICollection<WhatsappMensagemModel> Mensagens { get; set; }
    }
}
